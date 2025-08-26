using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using NapoleonCRM.Data;
using NapoleonCRM.Shared.Models;
using System.Diagnostics.Metrics;

namespace NapoleonCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableRateLimiting("Fixed")]
    public class CountryController(ApplicationDbContext _ctx, ILogger<CountryController> _logger) : ControllerBase
    {
        private readonly ILogger<CountryController> logger = _logger;
        private readonly ApplicationDbContext ctx = _ctx;

        [HttpGet("")]
        [EnableQuery]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IQueryable<Country>> Get()
        {
            return Ok(ctx.Country);
        }

        [HttpGet("{key}")]
        [EnableQuery]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Country>> GetAsync(long key)
        {
            var country = await ctx.Country.FirstOrDefaultAsync(x => x.Id == key);

            if (country == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(country);
            }
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Country>> PostAsync(Country country)
        {
            var record = await ctx.Country.FindAsync(country.Id);
            if (record != null)
            {
                return Conflict();
            }

            await ctx.Country.AddAsync(country);

            await ctx.SaveChangesAsync();

            return Created($"/country/{country.Id}", country);
        }

        [HttpPut("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Country>> PutAsync(long key, Country update)
        {
            var country = await ctx.Country.FirstOrDefaultAsync(x => x.Id == key);

            if (country == null)
            {
                return NotFound();
            }

            ctx.Entry(country).CurrentValues.SetValues(update);

            await ctx.SaveChangesAsync();

            return Ok(country);
        }

        [HttpPatch("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Country>> PatchAsync(long key, Delta<Country> delta)
        {
            var country = await ctx.Country.FirstOrDefaultAsync(x => x.Id == key);

            if (country == null)
            {
                return NotFound();
            }

            delta.Patch(country);

            await ctx.SaveChangesAsync();

            return Ok(country);
        }

        [HttpDelete("{key}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(long key)
        {
            var country = await ctx.Country.FindAsync(key);

            if (country != null)
            {
                ctx.Country.Remove(country);
                await ctx.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
