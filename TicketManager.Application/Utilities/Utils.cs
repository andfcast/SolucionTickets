using TicketManager.Application.DTO.Entities;
using TicketManager.Domain.Entities;

namespace TicketManager.Application.Utilities
{
    public static class Utils
    {
        public static TicketDTO ConvertToDTO(Ticket entity)
        {
            List<TicketLogDTO> lstLogs = new List<TicketLogDTO>();
            if (entity.Comments.Count > 0)
            {
                foreach (var item in entity.Comments)
                {
                    lstLogs.Add(ConvertToDTO(item));
                }
            }
            return new TicketDTO
            {
                Id = entity.Id,
                Active = entity.Active,
                CategoryId = entity.CategoryId,
                CategoryName = entity.Category!.Description,
                Comments = lstLogs,
                Description = entity.Description,
                StatusId = entity.StatusId,
                StatusName = entity.Status!.Description,
                Title = entity.Title,
                UserFullName = entity.User!.FullName,
                UserName = entity.User.UserName
            };
        }

        public static TicketLogDTO ConvertToDTO(TicketComment entity) {
            return new TicketLogDTO
            {
                Id = entity.Id,
                TicketId = entity.TicketId,
                RegDate = entity.CreationDate,
                Text = entity.Text,
                UserName = entity.User.UserName,
                FullUserName = entity.User.FullName
            };
        }

        public static Ticket ConvertToEntity(TicketEditDTO dto) {
            return new Ticket
            {
                Id = dto.Id,
                Active = true,
                CategoryId = dto.CategoryId,
                Description = dto.Description,
                StatusId = dto.StatusId,
                Title = dto.Title,
                UserId = dto.UserId
            };
        }
    }
}
