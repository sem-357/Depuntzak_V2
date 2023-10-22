namespace Depuntzak_V2.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Password { get; set; }

        public ICollection<Transaction>? Transactions { get; set; }
    }
}
