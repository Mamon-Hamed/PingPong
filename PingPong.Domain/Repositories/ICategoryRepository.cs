using PingPong.Domain.StronglyTypes;

namespace PingPong.Domain.Repositories;

using Entities;

public interface ICategoryRepository : IRepository<CategoryEntity, CategoryId>;