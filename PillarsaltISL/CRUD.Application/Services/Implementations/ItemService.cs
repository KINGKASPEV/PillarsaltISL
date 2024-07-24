using CRUD.Application.DTOs;
using CRUD.Application.Services.Interfaces;
using CRUD.Model;
using CRUD.Persistence.Repositories.Interfaces;
using System.Net;

namespace CRUD.Application.Services.Implementations
{
    public class ItemService : IItemService
    {
        private readonly IGenericRepository<Item> _repository;

        public ItemService(IGenericRepository<Item> repository)
        {
            _repository = repository;
        }

        public async Task<Response<IEnumerable<Item>>> GetAllItems()
        {
            try
            {
                var items = await _repository.GetAll();

                if (items is null || !items.Any())
                {
                    return new Response<IEnumerable<Item>>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "No items found."
                    };
                }

                return new Response<IEnumerable<Item>>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Items retrieved successfully.",
                    Data = items
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Item>>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while retrieving items: {ex.Message}"
                };
            }
        }

        public async Task<Response<Item>> GetItemById(int id)
        {
            try
            {
                var item = await _repository.GetById(id);
                if (item == null)
                {
                    return new Response<Item>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "Item not found."
                    };
                }

                return new Response<Item>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Item retrieved successfully.",
                    Data = item
                };
            }
            catch (Exception ex)
            {
                return new Response<Item>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while retrieving the item: {ex.Message}"
                };
            }
        }

        public async Task<Response<Item>> AddItem(ItemDto ItemDto)
        {
            try
            {
                if (ItemDto is null)
                {
                    return new Response<Item>
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = "Item data is required."
                    };
                }

                var item = new Item
                {
                    Name = ItemDto.Name,
                    Description = ItemDto.Description
                };

                await _repository.Add(item);
                await _repository.SaveChangesAsync();
                return new Response<Item>
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = "Item added successfully.",
                    Data = item
                };
            }
            catch (Exception ex)
            {
                return new Response<Item>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while adding the item: {ex.Message}"
                };
            }
        }

        public async Task<Response<Item>> UpdateItem(int Id, ItemDto ItemDto)
        {
            try
            {
                var existingItem = await _repository.GetById(Id);
                if (existingItem == null)
                {
                    return new Response<Item>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "Item not found."
                    };
                }

                existingItem.Name = ItemDto.Name;
                existingItem.Description = ItemDto.Description;

                await _repository.Update(existingItem);
                await _repository.SaveChangesAsync();

                return new Response<Item>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Item updated successfully.",
                    Data = existingItem
                };
            }
            catch (Exception ex)
            {
                return new Response<Item>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while updating the item: {ex.Message}"
                };
            }
        }


        public async Task<Response<string>> DeleteItem(int id)
        {
            try
            {
                var existingItem = await _repository.GetById(id);
                if (existingItem == null)
                {
                    return new Response<string>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "Item not found."
                    };
                }

                await _repository.Delete(id);
                await _repository.SaveChangesAsync();

                return new Response<string>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Item deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while deleting the item: {ex.Message}"
                };
            }
        }
    }
}
