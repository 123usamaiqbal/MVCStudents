using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCStudents.Data;
using MVCStudents.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Linq.Dynamic.Core;

namespace MVCStudents.Controllers
{
    public class StudentsController : Controller
    {
        private readonly MVCStudentsContext _context;

        public StudentsController(MVCStudentsContext context)
        {
            _context = context;
        }

        //REDIRECT to Grading/Create
        public IActionResult Gradingcreate()
        {
            return LocalRedirect("/Gradings/Create");
        }



    // GET: Students
    public async Task<IActionResult> Index()
        {
              return _context.Students != null ? 
                          View(await _context.Students.ToListAsync()) :
                          Problem("Entity set 'MVCStudentsContext.Students'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (students == null)
            {
                return NotFound();
            }
            

            return View(students);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //public async Task<IActionResult> Create([Bind("Id,Name,Fname,DOB,Email,ImageFile")] Students students)
        //{
        //    var ImageFile = "";
        //    if (ImageFile != null && ImageFile.Length > 0)
        //    {
        //        // Process the image file here
        //        // Example: Save the image to disk
        //        var imagePath = "path/to/save/image.jpg";
        //        using (var stream = new FileStream(imagePath, FileMode.Create))
        //        {
        //            ImageFile.CopyTo(stream);
        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(students);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(students);
        //}


        // POST: Students/Create    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Fname,DOB,Email,ImageFile")] Students students)
        {
           

            if (ModelState.IsValid)
            {
                _context.Add(students);
                await _context.SaveChangesAsync();
                var generatedId = students.Id;
                if (students.ImageFile != null && students.ImageFile.Length > 0)
                {
                    // Process the image file here
                    // Example: Save the image to disk
                    //var imagePath = "path/to/save/image.jpg";
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "img" + students.Id + ".jpg");
                    // var imagePath = @"C:\test\img" + students.Id + ".jpg";
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await students.ImageFile.CopyToAsync(stream);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(students);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Fname,DOB,Email,ImageFile")] Students students)
        {
            if (id != students.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (students.ImageFile != null && students.ImageFile.Length > 0)
                    {
                        // Process the image file here
                        // Example: Save the image to disk
                        //var imagePath = "path/to/save/image.jpg";

                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "img" + students.Id + ".jpg");
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await students.ImageFile.CopyToAsync(stream);
                        }
                    }
                    _context.Update(students);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsExists(students.Id))
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
            return View(students);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'MVCStudentsContext.Students'  is null.");
            }
            var students = await _context.Students.FindAsync(id);
            if (students != null)
            {

                var imageName = "img" + students.Id + ".jpg";
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageName);

                var fileInfo = new FileInfo(imagePath);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                    // Image successfully deleted
                }
                _context.Students.Remove(students);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentsExists(int id)
        {
          return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public IActionResult LoadData([FromBody] DataTablesParameters parameters)
        {
            // Fetch your data from the database (use sorting and filtering if necessary)
            IQueryable<Students> data = _context.Students;

            // Implement sorting based on DataTables parameters
            string sortColumnName = parameters.Columns[parameters.Order[0].Column].Data;
            string sortDirection = parameters.Order[0].Dir;

            data = data.OrderBy(sortColumnName + " " + sortDirection);

            // Implement filtering based on DataTables search parameter
            if (!string.IsNullOrEmpty(parameters.Search.Value))
            {
                data = data.Where(d =>
                    d.Name.Contains(parameters.Search.Value));
                // Add other properties for filtering as needed
            }

            // Get the total count before pagination
            int totalRecords = data.Count();

            // Implement server-side pagination
            data = data.Skip(parameters.Start).Take(parameters.Length);

            // Prepare the response
            var response = new
            {
                draw = parameters.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = data.ToList()
            };
                          
            return Ok(response);
        }
    }
}

