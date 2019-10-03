using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Detail.Models;

namespace Detail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailListsController : ControllerBase
    {
        private readonly DetailContext _context;

        public DetailListsController(DetailContext context)
        {
            _context = context;
        }

        // GET: api/DetailLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetailList>>> GetDetailLists()
        {
            return await _context.DetailLists.ToListAsync();
        }

        // GET: api/DetailLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetailList>> GetDetailList(long id)
        {
            var detailList = await _context.DetailLists.FindAsync(id);

            if (detailList == null)
            {
                return NotFound();
            }

            return detailList;
        }

        // PUT: api/DetailLists/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetailList(long id, DetailList detailList)
        {
            if (id != detailList.Id)
            {
                return BadRequest();
            }

            _context.Entry(detailList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailListExists(id))
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

        // POST: api/DetailLists
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DetailList>> PostDetailList(DetailList detailList)
        {
            _context.DetailLists.Add(detailList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetailList", new { id = detailList.Id }, detailList);
        }

        // DELETE: api/DetailLists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DetailList>> DeleteDetailList(long id)
        {
            var detailList = await _context.DetailLists.FindAsync(id);
            if (detailList == null)
            {
                return NotFound();
            }

            _context.DetailLists.Remove(detailList);
            await _context.SaveChangesAsync();

            return detailList;
        }

        private bool DetailListExists(long id)
        {
            return _context.DetailLists.Any(e => e.Id == id);
        }
    }
}
