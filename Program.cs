using AcquiringBank.SDK;
using paymentGateway;
using paymentGateway.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddSingleton<IAcquiringBank, MockAcquiringBank>();
builder.Services.AddSingleton<IDatabase<TransactionDataDTO>, InMemoryDatabase<TransactionDataDTO>>();
builder.Services.AddSingleton<PaymentTransactionOperations<TransactionDataDTO>>();

var app = builder.Build();

var x = app.Services.GetRequiredService<IAcquiringBank>();
x.SetPaymentStatus(new PaymentStatus(){IsSuccessful = true});

app.MapControllers();

app.Run();
