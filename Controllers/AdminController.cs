using AcquiringBank.SDK;
using Microsoft.AspNetCore.Mvc;

namespace paymentGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly ILogger<RootController> logger;

    private readonly IAcquiringBank acquiringBank;
    public AdminController(ILogger<RootController> logger, IAcquiringBank acquiringBank)
    {
        this.logger = logger;
        this.acquiringBank = acquiringBank;
    }

    [HttpGet]
    public ContentResult Get()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "View", "admin.html");
        var text = System.IO.File.ReadAllText(path);

        logger.LogDebug("Gtting the admin page.");
        return new ContentResult(){
            ContentType = "text/html",
            Content = text,
            StatusCode = (int)System.Net.HttpStatusCode.OK
        };
    }

    [HttpPost("SetBankStatus")]
    public void SetPaymentStatus([FromBody] PaymentStatus status)
    {
        logger.LogDebug($"Setting payment status to {status.IsSuccessful}");
        acquiringBank.SetPaymentStatus(status);
    }
}
