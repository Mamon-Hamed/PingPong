using Cortex.Mediator.Caching;

namespace PingPong.Application.Abstractions.Messaging;

// public class CacheKeyGenerator : ICacheKeyGenerator
// {
//     public string GenerateKey<TQuery, TResult>(TQuery query) 
//         where TQuery : IQuery<TResult>
//     {
//         return $"PingPong:{typeof(TQuery).Name}:{query.GetHashCode()}";
//     }
// }