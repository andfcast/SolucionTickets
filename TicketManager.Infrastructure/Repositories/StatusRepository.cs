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
    public class StatusRepository : IStatusRepository
    {
        private readonly TicketDbContext _context;
        public StatusRepository(TicketDbContext context) {
            _context = context;
        }

        public Task<List<TicketStatus>> GetAll()
        {
            return _context.TicketStatuses.ToListAsync();
        }

        public Task<TicketStatus> GetStatus(int id)
        {            
            return _context.TicketStatuses.FirstAsync(x => x.Id == id);            
        }

        public async Task<bool> IsValidStatus(int id)
        {
            return await _context.TicketStatuses.CountAsync(x => x.Id == id) > 0;
        }

        
    }
}
