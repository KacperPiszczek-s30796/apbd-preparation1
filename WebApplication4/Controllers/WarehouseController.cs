using Microsoft.AspNetCore.Mvc;
using WebApplication4.contracts.requests;
using WebApplication4.entities;
using WebApplication4.Repositories.abstractions;
using WebApplication4.Services.abstractions;

namespace WebApplication4.Controllers;
[ApiController]
[Route("[controller]")]
public class WarehouseController: ControllerBase
{
    private readonly IProductService productService;

    public WarehouseController(IProductService productService)
    {
        this.productService = productService;
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> AddWarehouse([FromBody] CreateProductWarehouse warehouse,CancellationToken token = default)
    {
        int? result = await productService.RealizeOrder(warehouse, token);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }
    
}