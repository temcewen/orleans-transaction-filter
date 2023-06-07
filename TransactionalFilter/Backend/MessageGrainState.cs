namespace TransactionalFilter.Backend;

[GenerateSerializer]
public sealed record MessageGrainState
{
    [Id(0)]
    public List<String> Messages { get; set; }
}
