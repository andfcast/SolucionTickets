using TicketManager.Application.DTO.Entities;
using TicketManager.Application.DTO.RequestResponse;
using TicketManager.Application.Services.Interfaces;
using TicketManager.Application.Utilities;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;

namespace TicketManager.Application.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repository;
        public TicketService(ITicketRepository repository) {
            _repository = repository;
        }

        public async Task<ResponseDTO> GetAll() { 
            List<Ticket> lstTickets = await _repository.GetAll();
            List<TicketDTO> lstDto = new List<TicketDTO>();
            if (lstTickets != null) { 
                foreach(var item in lstTickets)
                {
                    lstDto.Add(Utils.ConvertToDTO(item));
                }
            }
            return new ResponseDTO { 
                IsValid = lstDto.Any(),
                Message = lstDto.Any() ? "": "No hay registros",
                ResultData = lstDto.Any() ? lstDto : null
            }; 
        }
        public async Task<ResponseDTO> GetById(int id) { 
            Ticket objTicket = await _repository.GetById(id);
            return new ResponseDTO
            {
                IsValid = objTicket != null,
                Message = "",
                ResultData = Utils.ConvertToDTO(objTicket)
            }; 
        }
        public async Task<ResponseDTO> CreateNew(TicketEditDTO objDto) { return new ResponseDTO(); }
        public async Task<ResponseDTO> Update(TicketEditDTO objDto) { return new ResponseDTO(); }
        public async Task<ResponseDTO> InsertComment(TicketLogDTO objLog) { return new ResponseDTO(); }
        public async Task<ResponseDTO> Delete(int id) { return new ResponseDTO(); }
    }
}
