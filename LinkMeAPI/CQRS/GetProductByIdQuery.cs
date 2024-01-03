using LinkMeAPI.Models;
using MediatR;

namespace LinkMeAPI.CQRS
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public string Id { get; set; }
    }

}
