using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BoynerCaseProject.Data;
using BoynerCaseProject.Model;

namespace BoynerCaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeDetailsController : ControllerBase
    {
        private readonly BoynerCaseProjectContext _context;

        public AttributeDetailsController(BoynerCaseProjectContext context)
        {
            _context = context;
        }

        // GET: api/AttributeDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttributeDetail>>> GetAttributeDetail()
        {
          if (_context.AttributeDetail == null)
          {
              return NotFound();
          }
            return await _context.AttributeDetail.ToListAsync();
        }

        // GET: api/AttributeDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttributeDetail>> GetAttributeDetail(int id)
        {
          if (_context.AttributeDetail == null)
          {
              return NotFound();
          }
            var attributeDetail = await _context.AttributeDetail.FindAsync(id);

            if (attributeDetail == null)
            {
                return NotFound();
            }

            return attributeDetail;
        }

        // PUT: api/AttributeDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttributeDetail(int id, AttributeDetail attributeDetail)
        {
            if (id != attributeDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(attributeDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttributeDetailExists(id))
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

        // POST: api/AttributeDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AttributeDetail>> PostAttributeDetail(AttributeDetail attributeDetail)
        {
          if (_context.AttributeDetail == null)
          {
              return Problem("Entity set 'BoynerCaseProjectContext.AttributeDetail'  is null.");
          }
            _context.AttributeDetail.Add(attributeDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttributeDetail", new { id = attributeDetail.Id }, attributeDetail);
        }

        // DELETE: api/AttributeDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttributeDetail(int id)
        {
            if (_context.AttributeDetail == null)
            {
                return NotFound();
            }
            var attributeDetail = await _context.AttributeDetail.FindAsync(id);
            if (attributeDetail == null)
            {
                return NotFound();
            }

            _context.AttributeDetail.Remove(attributeDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttributeDetailExists(int id)
        {
            return (_context.AttributeDetail?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
