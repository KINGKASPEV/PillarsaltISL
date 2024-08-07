using CRUD.Application;
using CRUD.Application.Utilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ScratchCardConsumer.Models;

namespace ScratchCardConsumer.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        public ApiService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
            _httpClient.BaseAddress = new Uri(_apiSettings.BaseUrl);
        }

        private async Task<string> GetErrorResponseAsync(HttpResponseMessage response)
        {
            var errorJson = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<Response<ScratchCardViewModel>>(errorJson);
            return errorResponse != null ? $"Error: {errorResponse.Message}" : "An unexpected error occurred.";
        }

        public async Task<PaginatedResponse<IEnumerable<ScratchCardViewModel>>> GetPaginatedCardsAsync(int page, int perPage)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/scratchcard/list?page={page}&perPage={perPage}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await GetErrorResponseAsync(response);
                    return new PaginatedResponse<IEnumerable<ScratchCardViewModel>>
                    {
                        StatusCode = (int)response.StatusCode,
                        Message = errorMessage,
                        Data = new PageResult<IEnumerable<ScratchCardViewModel>>
                        {
                            Data = Array.Empty<ScratchCardViewModel>(),
                            CurrentPage = page,
                            PerPage = perPage,
                            TotalPageCount = 0,
                            TotalCount = 0
                        }
                    };
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    return new PaginatedResponse<IEnumerable<ScratchCardViewModel>>
                    {
                        StatusCode = (int)response.StatusCode,
                        Message = "No data available.",
                        Data = new PageResult<IEnumerable<ScratchCardViewModel>>
                        {
                            Data = Array.Empty<ScratchCardViewModel>(),
                            CurrentPage = page,
                            PerPage = perPage,
                            TotalPageCount = 0,
                            TotalCount = 0
                        }
                    };
                }

                return JsonConvert.DeserializeObject<PaginatedResponse<IEnumerable<ScratchCardViewModel>>>(jsonString);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return new PaginatedResponse<IEnumerable<ScratchCardViewModel>>
                {
                    StatusCode = 500,
                    Message = $"An error occurred while fetching paginated data: {ex.Message}",
                    Data = new PageResult<IEnumerable<ScratchCardViewModel>>
                    {
                        Data = Array.Empty<ScratchCardViewModel>(),
                        CurrentPage = page,
                        PerPage = perPage,
                        TotalPageCount = 0,
                        TotalCount = 0
                    }
                };
            }
        }

        public async Task<string> GetDataAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/scratchcard");

                if (!response.IsSuccessStatusCode)
                {
                    return await GetErrorResponseAsync(response);
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                return string.IsNullOrWhiteSpace(jsonString) ? "No data available." : jsonString;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return $"An error occurred while fetching data: {ex.Message}";
            }
        }

        public async Task<GenerateCardsResponseViewModel> GenerateCardsAsync(int count)
        {
            try
            {
                var response = await _httpClient.PostAsync($"api/scratchcard/generate?count={count}", null);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GenerateCardsResponseViewModel>(jsonString);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return new GenerateCardsResponseViewModel
                {
                    StatusCode = 500,
                    Message = $"An error occurred while generating cards: {ex.Message}"
                };
            }
        }

        public async Task<PurchaseCardResponseViewModel> PurchaseCardAsync(string serialNumber)
        {
            try
            {
                var response = await _httpClient.PostAsync($"api/scratchcard/purchase?serialNumber={serialNumber}", null);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await GetErrorResponseAsync(response);
                    return new PurchaseCardResponseViewModel
                    {
                        StatusCode = (int)response.StatusCode,
                        Message = errorMessage,
                        Card = null
                    };
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<Response<ScratchCardViewModel>>(jsonString);

                return new PurchaseCardResponseViewModel
                {
                    StatusCode = apiResponse.StatusCode,
                    Message = apiResponse.Message,
                    Card = apiResponse.Data != null ? new CardDetail
                    {
                        SerialNumber = apiResponse.Data.SerialNumber,
                        Pin = apiResponse.Data.Pin,
                        IsUsed = apiResponse.Data.IsUsed,
                        IsPurchased = apiResponse.Data.IsPurchased
                    } : null
                };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return new PurchaseCardResponseViewModel
                {
                    StatusCode = 500,
                    Message = $"An error occurred while purchasing the card: {ex.Message}",
                    Card = null
                };
            }
        }

        public async Task<UseCardResponseViewModel> UseCardAsync(string serialNumber, string pin)
        {
            try
            {
                var response = await _httpClient.PostAsync($"api/scratchcard/use?serialNumber={serialNumber}&pin={pin}", null);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await GetErrorResponseAsync(response);
                    return new UseCardResponseViewModel
                    {
                        StatusCode = (int)response.StatusCode,
                        Message = errorMessage,
                        Card = null
                    };
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<Response<ScratchCardViewModel>>(jsonString);

                return new UseCardResponseViewModel
                {
                    StatusCode = apiResponse.StatusCode,
                    Message = apiResponse.Message,
                    Card = apiResponse.Data != null ? new CardDetail
                    {
                        SerialNumber = apiResponse.Data.SerialNumber,
                        Pin = apiResponse.Data.Pin,
                        IsUsed = apiResponse.Data.IsUsed,
                        IsPurchased = apiResponse.Data.IsPurchased
                    } : null
                };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
                return new UseCardResponseViewModel
                {
                    StatusCode = 500,
                    Message = $"An error occurred while using the card: {ex.Message}",
                    Card = null
                };
            }
        }
    }
}
