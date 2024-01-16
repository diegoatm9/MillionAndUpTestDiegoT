namespace MillionAndUp.Models
{
    public class PropertyImage
    {
        public int? IdPropertyImage { get; set; }
        public int? IdProperty{ get; set; }
        public byte[]? file { get; set; }
        public bool? Enabled{ get; set; }
    }
}
