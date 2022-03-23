namespace paymentGateway.Validators;

using System.ComponentModel.DataAnnotations;
using paymentGateway.Contracts;

public class CardDataValidator : ValidationAttribute
{
    private string errorMessage = string.Empty;
    private IList<string> allowedCurrencies = new List<string>(){"GBP", "USD", "INR", "EUR"};
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value == null) return new ValidationResult("Value is null");

        var cardData = (TransactionData)value;

        if(ValidateAmount(cardData.Amount) && ValidateExpiryDate(cardData.ExpiryDate) && ValidateCurrency(cardData.Currency))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(errorMessage);
    }

    private bool ValidateAmount(float amount)
    {
        if(amount >= 0) return true;

        errorMessage = "Amount cannot be negative";

        return false;
    }

    private bool ValidateExpiryDate(string expiryDate)
    {
        try
        {
            var parsedDate = DateTime.ParseExact(expiryDate, "MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var currentDate = DateTime.Now;
            
            if((parsedDate.Year < currentDate.Year) || ((parsedDate.Year == currentDate.Year) && (parsedDate.Month < currentDate.Month)))
            {
                errorMessage = "The date entered should be a future date.";
                return false;
            }

            return true;
        }
        catch(Exception e)
        {
            errorMessage = "Date format is incorrect. It should be of the form MM/YYYY";
        }
        
        return false;
    }

    private bool ValidateCurrency(string currency)
    {
        if(allowedCurrencies.Contains(currency)) return true;

        errorMessage = "Invalid Currency. The allowed values are GBP, USD, INR, EUR.";

        return false;
    }
}