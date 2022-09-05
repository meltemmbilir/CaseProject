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
    public class CategoriesController : ControllerBase
    {
        private readonly BoynerCaseProjectContext _context;

        public CategoriesController(BoynerCaseProjectContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            if (_context.Category == null)
            {
                return NotFound();
            }
            if (_context.Attribute == null)
            {
                return Problem("Categori icindeki Attribute null!!!");
            }

            //Kategori Attributeleri doldurulur .
            foreach (var itemCategory in _context.Category)
            {
                itemCategory.CategoryAttributes = _context.Attribute.Where(e=>e.CategoryId == itemCategory.Id).ToList();
              
                // Attribute Detayları doldurulur .
                foreach (var itemAttribute in itemCategory.CategoryAttributes)
                {
                    itemAttribute.AttributeDetail = _context.AttributeDetail.Where(e=>e.AttributeId == itemAttribute.Id).ToList();
                }
            }

            return await _context.Category.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            if (_context.Category == null)
            {
                return NotFound();
            }
            var category = await _context.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            //Kategori Attributeleri doldurulur .
            category.CategoryAttributes = _context.Attribute.Where(e => e.CategoryId == id).ToList();
            
            // Attribute Detayları doldurulur .
            foreach (var itemAttribute in category.CategoryAttributes)
            {
                itemAttribute.AttributeDetail = _context.AttributeDetail.Where(e => e.AttributeId == itemAttribute.Id).ToList();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            if (_context.Category == null)
            {
                return Problem("Entity set 'BoynerCaseProjectContext.Category'  is null.");
            }
            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Category == null)
            {
                return NotFound();
            }
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Category?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}/CategoryName")]
        public async Task<ActionResult<Category>> GetCategoryWithName(string name)
        {
            if (_context.Category == null)
            {
                return NotFound();
            }
            var category = await _context.Category.Where(e => e.Name == name).FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound();
            }

            //Kategori Attributeleri doldurulur .
            category.CategoryAttributes = _context.Attribute.Where(e => e.CategoryId == category.Id).ToList();

            // Attribute Detayları doldurulur .
            foreach (var itemAttribute in category.CategoryAttributes)
            {
                itemAttribute.AttributeDetail = _context.AttributeDetail.Where(e => e.AttributeId == itemAttribute.Id).ToList();
            }
            return category;
        }

        /// <summary>
        /// Attribute Name e göre Kategorileri dönen methoddur.
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        [HttpGet("{attributeName}/CategoryAttribute")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryWithAttributes(string attributeName)
        {
            var categoryList = new List<Category>();    

            if (_context.Category == null)
            {
                return NotFound();
            }

            var attributeList = _context.Attribute.Where(e => e.Name == attributeName).ToList();
            if (attributeList == null)
            {
                return NotFound();
            }

            foreach (var item in attributeList)
            {
                categoryList = await _context.Category.Where(e => e.Id == item.CategoryId).ToListAsync();
            }
            return categoryList;
        }
    }
}
