namespace TransactionalFilter.Backend;

public interface IMessageGrain : IGrainWithGuidKey
{
    [Transaction(TransactionOption.CreateOrJoin)]
    Task SendMessage(Guid recipient, String message);

    [Transaction(TransactionOption.CreateOrJoin)]
    Task ReceiveMessage(String message);
}
