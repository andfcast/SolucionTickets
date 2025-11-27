using TicketManager.Domain.Entities;

namespace TicketManager.Domain.Repositories
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAll();
        Task<Ticket> GetById(int id);
        Task<int> Insert(Ticket entity);
        Task<bool> Update(Ticket entity);
        Task<bool> Delete(int id);
        Task<int> AddComment(TicketComment comment);
    }
}
