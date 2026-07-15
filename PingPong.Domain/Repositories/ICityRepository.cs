using PingPong.Domain.Entities;
using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Repositories;

public interface ICityRepository : IRepository<CityEntity, CityId>;
