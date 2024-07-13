using System.Linq.Expressions;
using Abstraction.Interfaces.Repositories;
using AutoMapper;
using DAL.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Model.Dtos;
using Model.Entities;

namespace DAL.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }


    public async Task<User?> GetUserByNameAsync(Expression<Func<User, bool>> filter)
    {
        return await Set
            .FirstOrDefaultAsync(filter);
    }
}   