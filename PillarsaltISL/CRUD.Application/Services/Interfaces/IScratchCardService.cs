using CRUD.Domain.Entities;

namespace CRUD.Application.Services.Interfaces
{
    public interface IScratchCardService
    {
        Task<Response<IEnumerable<ScratchCard>>> ListCards();
        Task<ResponseList<ScratchCard>> GenerateCards(int count);
        Task<Response<ScratchCard>> PurchaseCard(string serialNumber);
        Task<Response<ScratchCard>> UseCard(string serialNumber, string pin);
    }
}
