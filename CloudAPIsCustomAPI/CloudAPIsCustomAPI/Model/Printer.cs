namespace Model
{
    public class Printer
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public string PrintVolume { get; set; }
        public string PrintMethod { get; set; }
        public int Price { get; set; }
        public Brand Brand { get; set; }
    }
}
