using CRUD.Application.DTOs;
using CRUD.Model;

namespace CRUD.Application.Services.Interfaces
{
    public interface IItemService
    {
        Task<Response<IEnumerable<Item>>> GetAllItems();
        Task<Response<Item>> GetItemById(int id);
        Task<Response<Item>> AddItem(ItemDto ItemDto);
        Task<Response<Item>> UpdateItem(int Id, ItemDto ItemDto);
        Task<Response<string>> DeleteItem(int id);
    }
}
