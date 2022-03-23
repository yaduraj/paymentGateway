namespace AcquiringBank.SDK;

public record PaymentStatus
{
    public bool IsSuccessful {get; set;}

    public string? ErrorMessage {get; set;}
}