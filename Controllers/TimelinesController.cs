using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Xronopic.Api.Data;
using Xronopic.Api.Models;

namespace Xronopic.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize] // Added: Requires authentication
  public class TimelinesController : ControllerBase
  {
    private readonly AppDbContext _context;

    public TimelinesController(AppDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Timeline>>> GetTimelines()
    {
      var timelines = await _context.Timelines
          .Include(t => t.Events)
          .ToListAsync();
      return Ok(timelines);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Timeline>> GetTimeline(int id)
    {
      var timeline = await _context.Timelines
          .Include(t => t.Events)
          .FirstOrDefaultAsync(t => t.Id == id);

      if (timeline == null) return NotFound();
      return Ok(timeline);
    }

    [HttpPost]
    public async Task<ActionResult<Timeline>> CreateTimeline([FromBody] Timeline timeline)
    {
      if (timeline == null)
      {
        return BadRequest("Invalid timeline data.");
      }

      // Get the user's ID from the JWT claims
      var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
      if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
      {
        return Unauthorized("User ID not found in JWT.");
      }

      timeline.UserId = userId; // Set the user ID from the JWT.

      // Convert all event dates to UTC
      if (timeline.Events != null)
      {
        foreach (var evt in timeline.Events)
        {
          if (evt.EventDate.HasValue)
          {
            evt.EventDate = evt.EventDate.Value.ToUniversalTime();
          }
        }
      }

      _context.Timelines.Add(timeline);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetTimeline), new { id = timeline.Id }, timeline);
    }

    //TODO:! add put n delete later

    [HttpGet("me")]
    public async Task<ActionResult<IEnumerable<Timeline>>> GetTimelineByUserId()
    {
      // Get the user's ID from the JWT claims
      var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
      if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
      {
        return Unauthorized("User ID not found in JWT.");
      }

      var timelines = await _context.Timelines
          .Include(t => t.Events)
          .Where(t => t.UserId == userId)
          .ToListAsync();

      return Ok(timelines);
    }
  }
}