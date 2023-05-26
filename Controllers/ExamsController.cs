using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPNETMVC_Exam.Entities;

namespace ASPNETMVC_Exam.Controllers
{
    public class ExamsController : Controller
    {
        private readonly MvcExamContext _context;

        public ExamsController(MvcExamContext context)
        {
            _context = context;
        }

        // GET: Exams
        public async Task<IActionResult> Index()
        {
            var mvcExamContext = _context.Exams.Include(e => e.ClassNameNavigation).Include(e => e.FacultyNameNavigation).Include(e => e.SubjectNameNavigation);
            return View(await mvcExamContext.ToListAsync());
        }

        // GET: Exams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.ClassNameNavigation)
                .Include(e => e.FacultyNameNavigation)
                .Include(e => e.SubjectNameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: Exams/Create
        public IActionResult Create()
        {
            ViewData["ClassName"] = new SelectList(_context.Classes, "Name", "Name");
            ViewData["FacultyName"] = new SelectList(_context.Faculties, "Name", "Name");
            ViewData["SubjectName"] = new SelectList(_context.Subjects, "Name", "Name");
            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime,ExamDate,ExamDuration,ClassName,SubjectName,FacultyName,Status")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassName"] = new SelectList(_context.Classes, "Name", "Name", exam.ClassName);
            ViewData["FacultyName"] = new SelectList(_context.Faculties, "Name", "Name", exam.FacultyName);
            ViewData["SubjectName"] = new SelectList(_context.Subjects, "Name", "Name", exam.SubjectName);
            return View(exam);
        }

        // GET: Exams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            ViewData["ClassName"] = new SelectList(_context.Classes, "Name", "Name", exam.ClassName);
            ViewData["FacultyName"] = new SelectList(_context.Faculties, "Name", "Name", exam.FacultyName);
            ViewData["SubjectName"] = new SelectList(_context.Subjects, "Name", "Name", exam.SubjectName);
            return View(exam);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,ExamDate,ExamDuration,ClassName,SubjectName,FacultyName,Status")] Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassName"] = new SelectList(_context.Classes, "Name", "Name", exam.ClassName);
            ViewData["FacultyName"] = new SelectList(_context.Faculties, "Name", "Name", exam.FacultyName);
            ViewData["SubjectName"] = new SelectList(_context.Subjects, "Name", "Name", exam.SubjectName);
            return View(exam);
        }

        // GET: Exams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.ClassNameNavigation)
                .Include(e => e.FacultyNameNavigation)
                .Include(e => e.SubjectNameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exams == null)
            {
                return Problem("Entity set 'MvcExamContext.Exams'  is null.");
            }
            var exam = await _context.Exams.FindAsync(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id)
        {
          return (_context.Exams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
