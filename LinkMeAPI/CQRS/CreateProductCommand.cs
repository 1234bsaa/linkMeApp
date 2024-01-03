using LinkMeAPI.Models;
using MediatR;

namespace LinkMeAPI.CQRS
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string UPC { get; set; }
        public string ProdName { get; set; }
        public string Mfgr { get; set; }
        public string Model { get; set; }
        public int Stock { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
