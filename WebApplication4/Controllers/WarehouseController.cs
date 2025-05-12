using Microsoft.AspNetCore.Mvc;
using WebApplication4.contracts.requests;
using WebApplication4.entities;

namespace WebApplication4.Controllers;
[ApiController]
[Route("[controller]")]
public class WarehouseController: ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> AddWarehouse([FromBody] CreateProductWarehouse warehouse,CancellationToken token = default)
    {
        return Ok(warehouse);
    }
    
}