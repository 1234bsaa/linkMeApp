using LinkMeAPI.Models;
using MediatR;
using MongoDB.Driver;

namespace LinkMeAPI.CQRS
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public CreateProductCommandHandler(IMongoDatabase database)
        {
            _productsCollection = database.GetCollection<Product>("Products");
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                UPC = request.UPC,
                ProdName = request.ProdName,
                Stock = request.Stock,
                UnitPrice = request.UnitPrice,
                Mfgr= request.Mfgr,
                Model= request.Model,
            };
            // Set Id to a new GUID if it's null
            if (string.IsNullOrEmpty(product.Id))
            {
                product.Id = Guid.NewGuid().ToString();
            }
            await _productsCollection.InsertOneAsync(product);
            return product;
        }
    }
}
