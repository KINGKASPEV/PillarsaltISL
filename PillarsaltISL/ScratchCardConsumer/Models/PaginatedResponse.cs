using CRUD.Application.Utilities;

namespace ScratchCardConsumer.Models
{
    public class PaginatedResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public PageResult<T> Data { get; set; }
    }
}
