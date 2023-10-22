namespace Depuntzak_V2.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int? Subtotal { get; set; }


        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public ICollection<ProductTransaction>? ProductTransactions { get; set; }

    }
}
