using LinkMeAPI.Models;
using MediatR;
using MongoDB.Driver;

namespace LinkMeAPI.CQRS
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public UpdateProductCommandHandler(IMongoDatabase database)
        {
            _productsCollection = database.GetCollection<Product>("Products");
        }

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, request.Id);

            var update = Builders<Product>.Update
                .Set(p => p.UPC, request.UPC)
                .Set(p => p.ProdName, request.ProdName)
                .Set(p => p.Stock, request.Stock)
                .Set(p => p.UnitPrice, request.UnitPrice)
                .Set(p => p.Mfgr, request.Mfgr)
                .Set(p => p.Model, request.Model);

            var options = new FindOneAndUpdateOptions<Product>
            {
                ReturnDocument = ReturnDocument.After
            };

            var updatedProduct = await _productsCollection.FindOneAndUpdateAsync(filter, update, options);
            return updatedProduct;
        }
    }

}
