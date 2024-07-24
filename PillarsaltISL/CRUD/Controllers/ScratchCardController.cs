using CRUD.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScratchCardController : ControllerBase
    {
        private readonly IScratchCardService _scratchCardService;

        public ScratchCardController(IScratchCardService scratchCardService)
        {
            _scratchCardService = scratchCardService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateCards([FromQuery] int count)
        {
            var response = await _scratchCardService.GenerateCards(count);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> ListCards()
        {
            var response = await _scratchCardService.ListCards();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseCard([FromQuery] string serialNumber)
        {
            var response = await _scratchCardService.PurchaseCard(serialNumber);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("use")]
        public async Task<IActionResult> UseCard([FromQuery] string serialNumber, [FromQuery] string pin)
        {
            var response = await _scratchCardService.UseCard(serialNumber, pin);
            return StatusCode(response.StatusCode, response);
        }
    }
}
