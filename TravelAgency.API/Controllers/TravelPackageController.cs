using Microsoft.AspNetCore.Mvc;
using TravelAgency.Core.Interfaces;
using TravelAgency.Core.Models;

[Route("api/[controller]")]
[ApiController]
public class TravelPackageController : ControllerBase
{
    private readonly ITravelPackageService _packageService;

    public TravelPackageController(ITravelPackageService packageService)
    {
        _packageService = packageService;
    }

    [HttpGet("travel-packages")]
    public async Task<IActionResult> GetAll() => Ok(await _packageService.GetAllAsync());

    [HttpPost("create-travel-package")]
    public async Task<IActionResult> Create([FromBody] TravelPackage package)
    {
        await _packageService.AddAsync(package);
        return CreatedAtAction(nameof(GetAll), new { id = package.Id }, package);
    }
}