using TransactionalFilter.Backend;

namespace TransactionalFilter.Api;

public sealed class Mutation
{
    private readonly IGrainFactory _grainFactory;

    public Mutation(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }

    public async Task<MessageModel> SendMessage(Guid senderId, Guid recipientId, string message)
    {
        await _grainFactory.GetGrain<IMessageGrain>(senderId).SendMessage(recipientId, message);
        return new MessageModel()
        {
            Message = message,
        };
    }
}

public sealed class MessageModel
{
    public required string Message { get; set; }
}
