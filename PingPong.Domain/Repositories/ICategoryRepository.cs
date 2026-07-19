using PingPong.Domain.StronglyTypes;
using PingPong.Domain.Entities.Categories;

namespace PingPong.Domain.Repositories;

public interface ICategoryRepository : IRepository<CategoryEntity, CategoryId>;