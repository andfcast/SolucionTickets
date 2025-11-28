using Moq;
using TicketManager.API;
using TicketManager.Application;
using TicketManager.Application.DTO.Entities;
using TicketManager.Application.DTO.RequestResponse;
using TicketManager.Application.Services.Implementation;
using TicketManager.Application.Services.Interfaces;
using TicketManager.Domain.Entities;
using TicketManager.Domain.Repositories;
namespace TicketManager.Tests
{
    public class TicketServiceTest
    {
        private readonly Mock<ITicketRepository> _ticketRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly Mock<IStatusRepository> _statusRepository;
        private readonly ITicketService _service;

        public TicketServiceTest() {
            _ticketRepository = new Mock<ITicketRepository>();
            _userRepository = new Mock<IUserRepository>();
            _categoryRepository = new Mock<ICategoryRepository>();
            _statusRepository = new Mock<IStatusRepository>();
            _service = new TicketService(_ticketRepository.Object, _userRepository.Object, _categoryRepository.Object, _statusRepository.Object);
        }

        [Fact]
        public async Task IngresoTicket_Exitoso()
        {
            ResponseDTO response = new ResponseDTO();
            User curUser = new User
            {
                Id = 1,
                FullName = "Pepito Perez",
                UserName = "pperez",
                Active = true
            };
            TicketEditDTO newDto = new TicketEditDTO
            {
                UserName = "pperez",
                CategoryId = 1,
                StatusId = 1,
                Description = "Prueba 1",
                Title = "Prueba 1",
            };
            Ticket newTicket = new Ticket
            {
                UserId = 1,
                Active = true,
                CategoryId = 1,
                StatusId = 1,
                Title = "Prueba 1",
                Description = "Prueba 1",
                CreationDate = DateTime.Now                
            };            
            _statusRepository.Setup(x => x.IsValidStatus(newDto.StatusId)).ReturnsAsync(true);
            _categoryRepository.Setup(x => x.IsValidCategory(newDto.CategoryId)).ReturnsAsync(true);
            _userRepository.Setup(x => x.IsValidUser(0,newDto.UserName)).ReturnsAsync(true);
            _userRepository.Setup(x => x.GetUser(0, newDto.UserName)).ReturnsAsync(curUser);
            _ticketRepository.Setup(x => x.Insert(newTicket)).ReturnsAsync(1).Verifiable();
            response = await _service.CreateNew(newDto);
            Assert.NotNull(response);
            Assert.True(response.IsValid);            
            _ticketRepository.Verify(x => x.Insert(It.IsAny<Ticket>()), Times.Once);
        }

        [Fact]
        public async Task IngresoTicket_Fallido()
        {
            ResponseDTO response = new ResponseDTO();
            User curUser = new User
            {
                Id = 1,
                FullName = "Pepito Perez",
                UserName = "pperez",
                Active = true
            };
            TicketEditDTO newDto = new TicketEditDTO
            {
                UserName = "pperez",
                CategoryId = 1,
                StatusId = 4,
                Description = "Prueba 1",
                Title = "Prueba 1",
            };
            Ticket newTicket = new Ticket
            {
                UserId = 1,
                Active = true,
                CategoryId = 1,
                StatusId = 4,
                Title = "Prueba 1",
                Description = "Prueba 1",
                CreationDate = DateTime.Now
            };
            _statusRepository.Setup(x => x.IsValidStatus(newDto.StatusId)).ReturnsAsync(false);
            _categoryRepository.Setup(x => x.IsValidCategory(newDto.CategoryId)).ReturnsAsync(true);
            _userRepository.Setup(x => x.IsValidUser(0, newDto.UserName)).ReturnsAsync(true);
            _userRepository.Setup(x => x.GetUser(0, newDto.UserName)).ReturnsAsync(curUser);
            _ticketRepository.Setup(x => x.Insert(newTicket)).ReturnsAsync(1).Verifiable();
            response = await _service.CreateNew(newDto);
            Assert.NotNull(response);
            Assert.False(response.IsValid);
            Assert.Equal("Invalid status", response.Message);
        }
    }    
}