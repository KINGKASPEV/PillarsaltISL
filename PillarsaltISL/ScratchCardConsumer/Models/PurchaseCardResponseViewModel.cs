namespace ScratchCardConsumer.Models
{
    public class PurchaseCardResponseViewModel
    {
         public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public CardDetail Card { get; set; }
    }
}
