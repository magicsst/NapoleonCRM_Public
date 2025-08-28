using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using NapoleonCRM.Data;
using NapoleonCRM.Shared.Models;

namespace NapoleonCRM.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[EnableRateLimiting("Fixed")]
public class OrderController(ApplicationDbContext _ctx, ILogger<OrderController> _logger) : ControllerBase
{
    private readonly ILogger<OrderController> logger = _logger;
    private readonly ApplicationDbContext ctx = _ctx;

    [HttpGet("")]
    [EnableQuery]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<IQueryable<Order>> Get()
    {
        return Ok(ctx.Order.Include(x => x.Customer).Include(x => x.OrderDetails).ThenInclude(x => x.Product));
    }

    [HttpGet("{key}")]
    [EnableQuery]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Order>> GetAsync(long key)
    {
        var order = await ctx.Order.Include(x => x.Customer).Include(x => x.OrderDetails).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id == key);

        if (order == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(order);
        }
    }

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Order>> PostAsync(Order order)
    {
        var record = await ctx.Order.FindAsync(order.Id);
        if (record != null)
        {
            return Conflict();
        }

        var details = order.OrderDetails;
        order.OrderDetails = null;

        await ctx.Order.AddAsync(order);

        if (details != null)
        {
            foreach (var detail in details)
            {
                detail.Product = detail.ProductId.HasValue ? await ctx.Product.FindAsync(detail.ProductId.Value) : null;
                order.OrderDetails ??= [];
                order.OrderDetails.Add(detail);
            }
        }

        await ctx.SaveChangesAsync();

        return Created($"/order/{order.Id}", order);
    }

    [HttpPut("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Order>> PutAsync(long key, Order update)
    {
        var order = await ctx.Order.Include(x => x.OrderDetails).FirstOrDefaultAsync(x => x.Id == key);

        if (order == null)
        {
            return NotFound();
        }

        ctx.Entry(order).CurrentValues.SetValues(update);

        if (update.OrderDetails != null)
        {
            order.OrderDetails ??= [];
            order.OrderDetails.Clear();
            foreach (var detail in update.OrderDetails)
            {
                detail.Product = detail.ProductId.HasValue ? await ctx.Product.FindAsync(detail.ProductId.Value) : null;
                order.OrderDetails.Add(detail);
            }
        }

        await ctx.SaveChangesAsync();

        return Ok(order);
    }

    [HttpPatch("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Order>> PatchAsync(long key, Delta<Order> delta)
    {
        var order = await ctx.Order.Include(x => x.Customer).Include(x => x.OrderDetails).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id == key);

        if (order == null)
        {
            return NotFound();
        }

        delta.Patch(order);

        await ctx.SaveChangesAsync();

        return Ok(order);
    }

    [HttpDelete("{key}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(long key)
    {
        var order = await ctx.Order.Include(x => x.OrderDetails).FirstOrDefaultAsync(x => x.Id == key);

        if (order != null)
        {
            ctx.Order.Remove(order);
            await ctx.SaveChangesAsync();
        }

        return NoContent();
    }
}
