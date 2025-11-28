using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManager.Application.DTO.Entities
{
    public class TicketDTO: TicketEditDTO
    {
        public string StatusName { get; set; } = string.Empty;               
        public string CategoryName { get; set; } = string.Empty;
        public string UserFullName { get; set; } = string.Empty;
        public bool? Active { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public List<TicketLogDTO>? Comments { get; set; }
    }

    public class TicketEditDTO {
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "No debe pasar de 20 caracteres")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(100, ErrorMessage = "No debe pasar de 100 caracteres")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public int StatusId { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
    }
}
