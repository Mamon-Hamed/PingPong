using PingPong.Domain.Entities;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Repositories;

public interface ICountryRepository : IRepository<CountryEntity, CountryId>;
