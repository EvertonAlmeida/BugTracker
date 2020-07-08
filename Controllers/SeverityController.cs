using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BugTracker.Models;

namespace BugTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeverityController : ControllerBase
    {
        private readonly BugTrackerContext _context;

        public SeverityController(BugTrackerContext context)
        {
            _context = context;
        }

        // GET: api/Severity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Severity>>> GetSeverities()
        {
            return await _context.Severities.ToListAsync();
        }

        // GET: api/Severity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Severity>> GetSeverity(Guid id)
        {
            var severity = await _context.Severities.FindAsync(id);

            if (severity == null)
            {
                return NotFound();
            }

            return severity;
        }

        // PUT: api/Severity/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeverity(Guid id, Severity severity)
        {
            if (id != severity.Id)
            {
                return BadRequest();
            }

            _context.Entry(severity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeverityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Severity
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Severity>> PostSeverity(Severity severity)
        {
            _context.Severities.Add(severity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeverity", new { id = severity.Id }, severity);
        }

        // DELETE: api/Severity/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Severity>> DeleteSeverity(Guid id)
        {
            var severity = await _context.Severities.FindAsync(id);
            if (severity == null)
            {
                return NotFound();
            }

            _context.Severities.Remove(severity);
            await _context.SaveChangesAsync();

            return severity;
        }

        private bool SeverityExists(Guid id)
        {
            return _context.Severities.Any(e => e.Id == id);
        }
    }
}
