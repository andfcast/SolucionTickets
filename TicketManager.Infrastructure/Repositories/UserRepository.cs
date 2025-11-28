using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;
using TicketManager.Infrastructure.Persistence;

namespace TicketManager.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TicketDbContext _context;
        public UserRepository(TicketDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUser(int id, string username)
        {
            if (id != 0)
                return await _context.Users.FirstAsync(x => x.Id == id);
            if (!string.IsNullOrEmpty(username))
            {
                return await _context.Users.FirstAsync(x => x.UserName.ToLower() == username.ToLower());
            }
            return null;
        }

        public async Task<bool> IsValidUser(int id, string username)
        {
            if (id != 0)
                return await _context.Users.CountAsync(x => x.Id == id) > 0;
            if (!string.IsNullOrEmpty(username))
                return await _context.Users.CountAsync(x => x.UserName.ToLower() == username.ToLower()) > 0;
            return false;
        }
    }
}
