using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Interfaces;
using TicketManager.Infrastructure.Persistence;

namespace TicketManager.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TicketDbContext _context;
        public UserRepository(TicketDbContext context) {
            _context = context;
        }

        public Task<User> GetUser(int id, string username)
        {
            if (id != 0)
                return _context.Users.FirstAsync(x => x.Id == id);
            if (!string.IsNullOrEmpty(username)) { 
                return _context.Users.FirstAsync(x => x.UserName == username);
            }
            return null;
        }
    }
}
