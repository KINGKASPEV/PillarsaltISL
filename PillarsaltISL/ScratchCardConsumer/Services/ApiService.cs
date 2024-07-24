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
    }


}
