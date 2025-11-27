using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManager.Application.DTO.Entities
{
    public class TicketLogDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public int TicketId { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public string UserName { get; set; }
        public string FullUserName { get; set; }
        public DateTime RegDate { get; set; } = DateTime.Now;
    }
}
