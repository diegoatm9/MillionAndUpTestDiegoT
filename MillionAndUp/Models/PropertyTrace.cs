namespace MillionAndUp.Models
{
    public class PropertyTrace
    {
        public int IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public int Value{ get; set; }
        public int Tax { get; set; }
        public int IdProperty { get; set; }
    }
}
