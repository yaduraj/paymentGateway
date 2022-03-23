using paymentGateway.Contracts;

namespace paymentGateway.ResponseType;

public class TransactionDataResponse
{
    public string TransactionId {get; set;}

    public TransactionData CardData {get; set;}

    public string TransactionStatus {get; set;}
    
}