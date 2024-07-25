using CRUD.Application;
using CRUD.Domain.Entities;
using Newtonsoft.Json;
using ScratchCardConsumer.Models;

namespace ScratchCardConsumer.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7222");
        }

        public async Task<string> GetDataAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/scratchcard");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }


        public async Task<GenerateCardsResponseViewModel> GenerateCardsAsync(int count)
        {
            var response = await _httpClient.PostAsync($"api/scratchcard/generate?count={count}", null);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GenerateCardsResponseViewModel>(jsonString);
        }


        public async Task<PurchaseCardResponseViewModel> PurchaseCardAsync(string serialNumber)
        {
            var response = await _httpClient.PostAsync($"api/scratchcard/purchase?serialNumber={serialNumber}", null);

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<Response<ScratchCardViewModel>>(errorJson);
                return new PurchaseCardResponseViewModel
                {
                    StatusCode = errorResponse.StatusCode,
                    Message = errorResponse.Message,
                    Card = null
                };
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<Response<ScratchCardViewModel>>(jsonString);

            return new PurchaseCardResponseViewModel
            {
                StatusCode = apiResponse.StatusCode,
                Message = apiResponse.Message,
                Card = apiResponse.Data != null
                    ? new CardDetail
                    {
                        SerialNumber = apiResponse.Data.SerialNumber,
                        Pin = apiResponse.Data.Pin,
                        IsUsed = apiResponse.Data.IsUsed,
                        IsPurchased = apiResponse.Data.IsPurchased
                    }
                    : null
            };
        }


        public async Task<UseCardResponseViewModel> UseCardAsync(string serialNumber, string pin)
        {
            var response = await _httpClient.PostAsync($"api/scratchcard/use?serialNumber={serialNumber}&pin={pin}", null);
            if (!response.IsSuccessStatusCode)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<Response<ScratchCardViewModel>>(errorJson);
                return new UseCardResponseViewModel
                {
                    StatusCode = errorResponse.StatusCode,
                    Message = errorResponse.Message,
                    Card = null
                };
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<Response<ScratchCardViewModel>>(jsonString);

            return new UseCardResponseViewModel
            {
                StatusCode = apiResponse.StatusCode,
                Message = apiResponse.Message,
                Card = apiResponse.Data != null
                    ? new CardDetail
                    {
                        SerialNumber = apiResponse.Data.SerialNumber,
                        Pin = apiResponse.Data.Pin,
                        IsUsed = apiResponse.Data.IsUsed,
                        IsPurchased = apiResponse.Data.IsPurchased
                    }
                    : null
            };
        }
    }
}
