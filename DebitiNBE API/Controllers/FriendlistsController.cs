using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DebitiNBE_API.Models.DB;

namespace DebitiNBE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendlistsController : ControllerBase
    {
        private readonly DebitiContext _context;

        public FriendlistsController(DebitiContext context)
        {
            _context = context;
        }

        // GET: api/Friendlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friendlist>>> GetFriendlist()
        {
            return await _context.Friendlist.ToListAsync();
        }

        // GET: api/Friendlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Friendlist>> GetFriendlist(int id)
        {
            var friendlist = await _context.Friendlist.FindAsync(id);

            if (friendlist == null)
            {
                return NotFound();
            }

            return friendlist;
        }

        // PUT: api/Friendlists/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriendlist(int id, Friendlist friendlist)
        {
            if (id != friendlist.UserId)
            {
                return BadRequest();
            }

            _context.Entry(friendlist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendlistExists(id))
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

        // POST: api/Friendlists
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Friendlist>> PostFriendlist(Friendlist friendlist)
        {
            _context.Friendlist.Add(friendlist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FriendlistExists(friendlist.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFriendlist", new { id = friendlist.UserId }, friendlist);
        }

        // DELETE: api/Friendlists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Friendlist>> DeleteFriendlist(int id)
        {
            var friendlist = await _context.Friendlist.FindAsync(id);
            if (friendlist == null)
            {
                return NotFound();
            }

            _context.Friendlist.Remove(friendlist);
            await _context.SaveChangesAsync();

            return friendlist;
        }

        private bool FriendlistExists(int id)
        {
            return _context.Friendlist.Any(e => e.UserId == id);
        }
    }
}
