namespace Depuntzak_V2.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? Price { get; set; }

        public bool Discount { get; set; }

        public ICollection<ProductTransaction>? ProductTransactions { get; set; }

        public int MenuId { get; set; }
        public Menu? Menu { get; set; }
    }
}
