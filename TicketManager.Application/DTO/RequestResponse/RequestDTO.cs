using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManager.Application.DTO.RequestResponse
{
    public class RequestDTO 
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public object? Body { get; set; }
    }
}
