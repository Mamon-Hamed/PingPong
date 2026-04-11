using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Infrastructure.Persistence.Converters;

public class StronglyTypedIdValueConverter<TId>() : ValueConverter<TId, Guid>(id => id.Value,
    guid => (TId)Activator.CreateInstance(typeof(TId), guid)!)
    where TId : StronglyTypedId;
