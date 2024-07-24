using CRUD.Application.Services.Interfaces;
using CRUD.Domain.Entities;
using CRUD.Persistence.Repositories.Interfaces;
using System.Net;

namespace CRUD.Application.Services.Implementations
{
    public class ScratchCardService : IScratchCardService
    {
        private readonly IGenericRepository<ScratchCard> _repository;

        public ScratchCardService(IGenericRepository<ScratchCard> repository)
        {
            _repository = repository;
        }

        public async Task<Response<IEnumerable<ScratchCard>>> ListCards()
        {
            try
            {
                var cards = await _repository.GetAll();
                if (cards is null || !cards.Any())
                {
                    return new Response<IEnumerable<ScratchCard>>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "No scratch cards found."
                    };
                }

                return new Response<IEnumerable<ScratchCard>>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Scratch cards retrieved successfully.",
                    Data = cards
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ScratchCard>>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while retrieving scratch cards: {ex.Message}"
                };
            }
        }

       public async Task<ResponseList<ScratchCard>> GenerateCards(int count)
        {
            try
            {
                var cards = new List<ScratchCard>();
                var random = new Random();

                for (int i = 0; i < count; i++)
                {
                    var serialNumber = Guid.NewGuid().ToString("N"); 
                    var pin = random.Next(1000, 9999).ToString(); 

                    var card = new ScratchCard
                    {
                        SerialNumber = serialNumber,
                        Pin = pin,
                        IsUsed = false
                    };

                    cards.Add(card);
                }

                foreach (var card in cards)
                {
                    await _repository.Add(card);
                }

                await _repository.SaveChangesAsync();

                return new ResponseList<ScratchCard>
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = "Scratch cards generated successfully.",
                    Data = cards
                };
            }
            catch (Exception ex)
            {
                return new ResponseList<ScratchCard>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while generating scratch cards: {ex.Message}",
                    Data = Enumerable.Empty<ScratchCard>() 
                };
            }
        }


        public async Task<Response<ScratchCard>> PurchaseCard(string serialNumber)
        {
            try
            {
                var card = (await _repository.GetAll()).FirstOrDefault(c => c.SerialNumber == serialNumber && !c.IsUsed);
                if (card == null)
                {
                    return new Response<ScratchCard>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "Scratch card not found or already used."
                    };
                }

                return new Response<ScratchCard>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Scratch card purchased successfully.",
                    Data = card
                };
            }
            catch (Exception ex)
            {
                return new Response<ScratchCard>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while purchasing the scratch card: {ex.Message}"
                };
            }
        }

        public async Task<Response<ScratchCard>> UseCard(string serialNumber, string pin)
        {
            try
            {
                var card = (await _repository.GetAll()).FirstOrDefault(c => c.SerialNumber == serialNumber && c.Pin == pin && !c.IsUsed);
                if (card == null)
                {
                    return new Response<ScratchCard>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "Scratch card not found, PIN incorrect, or card already used."
                    };
                }

                card.IsUsed = true;
                await _repository.Update(card);
                await _repository.SaveChangesAsync();

                return new Response<ScratchCard>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Scratch card used successfully.",
                    Data = card
                };
            }
            catch (Exception ex)
            {
                return new Response<ScratchCard>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while using the scratch card: {ex.Message}"
                };
            }
        }
    }
}
