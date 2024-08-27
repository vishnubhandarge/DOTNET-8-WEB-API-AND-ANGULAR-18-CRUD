using EmployeePortal.Data;
using EmployeePortal.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Repository
{
    public class EmployeeRepository
    {
        private readonly AppDbContext dbContext;
        public EmployeeRepository(AppDbContext appDbContext)
        {
            this.dbContext = appDbContext;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await dbContext.Employees.ToListAsync();
        }

        public async Task CreateEmployee(Employee employee)
        {
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateEmployee(int id, Employee emp)
        {
            var employee = await dbContext.Employees.FindAsync(id);

            if (employee != null)
            {
                employee.Name = emp.Name;
                employee.Email = emp.Email;
                employee.Salary = emp.Salary;
                employee.Mobile = emp.Mobile;
                employee.Status = emp.Status;
                employee.Age = emp.Age;
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteEmployee(int id)
        {
            var empid = await dbContext.Employees.FindAsync(id);
            if (empid != null)
            {
                dbContext.Employees.Remove(empid);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Employee not found");
            }
        }
    }
}
