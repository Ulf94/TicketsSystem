using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI;
using ToDoListAPI.Data;

namespace ToDoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoryTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CategoryTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryType>>> GetCategoryTypes()
        {
            return await _context.CategoryTypes.ToListAsync();
        }

        // GET: api/CategoryTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryType>> GetCategoryType(int id)
        {
            var categoryType = await _context.CategoryTypes.FindAsync(id);

            if (categoryType == null)
            {
                return NotFound();
            }

            return categoryType;
        }

        // PUT: api/CategoryTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryType(int id, CategoryType categoryType)
        {
            if (id != categoryType.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoryType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryTypeExists(id))
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

        // POST: api/CategoryTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryType>> PostCategoryType(CategoryType categoryType)
        {
            _context.CategoryTypes.Add(categoryType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryType", new { id = categoryType.Id }, categoryType);
        }

        // DELETE: api/CategoryTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryType(int id)
        {
            var categoryType = await _context.CategoryTypes.FindAsync(id);
            if (categoryType == null)
            {
                return NotFound();
            }

            _context.CategoryTypes.Remove(categoryType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryTypeExists(int id)
        {
            return _context.CategoryTypes.Any(e => e.Id == id);
        }
    }
}
