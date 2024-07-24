namespace CRUD.Domain.Entities
{
    public class ScratchCard
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Pin { get; set; }
        public bool IsUsed { get; set; }
    }
}
