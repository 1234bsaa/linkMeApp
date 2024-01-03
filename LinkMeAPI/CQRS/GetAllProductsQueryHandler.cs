using LinkMeAPI.Models;
using MediatR;
using MongoDB.Driver;

namespace LinkMeAPI.CQRS
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public GetAllProductsQueryHandler(IMongoDatabase database)
        {
            _productsCollection = database.GetCollection<Product>("Products");
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productsCollection.Find(_ => true).ToListAsync();
        }
    }
}
