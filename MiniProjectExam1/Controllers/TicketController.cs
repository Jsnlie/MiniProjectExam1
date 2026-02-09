using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using MiniProjectExam1.Models;
using MiniProjectExam1.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniProjectExam1.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _service;

        public TicketController(TicketService service)
        {
            _service = service;
        }

        // GET: api/<TicketController>
        [HttpGet("get-available-ticket")]
        public async Task<IActionResult> GetAvailableTickets([FromQuery] TicketModel request)
        {
            try
            {
                var result = await _service.GetAllAvailableTickets(request);

                return Ok(new
                {
                    tickets = result.Tickets,
                    totalTickets = result.TotalTickets
                });
            }
            catch (Exception ex)
            {
                return Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "An error occurred while processing your request"
                );
            }
        }

        // GET api/<TicketController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TicketController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
