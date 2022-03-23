namespace paymentGateway;

public class InMemoryDatabase<T> : IDatabase<T> where T : class
{
    private Dictionary<string, T> store = new Dictionary<string, T>();

    public Task<T?> Get(string id)
    {
        var tcs = new TaskCompletionSource<T?>();
        if(store.TryGetValue(id, out var data))
        {
            tcs.SetResult(data);
            return tcs.Task;
        }
        tcs.SetResult(null);
        return tcs.Task;
    }

    public async Task Add(string id, T data)
    {
        if(store.ContainsKey(id))
        {
            store[id] = data;
        }

        store.Add(id, data);
    }
}