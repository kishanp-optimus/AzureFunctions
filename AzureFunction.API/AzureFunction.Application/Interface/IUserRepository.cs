using AzureFunction.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunction.Application.Interface
{
    public interface IUserRepository
    {
        public Task<User> CreateUser(User user);
    }
}
