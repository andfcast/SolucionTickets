using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManager.Application.DTO.Entities
{
    public class TicketLogDTO
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Text { get; set; }
        public string User { get; set; }
        public DateTime RegDate { get; set; }
    }
}
