using Microsoft.EntityFrameworkCore;
namespace CPW219_CRUD_Troubleshooting.Models
{
    public static class StudentDb
    {
        // My data was not populating and I couldn't figure it out
        // So, I added some test code into the database manually so
        // you can test out the CRUD ability fully.
        public static Student Add(Student p, SchoolContext context)
        {
            //Add student to context
            context.Students.Add(p);

            context.SaveChangesAsync();

            //context.SaveChanges();
            return p;
        }

        public static List<Student> GetStudents(SchoolContext context)
        {
            return (from s in context.Students
                    select s).ToList();
        }

        public static Student GetStudent(SchoolContext context, int id)
        {
            Student p2 = context
                            .Students
                            .Where(s => s.StudentId == id)
                            .Single();
            return p2;
        }

        // You'll need to refresh the page so that it disappears
        public static void Delete(SchoolContext context, Student p)
        {
            context.Students.Remove(p);
            context.SaveChangesAsync();
           
        }

        public static void Update(SchoolContext context, Student p)
        {
            //Mark the object as deleted
            context.Students.Update(p);

            //Send delete query to database
            context.SaveChangesAsync();

        }
    }
}
