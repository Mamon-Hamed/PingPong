using Cortex.Mediator.Caching;

namespace PingPong.Application.Abstractions.Messaging;

public class CacheKeyGenerator : ICacheKeyGenerator
{
    public string GenerateKey<TQuery, TResult>(TQuery query) 
        where TQuery : Cortex.Mediator.Queries.IQuery<TResult>
    {
        return $"PingPong:{typeof(TQuery).Name}:{query.GetHashCode()}";
    }
}