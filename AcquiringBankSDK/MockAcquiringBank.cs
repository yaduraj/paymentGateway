namespace AcquiringBank.SDK;

public class MockAcquiringBank : IAcquiringBank
{
    private PaymentStatus paymentStatus;

    public void SetPaymentStatus(PaymentStatus paymentStatus)
    {
        this.paymentStatus = paymentStatus;
    }
    
    public PaymentStatus MakePayment(float amount, string CardNumber, string CVV, string expiryDates)
    {
        return paymentStatus;
    }
}