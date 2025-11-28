using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManager.Domain.Entities;

namespace TicketManager.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(int id, string username);
    }
}
