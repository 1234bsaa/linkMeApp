using LinkMeAPI.Models;
using MediatR;

namespace LinkMeAPI.CQRS
{
    public class GetAllProductsQuery : IRequest<List<Product>>
    {
    }
}
