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
        public string Title { get; set; }
        [Required(ErrorMessage ="Campo oligatorio")]
        [MaxLength(100,ErrorMessage = "No debe pasar de 100 caracteres")]
        public string Description { get; set; }
        public string StatusName { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }
        public bool Active { get; set; }
    }
}
