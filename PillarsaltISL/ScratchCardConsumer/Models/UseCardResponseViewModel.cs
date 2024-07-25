namespace ScratchCardConsumer.Models
{
    public class UseCardResponseViewModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public CardDetail Card { get; set; }
    }
}
