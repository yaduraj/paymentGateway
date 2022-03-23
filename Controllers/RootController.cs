using Microsoft.AspNetCore.Mvc;

namespace paymentGateway.Controllers;

[ApiController]
[Route("/")]
public class RootController : ControllerBase
{
    private readonly ILogger<RootController> logger;

    public RootController(ILogger<RootController> logger)
    {
        this.logger = logger;
    }

    [HttpGet(Name = "Root")]
    public ContentResult Get()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "View", "index.html");
        var text = System.IO.File.ReadAllText(path);
        return new ContentResult(){
            ContentType = "text/html",
            Content = text,
            StatusCode = (int)System.Net.HttpStatusCode.OK
        };
    }
}
