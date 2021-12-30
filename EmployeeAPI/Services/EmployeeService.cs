using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeAPIContext _context;
        public EmployeeService(EmployeeAPIContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var result = await _context.Employee.ToListAsync();
            return result;
        }

        public async Task<Employee> GetEmployeeById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);
            if(employee == null)
            {
                return null;
            }
            return employee;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
             _context.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return employee;
        }
        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }

        public async Task<string> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return "Deleted Succesfully";
        }
    }
}


































