using System.Linq.Expressions;
using Model.Dtos;
using Model.Entities;

namespace Abstraction.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserByNameAsync(Expression<Func<User, bool>> filter);
}