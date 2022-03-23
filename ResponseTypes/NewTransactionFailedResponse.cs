namespace paymentGateway.ResponseType;

public record NewTransactionFailedResponse
{
    public string TransactionId {get; set; }
    public string ErrorMessage {get; set;}

    public string TransactionStatus {get; set;}
}