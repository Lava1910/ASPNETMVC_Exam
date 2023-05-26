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
            var mvcExamContext = _context.Exams.Include(e => e.Class).Include(e => e.Faculty).Include(e => e.Subject);
            return View(await mvcExamContext.ToListAsync());
        }


        // GET: Exams/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id");
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id");
            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime,ExamDate,ExamDuration,ClassId,SubjectId,FacultyId,Status")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id", exam.ClassId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Id", exam.FacultyId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", exam.SubjectId);
            return View(exam);
        }

    }
}
