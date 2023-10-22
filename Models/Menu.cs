namespace Depuntzak_V2.Models
{
    public class Menu
    {
        
        public int Id { get; set; }

        public ICollection<Product>? Products { get; set; }

        public Owner? Owner { get; set; }
    }
}
