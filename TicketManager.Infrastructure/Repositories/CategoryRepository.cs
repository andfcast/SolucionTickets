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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TicketDbContext _context;
        public CategoryRepository(TicketDbContext context) {
            _context = context;
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {            
            return await _context.Categories.FirstAsync(x => x.Id == id);            
        }

        public async Task<bool> IsValidCategory(int id)
        {
            return await _context.Categories.CountAsync(x => x.Id == id) > 0;
        }
    }
}
