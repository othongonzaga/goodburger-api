using goodburger_api.Application.DTOs;
using goodburger_api.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace goodburger_api.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<OrderResponse>> Create(
        [FromBody] UpsertOrderRequest request,
        CancellationToken cancellationToken)
    {
        var response = await orderService.CreateAsync(request, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = response.Id },
            response);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<OrderResponse>>> GetAll(
        CancellationToken cancellationToken)
    {
        var response = await orderService.GetAllAsync(cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderResponse>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var response = await orderService.GetByIdAsync(id, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<OrderResponse>> Update(
        Guid id,
        [FromBody] UpsertOrderRequest request,
        CancellationToken cancellationToken)
    {
        var response = await orderService.UpdateAsync(id, request, cancellationToken);
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        await orderService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}