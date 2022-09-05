using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoynerCaseProject.Data;
using BoynerCaseProject.Model;
using Attribute = BoynerCaseProject.Model.Attribute;

namespace BoynerCaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly BoynerCaseProjectContext _context;

        public AttributesController(BoynerCaseProjectContext context)
        {
            _context = context;
        }

        // GET: api/Attributes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attribute>>> GetAttribute()
        {
          if (_context.Attribute == null)
          {
              return NotFound();
          }
            return await _context.Attribute.ToListAsync();
        }

        // GET: api/Attributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attribute>> GetAttribute(int id)
        {
          if (_context.Attribute == null)
          {
              return NotFound();
          }
            var attribute = await _context.Attribute.FindAsync(id);

            if (attribute == null)
            {
                return NotFound();
            }

            return attribute;
        }

        // PUT: api/Attributes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttribute(int id, Attribute attribute)
        {
            if (id != attribute.Id)
            {
                return BadRequest();
            }

            _context.Entry(attribute).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttributeExists(id))
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

        // POST: api/Attributes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Attribute>> PostAttribute(Attribute attribute)
        {
          if (_context.Attribute == null)
          {
              return Problem("Entity set 'BoynerCaseProjectContext.Attribute'  is null.");
          }
            _context.Attribute.Add(attribute);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttribute", new { id = attribute.Id }, attribute);
        }

        // DELETE: api/Attributes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttribute(int id)
        {
            if (_context.Attribute == null)
            {
                return NotFound();
            }
            var attribute = await _context.Attribute.FindAsync(id);
            if (attribute == null)
            {
                return NotFound();
            }

            _context.Attribute.Remove(attribute);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttributeExists(int id)
        {
            return (_context.Attribute?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
