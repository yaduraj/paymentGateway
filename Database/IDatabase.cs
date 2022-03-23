namespace paymentGateway;

public interface IDatabase<T> 
{
    Task<T?> Get(string id);

    Task Add(string id, T data);
}