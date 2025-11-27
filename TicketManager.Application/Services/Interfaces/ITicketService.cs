using TicketManager.Application.DTO.Entities;
using TicketManager.Application.DTO.RequestResponse;

namespace TicketManager.Application.Services.Interfaces
{
    public interface ITicketService
    {
        Task<ResponseDTO> GetAll();
        Task<ResponseDTO> GetById(int id);
        Task<ResponseDTO> CreateNew(TicketDTO objDto);
        Task<ResponseDTO> Update(TicketDTO objDto);
        Task<ResponseDTO> InsertComment(TicketLogDTO objLog);
        Task<ResponseDTO> Delete(int id);
    }
}
