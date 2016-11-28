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
    [Route("api/QuestionApi")]
    public class QuestionApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/QuestionApi
        [HttpGet]
        public IEnumerable<Question> GetQuestion()
        {
            //            return _context.Question;
          
                var questions = _context.Question
                    .Include(question => question.QuestionSet)
                    .ToList();
            return questions;
            
        }

        // GET: api/QuestionApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Question question = await _context.Question.SingleOrDefaultAsync(m => m.ID == id);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // PUT: api/QuestionApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion([FromRoute] long id, [FromBody] Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != question.ID)
            {
                return BadRequest();
            }

            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
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

        // POST: api/QuestionApi
        [HttpPost]
        public async Task<IActionResult> PostQuestion([FromBody] Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Question.Add(question);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuestionExists(question.ID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetQuestion", new { id = question.ID }, question);
        }

        // DELETE: api/QuestionApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Question question = await _context.Question.SingleOrDefaultAsync(m => m.ID == id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Question.Remove(question);
            await _context.SaveChangesAsync();

            return Ok(question);
        }

        private bool QuestionExists(long id)
        {
            return _context.Question.Any(e => e.ID == id);
        }
    }
}