using CPW219_CRUD_Troubleshooting.Models;
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
        // Same here:
        // My data was not populating and I couldn't figure it out
        // So, I added some test code into the database manually so
        // you can test out the CRUD ability fully.
        [HttpPost]
        public IActionResult Create(Student p)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Add(p, context);
                //context.SaveChanges();
                TempData["Message"] = $"{p.Name} was added!";
                return RedirectToAction("Index");
            }

            //Show web page with errors
            return View(p);
        }

        public IActionResult Edit(int id)
        {
            //get the product by id
            Student p = StudentDb.GetStudent(context, id);
            return View(p);
        }

        [HttpPost]
        public IActionResult Edit(Student p)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Update(context, p);

                TempData["Message"] = "Product is Updated!";
                return RedirectToAction("Index");
            }
            //return view with errors
            return View(p);
        }

        // You'll need to refresh the page so that it disappears
        public IActionResult Delete(int id)
        {
            Student p = StudentDb.GetStudent(context, id);
            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            //Get Product from database
            Student p = StudentDb.GetStudent(context, id);
            StudentDb.Delete(context, p);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Student p = StudentDb.GetStudent(context, id);
            return View(p);
        }
    }
}
