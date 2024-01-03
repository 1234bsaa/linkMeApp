namespace LinkMeAPI.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string UPC { get; set; }
        public string ProdName { get; set; }
        public string Mfgr { get; set; }
        public string Model { get; set; }
        public int Stock { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
