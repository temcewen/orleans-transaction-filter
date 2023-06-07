using Orleans.Concurrency;
using Orleans.Runtime;
using Orleans.Transactions;
using Orleans.Transactions.Abstractions;

namespace TransactionalFilter.Backend;

[Reentrant]
public sealed class MessageGrain : IGrainBase, IMessageGrain
{
    public IGrainContext GrainContext { get; }

    private readonly IGrainFactory _grainFactory;

    private readonly TransactionalState<MessageGrainState> _state;

    public MessageGrain(IGrainContext context, IGrainFactory grainFactory, [TransactionalState(nameof(MessageGrainState))] TransactionalState<MessageGrainState> state)
    {
        GrainContext = context;
        _grainFactory = grainFactory;
        _state = state;
    }
    
    public async Task SendMessage(Guid recipient, string message)
    {
        await _grainFactory.GetGrain<IMessageGrain>(recipient).ReceiveMessage(message);
    }

    public Task ReceiveMessage(string message)
    {
        throw new Exception("Test");
    }
}
