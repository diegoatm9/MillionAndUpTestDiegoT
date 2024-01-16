namespace MillionAndUp.Models
{
    public class Property
    {
        public int IdProperty { get; set; }
        public string? Name{ get; set; }
        public string? Address { get; set; }
        public int? Price { get; set; }
        public int? CodeInternal { get; set; }
        public int? Year { get; set; }
        public int? IdOwner { get; set; }
    }
}
