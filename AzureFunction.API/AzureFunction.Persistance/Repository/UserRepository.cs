using AzureFunction.Application.Interface;
using AzureFunction.Domain.Entities;
using AzureFunction.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunction.Persistance.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Task.FromResult(user);
            throw new NotImplementedException();
        }
    }
}
