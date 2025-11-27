using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManager.Application.DTO.Entities
{
    public class TicketDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "No debe pasar de 20 caracteres")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Campo obligatorio")]
        [MaxLength(100,ErrorMessage = "No debe pasar de 100 caracteres")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public bool Active { get; set; }
        public List<TicketLogDTO> Comments { get; set; }
    }
}
