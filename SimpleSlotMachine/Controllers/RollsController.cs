using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleSlotMachine.Services;

namespace SimpleSlotMachine.Controllers
{
    [Route("api/rolls/{sessionId}")]
    [ApiController]
    public class RollsController : ControllerBase
    {
        private readonly ISlotMachineService _slotMachineService;
        private readonly ISessionService _sessionService;

        public RollsController(ISlotMachineService slotMachineService)
        {
            _slotMachineService = slotMachineService;
        }

        [HttpPost("start")]
        public IActionResult StartRoll(string sessionId)
        {
            var result = _slotMachineService.StartRoll(sessionId);
            return Ok(result);
        }
    }
}
