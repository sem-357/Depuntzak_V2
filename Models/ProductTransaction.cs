namespace Depuntzak_V2.Models
{
    public class ProductTransaction
    {
        public int Id { get; set; }


        public int TransactionId { get; set; }
        public Transaction? Transaction { get; set; }


        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
