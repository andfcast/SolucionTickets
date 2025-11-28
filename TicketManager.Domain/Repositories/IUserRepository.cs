using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManager.Domain.Entities;

namespace TicketManager.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> GetUser(int id, string username);
        Task<bool> IsValidUser(int id, string username);
    }
}
