using LinkMeAPI.CQRS;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LinkMeAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // CRUD operations endpoints here

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            var product = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, UpdateProductCommand command)
        {
            command.Id = id;
            var product = await _mediator.Send(command);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateProductAsync(UpdateProductCommand command)
        {
            var product = await _mediator.Send(command);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var command = new DeleteProductCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var query = new GetProductByIdQuery { Id = id };
            var product = await _mediator.Send(query);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            var products = await _mediator.Send(query);
            return Ok(products);
        }

    }
}
