using paymentGateway.Contracts;

namespace paymentGateway.DTO;

public enum TransactionStatus
{
    Success,
    Failed
}
public class TransactionDataDTO
{
    public  TransactionData TransactionData {get; set;}    

    public TransactionStatus TransactionStatus {get; set;}

    public string FailureReason {get; set;}
}