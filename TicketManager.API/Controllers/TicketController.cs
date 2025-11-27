using Microsoft.AspNetCore.Mvc;
using TicketManager.Application.DTO.Entities;
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
            return Ok(new string[] { "value1", "value2" });
        }

        // GET api/<TicketController>/5
        [HttpGet("Details/{id:int}")]        
        public async Task<ActionResult> GetById(int id)
        {
            return Ok("value");
        }

        
        [HttpPost]
        [Route("CreateNew")]
        public async Task<IActionResult> CreateNew([FromBody] TicketDTO dto)
        {
            return Ok("Todo bien");
        }

        // PUT api/<TicketController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
