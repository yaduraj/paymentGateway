namespace AcquiringBank.SDK;

public interface IAcquiringBank
{
    void SetPaymentStatus(PaymentStatus paymentStatus);
    PaymentStatus MakePayment(float amount, string CardNumber, string CVV, string expiryDates);
}