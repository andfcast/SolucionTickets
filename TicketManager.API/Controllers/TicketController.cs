using Microsoft.AspNetCore.Mvc;
using TicketManager.Application.DTO.Entities;
using TicketManager.Application.DTO.RequestResponse;
using TicketManager.Application.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ILogger<TicketController> _logger;
        public TicketController(ITicketService ticketService, ILogger<TicketController> logger)
        {
            _ticketService = ticketService;
            _logger = logger;
        }
        
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            _logger.LogTrace("Start Ticket List service");
            ResponseDTO response =await _ticketService.GetAll();
            if (!response.IsValid)
            {
                _logger.LogWarning("GetAll: " + response.Message);
                return NotFound(response);
            }
            _logger.LogInformation("Ticket List Service executed");
            return Ok(response);
        }
        
        [HttpGet("Details/{id:int}")]        
        public async Task<ActionResult> GetById(int id)
        {
            _logger.LogTrace("Start Ticket Detail service");
            ResponseDTO response = await _ticketService.GetById(id);
            if (!response.IsValid)
            {
                _logger.LogWarning("GetById: " + response.Message);
                return NotFound(response);
            }
            _logger.LogInformation("Ticket Detail Service executed");
            return Ok(response);
        }

        
        [HttpPost]
        [Route("CreateNew")]
        public async Task<IActionResult> CreateNew([FromBody] TicketEditDTO dto)
        {
            _logger.LogTrace("Start Ticket Creation service");
            ResponseDTO response = await _ticketService.CreateNew(dto);
            if (!response.IsValid)
            {
                _logger.LogError("CreateNew: " + response.Message);
                return BadRequest(response);
            }
            _logger.LogInformation("Ticket Creation Service executed");
            return Ok(response);
        }

        [HttpPost]
        [Route("AddComment")]
        public async Task<IActionResult> AddComment([FromBody] TicketLogDTO dto)
        {
            _logger.LogTrace("Start Add Comment to Ticket service");
            ResponseDTO response = await _ticketService.InsertComment(dto);
            if (!response.IsValid)
            {
                _logger.LogError("AddComment: " + response.Message);
                return BadRequest(response);
            }
            _logger.LogInformation("Ticket Comment Service executed");
            return Ok(response);
        }


        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] TicketEditDTO dto)
        {
            _logger.LogTrace("Start Ticket Update service");
            ResponseDTO response = await _ticketService.Update(dto);
            if (!response.IsValid)
            {
                _logger.LogError("Update: " + response.Message);
                return BadRequest(response);
            }
            _logger.LogInformation("Ticket Update Service executed");
            return Ok(response);
        }

        
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogTrace("Start Ticket Delete service");
            ResponseDTO response = await _ticketService.Delete(id);
            if (!response.IsValid)
            {
                _logger.LogError("Delete: " + response.Message);
                return BadRequest(response);
            }
            _logger.LogInformation("Ticket Delete Service executed");
            return Ok(response);
        }
    }
}
