using CRUD.Application.DTOs;
using CRUD.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _itemService.GetAllItems();
            if (response.StatusCode != 200) return StatusCode(response.StatusCode, response.Message);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _itemService.GetItemById(id);
            if (response.StatusCode != 200) return StatusCode(response.StatusCode, response.Message);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ItemDto ItemDto)
        {
            var response = await _itemService.AddItem(ItemDto);
            if (response.StatusCode != 201) return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ItemDto ItemDto)
        {
            var response = await _itemService.UpdateItem(id, ItemDto);
            if (response.StatusCode != 200) return StatusCode(response.StatusCode, response.Message);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _itemService.DeleteItem(id);
            if (response.StatusCode != 200) return StatusCode(response.StatusCode, response.Message);
            return NoContent();
        }
    }
}
