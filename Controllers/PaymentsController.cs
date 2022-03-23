using AcquiringBank.SDK;
using Microsoft.AspNetCore.Mvc;
using paymentGateway.Contracts;
using paymentGateway.DTO;
using paymentGateway.ResponseType;

namespace paymentGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly ILogger<PaymentsController> logger;
    private readonly IAcquiringBank acquiringBank;
    private readonly PaymentTransactionOperations<DTO.TransactionDataDTO> paymentTransactionOperations;

    public PaymentsController(ILogger<PaymentsController> logger, IAcquiringBank acquiringBank, PaymentTransactionOperations<TransactionDataDTO> paymentTransactionOperations)
    {
        this.logger = logger;
        this.acquiringBank = acquiringBank;
        this.paymentTransactionOperations = paymentTransactionOperations;
    }

    [HttpGet("transaction/{transactionId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionDataResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTransaction([FromRoute(Name = "transactionId")] string transactionId)
    {
        var data = await paymentTransactionOperations.GetTransaction(transactionId);

        if (data == null) 
        {
            return NotFound($"The transaction with transactionId: {transactionId} is not found.");
        }

        var maskedString = new string('*', data.TransactionData.CardNumber.Length);
        data.TransactionData.CardNumber = maskedString;

        return Ok(new TransactionDataResponse()
        {
            TransactionId = transactionId,
            CardData = data.TransactionData,
            TransactionStatus = data.TransactionStatus.ToString()
        });
    }

    [HttpPost("transaction")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NewTransactionResponse))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NewTransactionFailedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MakeTransaction([FromBody] TransactionData transactionData)
    {
        //Validate with acquiring bank;
        var status = acquiringBank.MakePayment(transactionData.Amount, transactionData.CardNumber, transactionData.CVV, transactionData.ExpiryDate);
        var txId = string.Empty;
        if (!status.IsSuccessful)
        {
            return await ReturnFailedResponse(transactionData, status);
        }

        return await ReturnSuccessResponse(transactionData);
    }

    private async Task<OkObjectResult> ReturnFailedResponse(TransactionData transactionData, PaymentStatus paymentStatus)
    {
        var txId = await paymentTransactionOperations.AddTransaction(new TransactionDataDTO()
        {
            TransactionData = transactionData,
            TransactionStatus = TransactionStatus.Failed,
            FailureReason = paymentStatus.ErrorMessage
        });

        return Ok(new NewTransactionFailedResponse()
        {
            TransactionId = txId,
            TransactionStatus = TransactionStatus.Failed.ToString(),
            ErrorMessage = paymentStatus.ErrorMessage
        });
    }

    private async Task<OkObjectResult> ReturnSuccessResponse(TransactionData transactionData)
    {
        var txId = await paymentTransactionOperations.AddTransaction(new TransactionDataDTO()
        {
            TransactionData = transactionData,
            TransactionStatus = TransactionStatus.Success
        });

        return Ok(new NewTransactionResponse()
        {
            TransactionId = txId,
            TransactionStatus = TransactionStatus.Success.ToString()
        });
    }
}
