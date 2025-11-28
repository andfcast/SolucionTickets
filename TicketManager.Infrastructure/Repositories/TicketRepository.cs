using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.CompilerServices;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;
using TicketManager.Infrastructure.Persistence;

namespace TicketManager.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketDbContext _context;

        public TicketRepository(TicketDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> GetAll() {
            return await _context.Tickets.Include(x => x.Comments).ThenInclude(y => y.User).Include(x => x.Category).Include(x => x.Status).Include(x => x.User).ToListAsync();
        }
        public async Task<Ticket> GetById(int id) {
            return await _context.Tickets.Include(x => x.Comments).ThenInclude(y => y.User).Include(x => x.Category).Include(x => x.Status).Include(x => x.User).FirstAsync(x => x.Id == id);
        }
        public async Task<int> Insert(Ticket entity) {
            try
            {
                await _context.Tickets.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity.Id;
            }
            catch {
                return -1;
            }            
        }
        public async Task<bool> Update(Ticket entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) {
                return false;
            }
        }
        public async Task<bool> Delete(int id)
        {
            Ticket objDelete = await _context.Tickets.FirstAsync(x => x.Id == id);
            objDelete.Active = false;
            _context.Entry(objDelete).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<int> AddComment(TicketComment comment) {
            await _context.TicketComments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment.Id;
        }
    }
}
