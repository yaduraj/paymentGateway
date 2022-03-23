using paymentGateway.Controllers;

namespace paymentGateway;

public class PaymentTransactionOperations<T> where T : class
{
    private IDatabase<T> database;
    public PaymentTransactionOperations(IDatabase<T> database)
    {
        this.database = database;
    }

    public async Task<string> AddTransaction(T transactionData)
    {
        var id = Guid.NewGuid().ToString();
        await database.Add(id, transactionData);

        return id;
    }

    public async Task<T?> GetTransaction(string transactionId)
    {
        return await database.Get(transactionId);
    }
}