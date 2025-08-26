using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using NapoleonCRM.Data;
using NapoleonCRM.Shared.Models;

namespace NapoleonCRM.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[EnableRateLimiting("Fixed")]
public class CategoryFirstController(ApplicationDbContext _ctx, ILogger<CategoryFirstController> _logger) : ControllerBase
{
    private readonly ILogger<CategoryFirstController> logger = _logger;
    private readonly ApplicationDbContext ctx = _ctx;

    [HttpGet("")]
    [EnableQuery]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<IQueryable<CategoryFirst>> Get()
    {
        return Ok(ctx.CategoryFirst);
    }

    [HttpGet("{key}")]
    [EnableQuery]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryFirst>> GetAsync(long key)
    {
        var categoryFirst = await ctx.CategoryFirst.FirstOrDefaultAsync(x => x.Id == key);

        if (categoryFirst == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(categoryFirst);
        }
    }

    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CategoryFirst>> PostAsync(CategoryFirst categoryFirst)
    {
        var record = await ctx.CategoryFirst.FindAsync(categoryFirst.Id);
        if (record != null)
        {
            return Conflict();
        }

        await ctx.CategoryFirst.AddAsync(categoryFirst);

        await ctx.SaveChangesAsync();

        return Created($"/categoryFirst/{categoryFirst.Id}", categoryFirst);
    }

    [HttpPut("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryFirst>> PutAsync(long key, CategoryFirst update)
    {
        var categoryFirst = await ctx.CategoryFirst.FirstOrDefaultAsync(x => x.Id == key);

        if (categoryFirst == null)
        {
            return NotFound();
        }

        ctx.Entry(categoryFirst).CurrentValues.SetValues(update);

        await ctx.SaveChangesAsync();

        return Ok(categoryFirst);
    }

    [HttpPatch("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryFirst>> PatchAsync(long key, Delta<CategoryFirst> delta)
    {
        var categoryFirst = await ctx.CategoryFirst.FirstOrDefaultAsync(x => x.Id == key);

        if (categoryFirst == null)
        {
            return NotFound();
        }

        delta.Patch(categoryFirst);

        await ctx.SaveChangesAsync();

        return Ok(categoryFirst);
    }

    [HttpDelete("{key}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(long key)
    {
        var categoryFirst = await ctx.CategoryFirst.FindAsync(key);

        if (categoryFirst != null)
        {
            ctx.CategoryFirst.Remove(categoryFirst);
            await ctx.SaveChangesAsync();
        }

        return NoContent();
    }
}
