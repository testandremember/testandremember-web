using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAndRemember.Data;
using TestAndRemember.Models;

namespace TestAndRemember.Controllers
{
    [Produces("application/json")]
    [Route("api/QuestionSetApi")]
    public class QuestionSetApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionSetApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/QuestionSetApi
        [HttpGet]
        public IEnumerable<QuestionSet> GetQuestionSet()
        {
            return _context.QuestionSet;
        }

        // GET: api/QuestionSetApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionSet([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            QuestionSet questionSet = await _context.QuestionSet.SingleOrDefaultAsync(m => m.ID == id);

            if (questionSet == null)
            {
                return NotFound();
            }

            return Ok(questionSet);
        }

        // PUT: api/QuestionSetApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionSet([FromRoute] long id, [FromBody] QuestionSet questionSet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != questionSet.ID)
            {
                return BadRequest();
            }

            _context.Entry(questionSet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionSetExists(id))
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

        // POST: api/QuestionSetApi
        [HttpPost]
        public async Task<IActionResult> PostQuestionSet([FromBody] QuestionSet questionSet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.QuestionSet.Add(questionSet);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuestionSetExists(questionSet.ID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetQuestionSet", new { id = questionSet.ID }, questionSet);
        }

        // DELETE: api/QuestionSetApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionSet([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            QuestionSet questionSet = await _context.QuestionSet.SingleOrDefaultAsync(m => m.ID == id);
            if (questionSet == null)
            {
                return NotFound();
            }

            _context.QuestionSet.Remove(questionSet);
            await _context.SaveChangesAsync();

            return Ok(questionSet);
        }

        private bool QuestionSetExists(long id)
        {
            return _context.QuestionSet.Any(e => e.ID == id);
        }
    }
}