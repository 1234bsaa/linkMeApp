using LinkMeAPI.Models;
using MediatR;
using MongoDB.Driver;

namespace LinkMeAPI.CQRS
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public DeleteProductCommandHandler(IMongoDatabase database)
        {
            _productsCollection = database.GetCollection<Product>("Products");
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, request.Id);
            var result = await _productsCollection.DeleteOneAsync(filter);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
