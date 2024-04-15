using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleSlotMachine.Models;
using SimpleSlotMachine.Services;

namespace SimpleSlotMachine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService = new SessionService();

        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public IActionResult CreateSession()
        {
            try
            {
                var session = _sessionService.CreateSession();
                return Ok(session);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
