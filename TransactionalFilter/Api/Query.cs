using HotChocolate.Authorization;
using TransactionalFilter.Api.Models;

namespace TransactionalFilter.Api;

public sealed class Query
{
    private readonly IGrainFactory _grainFactory;

    public Query(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }

    public string HelloWorld => "Hello, World!";

    public ApiObject GetObject()
    {
        return new ApiObject
        {
            Id = Guid.NewGuid(),
            Example = "example",
        };
    }
}
