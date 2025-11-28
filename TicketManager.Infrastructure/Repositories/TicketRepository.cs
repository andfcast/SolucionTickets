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
            List<Ticket> lstTickets = await _context.Tickets.Include(x => x.Category).Include(x => x.Status).Include(x => x.User).Where(y => y.Active == true).ToListAsync();
            foreach (var item in lstTickets) {
                item.Comments = await _context.TicketComments.Include(x => x.User).Where(y => y.TicketId == item.Id).ToListAsync();
            }
            return lstTickets;
        }
        public async Task<Ticket> GetById(int id) {

            try
            {
                Ticket objTicket = await _context.Tickets.Include(x => x.Category).Include(x => x.Status).Include(x => x.User).FirstAsync(x => x.Id == id && x.Active == true);
                objTicket.Comments = await _context.TicketComments.Include(x => x.User).Where(y => y.TicketId == objTicket.Id).ToListAsync();
                return objTicket! as Ticket;
            }
            catch {
                return null;
            }            
        }
        public async Task<int> Insert(Ticket entity) {
            try
            {
                entity.CreationDate = DateTime.Now;
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
                entity.UpdateDate = DateTime.Now;
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
            objDelete.UpdateDate = DateTime.Now;
            _context.Entry(objDelete).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<int> AddComment(TicketComment comment) {
            await _context.TicketComments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment.Id;
        }

        public async Task<bool> IsValidTicket(int id)
        {
            return await _context.Tickets.CountAsync(x => x.Id == id) > 0;
        }
    }
}
