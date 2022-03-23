using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using paymentGateway.Validators;

namespace paymentGateway.Contracts;

[CardDataValidator]
public class TransactionData
{
    [CreditCard]
    [Required]
    public string CardNumber {get; set;}

    [Required]  
    [Range(0.0001, float.MaxValue)]
    public float Amount {get; set;} 

    [Required]
    [MinLength(3), MaxLength(4)]
    [RegularExpression("^[0-9]*$")]
    public string CVV {get; set;}

    [Required]
    public string ExpiryDate {get; set;}

    [Required]
    public string Currency {get; set;}
    
}