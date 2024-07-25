namespace ScratchCardConsumer.Models
{
    public class ScratchCardViewModel
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Pin { get; set; }
        public bool IsUsed { get; set; }
        public bool IsPurchased { get; set; }
    }
}
