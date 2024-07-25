namespace ScratchCardConsumer.Models
{
    public class GenerateCardsResponseViewModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<ScratchCardViewModel> Data { get; set; } = new List<ScratchCardViewModel>();
    }
}
