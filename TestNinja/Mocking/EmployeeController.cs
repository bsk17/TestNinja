using System.Data.Entity;

namespace TestNinja.Mocking
{
    public class EmployeeController
    {
        private readonly IEmployeeStorage storage;

        public EmployeeController(IEmployeeStorage storage)
        {
            this.storage = storage;
        }

        public ActionResult DeleteEmployee(int id)
        {
            storage.DeleteEmployee(id);  
            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }
 
    public class RedirectResult : ActionResult { }
    
    public class EmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }
}