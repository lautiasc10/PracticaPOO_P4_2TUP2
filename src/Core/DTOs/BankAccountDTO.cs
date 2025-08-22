namespace Core.DTOs
{
    public class BankAccountDTO
    {
        public string Owner { get; set; } = string.Empty; // obligatorio
        public decimal InitialBalance { get; set; }       // obligatorio
    }
}
