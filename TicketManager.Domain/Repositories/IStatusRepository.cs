using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManager.Domain.Entities;

namespace TicketManager.Domain.Repositories
{
    public interface IStatusRepository
    {
        Task<List<TicketStatus>> GetAll();
        Task<TicketStatus> GetStatus(int id);
        Task<bool> IsValidStatus(int id);
    }
}
