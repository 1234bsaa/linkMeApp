using LinkMeAPI.Models;
using MediatR;
using MongoDB.Driver;

namespace LinkMeAPI.CQRS
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public GetProductByIdQueryHandler(IMongoDatabase database)
        {
            _productsCollection = database.GetCollection<Product>("Products");
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, request.Id);
            return await _productsCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
