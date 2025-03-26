using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xronopic.Api.Data;
using Xronopic.Api.Models;

namespace Xronopic.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UsersController : ControllerBase
  {
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
      var users = await _context.Users.ToListAsync();
      return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null) return NotFound();
      return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] User user)
    {
      // TODO: hash pass correctly not like this, this is example
      _context.Users.Add(user);
      await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }
  }
}
