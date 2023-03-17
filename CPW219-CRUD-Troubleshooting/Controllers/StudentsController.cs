﻿using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Student> products = StudentDb.GetStudents(context);


            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student p)
        {
            if (ModelState.IsValid)
            {
                context.Students.Add(p);
                await context.SaveChangesAsync();

                ViewData["Message"] = $"{p.Name} was added!";
                return View();
            }

            //Show web page with errors
            return View(p);
        }

        public async Task<IActionResult> Edit(int id)
        {
            //get the product by id
            Student? p = await context.Students.FindAsync(id);
            if(p == null)
            {
                return NotFound();
            }
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student p)
        {
            if (ModelState.IsValid)
            {
                context.Students.Update(p);
                await context.SaveChangesAsync();

                ViewData["Message"] = "Product Updated!";
                return RedirectToAction("Index");
            }
            //return view with errors
            return View(p);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Student? p =  await context.Students.FindAsync(id);
            if(p == null)
            {
                return NotFound();
            }
            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            //Get Product from database
            Student? p =  await context.Students.FindAsync(id);

            if(p != null)
            {
                context.Students.Remove(p);
                await context.SaveChangesAsync();
                TempData["Message"] = $"{p.Name} was deleted!";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "Student was already deleted!!!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            Student? p = await context.Students.FindAsync(id);

            if(p == null) {         
                return NotFound();
            }
            return View(p);
        }
    }
}
