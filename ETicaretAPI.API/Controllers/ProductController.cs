using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;

        public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            await _productWriteRepository.AddRangeAsync(new()
                {
                    new() {Id = Guid.NewGuid(), Name = "Product 1", Price = 100, CreatedDate = DateTime.Now, Stock = 10},
                    new() {Id = Guid.NewGuid(), Name = "Product 2", Price = 200, CreatedDate = DateTime.Now, Stock = 20},
                    new() {Id = Guid.NewGuid(), Name = "Product 3", Price = 300, CreatedDate = DateTime.Now, Stock = 130},
                });
            await _productWriteRepository.SaveAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
           Product product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }

    }
}
