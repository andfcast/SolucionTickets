using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManager.Domain.Entities
{
    public class TicketComment
    {
        [Key]
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }        
        [Required(AllowEmptyStrings = false)]
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public Ticket? Ticket { get; set; }
        public User User { get; set; }        
    }
}
