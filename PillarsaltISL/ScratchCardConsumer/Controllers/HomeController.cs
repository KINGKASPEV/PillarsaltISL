using Microsoft.AspNetCore.Mvc;
using ScratchCardConsumer.Models;
using ScratchCardConsumer.Services;
using System.Diagnostics;

namespace ScratchCardConsumer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiService _apiService;

        public HomeController(ILogger<HomeController> logger, ApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _apiService.GetDataAsync();
            return View((object)data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GenerateCards()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateCards(int count)
        {
            try
            {
                var viewModel = await _apiService.GenerateCardsAsync(count);
                return View(viewModel);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error generating cards.");
                return Content($"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult PurchaseCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PurchaseCard(string serialNumber)
        {
            try
            {
                var response = await _apiService.PurchaseCardAsync(serialNumber);
                if (response.StatusCode == 404)
                {
                    ModelState.AddModelError("", response.Message);
                }
                return View(response);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error purchasing card.");
                return Content($"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult UseCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UseCard(string serialNumber, string pin)
        {
            try
            {
                var viewModel = await _apiService.UseCardAsync(serialNumber, pin);
                if (viewModel.StatusCode == 404)
                {
                    ModelState.AddModelError("", viewModel.Message);
                }

                return View(viewModel);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error using card.");
                return Content($"An error occurred: {ex.Message}");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
