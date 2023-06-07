namespace TransactionalFilter.Api.Models;

[Node]
public abstract class ApiModel : IEquatable<ApiModel>
{
    public required Guid Id { get; init; }

    public sealed override bool Equals(object? obj)
    {
        return obj is ApiModel other && Equals(other);
    }

    public bool Equals(ApiModel? other)
    {
        return Id == other?.Id;
    }

    public sealed override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(ApiModel lhs, ApiModel rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(ApiModel lhs, ApiModel rhs)
    {
        return !(lhs == rhs);
    }
}
