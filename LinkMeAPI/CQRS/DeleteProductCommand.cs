using MediatR;

namespace LinkMeAPI.CQRS
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}
