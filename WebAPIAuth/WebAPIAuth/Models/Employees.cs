namespace WebAPIAuth.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public int Age { get; set; }

        public static List<Employees> employees = new List<Employees>()
            {
                 new Employees{Id=1, Name="Aysha", Gender="female", Department="IT", Age=30},
                 new Employees{Id=2, Name="Shakir", Gender="male", Department="R&D", Age=25},
                 new Employees{Id=3, Name="Amna", Gender="female", Department="HR", Age=27},
                 new Employees{Id=4, Name="Ahmed", Gender="male", Department="R&D", Age=45},
                 new Employees{Id=5, Name="Rida", Gender="female", Department="IT", Age=35},
            };
        public static List<Employees> GetAllEmployees()
        {
            return employees;
        }
        public static Employees GetEmployeeById(int id)
        {
            return employees.FirstOrDefault(e => e.Id == id);
        }
        public static Employees UpdateEmployee(Employees emp)
        {
            Employees empDetails = employees.Find(e => e.Id == emp.Id);
            if(empDetails != null)
            {
                empDetails.Name = emp.Name;
                empDetails.Gender = emp.Gender;
                empDetails.Department = emp.Department;
                empDetails.Age = emp.Age;
            }
            return empDetails;
        }
    }
}
