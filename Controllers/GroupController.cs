using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace UserApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly GroupContext _context;

        public GroupController(GroupContext context)
        {
            _context = context;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups([FromQuery] string? name)
        {
            var queryable = _context.Groups.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                return await queryable.Where(x => x.Name == name).ToListAsync();
            }
            else
            {
                return await _context.Groups.ToListAsync();
            }

        }

        // GET: api/Groups/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
            var Group = await _context.Groups.FindAsync(id);

            if (Group == null)
            {
                return NotFound();
            }

            return Group;
        }

        // POST: api/Groups
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(Group item)
        {
            _context.Groups.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGroup), new { id = item.Id }, item);
        }

        // PUT: api/Group/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(int id, Group item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Group/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var Group = await _context.Groups.FindAsync(id);

            if (Group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(Group);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}