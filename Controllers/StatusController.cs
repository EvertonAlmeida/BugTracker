using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BugTracker.Models;

namespace BugTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly BugTrackerContext _context;

        public StatusController(BugTrackerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatus()
        {
            return await _context.Status.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus(Guid id)
        {
            var status = await _context.Status.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return status;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(Guid id, Status status)
        {
            if (id != status.Id)
            {
                return BadRequest();
            }

            _context.Entry(status).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
            _context.Status.Add(status);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatus", new { id = status.Id }, status);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Status>> DeleteStatus(Guid id)
        {
            var status = await _context.Status.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            _context.Status.Remove(status);
            await _context.SaveChangesAsync();

            return status;
        }

        private bool StatusExists(Guid id)
        {
            return _context.Status.Any(e => e.Id == id);
        }
    }
}
