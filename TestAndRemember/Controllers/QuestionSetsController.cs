using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestAndRemember.Data;
using TestAndRemember.Models;

namespace TestAndRemember.Controllers
{
    public class QuestionSetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionSetsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: QuestionSets
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuestionSet.ToListAsync());
        }

        // GET: QuestionSets/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionSet = await _context.QuestionSet.SingleOrDefaultAsync(m => m.ID == id);
            if (questionSet == null)
            {
                return NotFound();
            }

            return View(questionSet);
        }

        // GET: QuestionSets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuestionSets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,OwnerUserId,Title")] QuestionSet questionSet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionSet);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(questionSet);
        }

        // GET: QuestionSets/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionSet = await _context.QuestionSet.SingleOrDefaultAsync(m => m.ID == id);
            if (questionSet == null)
            {
                return NotFound();
            }
            return View(questionSet);
        }

        // POST: QuestionSets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ID,OwnerUserId,Title")] QuestionSet questionSet)
        {
            if (id != questionSet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionSet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionSetExists(questionSet.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(questionSet);
        }

        // GET: QuestionSets/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionSet = await _context.QuestionSet.SingleOrDefaultAsync(m => m.ID == id);
            if (questionSet == null)
            {
                return NotFound();
            }

            return View(questionSet);
        }

        // POST: QuestionSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var questionSet = await _context.QuestionSet.SingleOrDefaultAsync(m => m.ID == id);
            _context.QuestionSet.Remove(questionSet);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool QuestionSetExists(long id)
        {
            return _context.QuestionSet.Any(e => e.ID == id);
        }
    }
}
