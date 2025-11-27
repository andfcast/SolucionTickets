using TicketManager.Application.DTO.Entities;
using TicketManager.Application.DTO.RequestResponse;
using TicketManager.Application.Services.Interfaces;
using TicketManager.Domain.Repositories;

namespace TicketManager.Application.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repository;
        public TicketService(ITicketRepository repository) {
            _repository = repository;
        }

        public async Task<ResponseDTO> GetAll() { return new ResponseDTO(); }
        public async Task<ResponseDTO> GetById(int id) { return new ResponseDTO(); }
        public async Task<ResponseDTO> CreateNew(TicketDTO objDto) { return new ResponseDTO(); }
        public async Task<ResponseDTO> Update(TicketDTO objDto) { return new ResponseDTO(); }
        public async Task<ResponseDTO> InsertComment(TicketLogDTO objLog) { return new ResponseDTO(); }
        public async Task<ResponseDTO> Delete(int id) { return new ResponseDTO(); }
    }
}
