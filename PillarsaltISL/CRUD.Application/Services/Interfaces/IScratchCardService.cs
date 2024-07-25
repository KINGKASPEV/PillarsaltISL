using CRUD.Application.Utilities;
using CRUD.Domain.Entities;

namespace CRUD.Application.Services.Interfaces
{
    public interface IScratchCardService
    {
        Task<Response<IEnumerable<ScratchCard>>> ListCards();
        Task<Response<PageResult<IEnumerable<ScratchCard>>>> ListAllCard(int page, int perPage);
        Task<ResponseList<ScratchCard>> GenerateCards(int count);
        Task<Response<ScratchCard>> PurchaseCard(string serialNumber);
        Task<Response<ScratchCard>> UseCard(string serialNumber, string pin);
    }
}
