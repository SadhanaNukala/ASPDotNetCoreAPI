using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Authorization;
using EmployeeAPI.Authentication;
using EmployeeAPI.Services;

namespace EmployeeAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/employees")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: Employees
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            var response =await _employeeService.GetAllEmployees();
            return Ok(response);
        }

        // GET: Employees/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployeeById(int? id)
        {
           var response = await _employeeService.GetEmployeeById(id);
            return Ok(response);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> PostEmployee([FromBody] Employee employee)
        {
            if (ModelState.IsValid)
            {
               var response = await _employeeService.AddEmployee(employee);
                return CreatedAtAction("GetEmployeeById", new { id = employee.Id }, employee);
            }
            return View(employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditEmployee(int id, [FromBody] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await _employeeService.UpdateEmployee(id, employee);
                return CreatedAtAction("GetEmployeeById", new { id = employee.Id }, employee);
            }
            return View(employee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteEmployee(int id)
        {
            return await _employeeService.DeleteEmployee(id);
        }


    }
}
