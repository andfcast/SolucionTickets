using TicketManager.Application.DTO.Entities;
using TicketManager.Application.DTO.RequestResponse;
using TicketManager.Application.Services.Interfaces;
using TicketManager.Application.Utilities;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;
using TicketManager.Infrastructure.Repositories;

namespace TicketManager.Application.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStatusRepository _statusRepository;
        public TicketService(ITicketRepository ticketRepository, IUserRepository userRepository, ICategoryRepository categoryRepository, IStatusRepository statusRepository) {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _statusRepository = statusRepository;
        }

        public async Task<ResponseDTO> GetAll() { 
            List<Ticket> lstTickets = await _ticketRepository.GetAll();
            List<TicketDTO> lstDto = new List<TicketDTO>();
            if (lstTickets != null) { 
                foreach(var item in lstTickets)
                {
                    lstDto.Add(Utils.ConvertToDTO(item));
                }
            }
            return new ResponseDTO { 
                IsValid = lstDto.Any(),
                Message = lstDto.Any() ? "": "No items found",
                ResultData = lstDto.Any() ? lstDto : null
            }; 
        }
        public async Task<ResponseDTO> GetById(int id) { 
            Ticket objTicket = await _ticketRepository.GetById(id);
            return new ResponseDTO
            {
                IsValid = objTicket != null,
                Message = "",
                ResultData = Utils.ConvertToDTO(objTicket)
            }; 
        }
        public async Task<ResponseDTO> CreateNew(TicketEditDTO objDto) {
            ResponseDTO response = await IsValidTicket(objDto);
            if (string.IsNullOrEmpty(response.Message)) {
                return response;
            }
            int valInsert = await _ticketRepository.Insert(Utils.ConvertToEntity(objDto));
            if (valInsert < 0) {                
                response.Message = "Error creating the ticket";
                return response;
            }
            response.IsValid = true;
            response.Message = "Process ended successfully";
            response.ResultData = valInsert;
            return response;
        }
        public async Task<ResponseDTO> Update(TicketEditDTO objDto) {
            ResponseDTO response = await IsValidTicket(objDto);
            if (string.IsNullOrEmpty(response.Message)) {
                return response;
            }
            bool valUpdate = await _ticketRepository.Update(Utils.ConvertToEntity(objDto));
            if (!valUpdate)
            {
                response.Message = "Error al crear el ticket";
                return response;
            }
            response.IsValid = true;
            response.Message = "Operación exitosa";            
            return response;
        }
        public async Task<ResponseDTO> InsertComment(TicketLogDTO objLog) {
            ResponseDTO response = await IsValidComment(objLog);
            if (string.IsNullOrEmpty(response.Message))
            {
                return response;
            }
            TicketComment newComment = Utils.ConvertToEntity(objLog);
            newComment.UserId = ((await _userRepository.GetUser(0, objLog.UserName)) as User).Id;
            int valInsert = await _ticketRepository.AddComment(newComment);
            if (valInsert < 0)
            {
                response.Message = "Error inserting the xomment to the ticket";
                return response;
            }
            response.IsValid = true;
            response.Message = "Process ended successfully";
            response.ResultData = valInsert;
            return response;
        }
        public async Task<ResponseDTO> Delete(int id) {
            ResponseDTO response = new ResponseDTO();
            var ticket = await _ticketRepository.GetById(id);
            if (ticket == null)
            {
                response.Message = "The related ticket does not exist";
                return response;
            }
            response.IsValid = await _ticketRepository.Delete(id);
            response.Message = response.IsValid ? "Process ended successfully" : "Error deleting the ticket";
            return response;
        }

        private async Task<ResponseDTO> IsValidTicket(TicketEditDTO objDto ) {
            ResponseDTO response = new ResponseDTO();
            if (objDto == null) {
                response.Message = "Empty ticket";
                return response;
            } 
            var status = await _statusRepository.GetStatus(objDto.StatusId);
            if (status == null) {
                response.Message = "Invalid status";
                return response;
            }
            var category = await _categoryRepository.GetCategory(objDto.CategoryId);
            if (category == null)
            {
                response.Message = "Invalid category";
                return response;
            }
            var user = await _userRepository.GetUser(objDto.UserId, "");
            if (user == null)
            {
                response.Message = "invalid user";
                return response;
            }
            return response;
        }

        private async Task<ResponseDTO> IsValidComment(TicketLogDTO objDto)
        {
            ResponseDTO response = new ResponseDTO();
            if (objDto == null)
            {
                response.Message = "Empty comment";
                return response;
            }
            var ticket = await _ticketRepository.GetById(objDto.TicketId);
            if (ticket == null)
            {
                response.Message = "The related ticket does not exist";
                return response;
            }            
            var user = await _userRepository.GetUser(0, objDto.UserName);
            if (user == null)
            {
                response.Message = "Invalid user";
                return response;
            }
            return response;
        }
    }
}
