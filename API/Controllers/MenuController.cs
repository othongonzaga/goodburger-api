using goodburger_api.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace goodburger_api.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class MenuController(IMenuCatalog menuCatalog) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var response = menuCatalog.GetMenu();
        return Ok(response);
    }
}