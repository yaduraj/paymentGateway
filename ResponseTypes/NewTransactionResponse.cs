namespace paymentGateway.ResponseType;

public record NewTransactionResponse
{
    public string TransactionId {get; set;}

    public string TransactionStatus {get; set;}
}