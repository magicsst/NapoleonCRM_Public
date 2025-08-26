using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using NapoleonCRM.Data;
using NapoleonCRM.Shared.Models;

namespace NapoleonCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableRateLimiting("Fixed")]
    public class CurrencyController(ApplicationDbContext _ctx, ILogger<CurrencyController> _logger) : ControllerBase
    {
        private readonly ILogger<CurrencyController> logger = _logger;
        private readonly ApplicationDbContext ctx = _ctx;

        [HttpGet("")]
        [EnableQuery]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IQueryable<Currency>> Get()
        {
            return Ok(ctx.Currency);
        }

        [HttpGet("{key}")]
        [EnableQuery]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Currency>> GetAsync(long key)
        {
            var currency = await ctx.Currency.FirstOrDefaultAsync(x => x.Id == key);

            if (currency == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(currency);
            }
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Currency>> PostAsync(Address currency)
        {
            var record = await ctx.Currency.FindAsync(currency.Id);
            if (record != null)
            {
                return Conflict();
            }

            await ctx.Address.AddAsync(currency);

            await ctx.SaveChangesAsync();

            return Created($"/address/{currency.Id}", currency);
        }

        [HttpPut("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Currency>> PutAsync(long key, Currency update)
        {
            var currency = await ctx.Currency.FirstOrDefaultAsync(x => x.Id == key);

            if (currency == null)
            {
                return NotFound();
            }

            ctx.Entry(currency).CurrentValues.SetValues(update);

            await ctx.SaveChangesAsync();

            return Ok(currency);
        }

        [HttpPatch("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Currency>> PatchAsync(long key, Delta<Currency> delta)
        {
            var currency = await ctx.Currency.FirstOrDefaultAsync(x => x.Id == key);

            if (currency == null)
            {
                return NotFound();
            }

            delta.Patch(currency);

            await ctx.SaveChangesAsync();

            return Ok(currency);
        }

        [HttpDelete("{key}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(long key)
        {
            var currency = await ctx.Currency.FindAsync(key);

            if (currency != null)
            {
                ctx.Currency.Remove(currency);
                await ctx.SaveChangesAsync();
            }

            return NoContent();

        }

    }
}
