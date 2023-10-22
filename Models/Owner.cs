namespace Depuntzak_V2.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Password { get; set; }

        public int? MenuId { get; set; }
        public Menu? Menu { get; set; }
    }
}
