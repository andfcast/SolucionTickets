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

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: api/<TicketController>
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            ResponseDTO response =await _ticketService.GetAll();
            if (!response.IsValid)
                return NotFound(response);
            return Ok(response);
        }

        // GET api/<TicketController>/5
        [HttpGet("Details/{id:int}")]        
        public async Task<ActionResult> GetById(int id)
        {
            ResponseDTO response = await _ticketService.GetById(id);
            if (!response.IsValid)
                return NotFound(response);
            return Ok(response);
        }

        
        [HttpPost]
        [Route("CreateNew")]
        public async Task<IActionResult> CreateNew([FromBody] TicketEditDTO dto)
        {
            ResponseDTO response = await _ticketService.CreateNew(dto);
            if (!response.IsValid)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost]
        [Route("AddComment")]
        public async Task<IActionResult> AddComment([FromBody] TicketLogDTO dto)
        {
            ResponseDTO response = await _ticketService.InsertComment(dto);
            if (!response.IsValid)
                return BadRequest(response);
            return Ok(response);
        }


        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] TicketEditDTO dto)
        {
            ResponseDTO response = await _ticketService.Update(dto);
            if (!response.IsValid)
                return BadRequest(response);
            return Ok(response);
        }

        
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseDTO response = await _ticketService.Delete(id);
            if (!response.IsValid)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
