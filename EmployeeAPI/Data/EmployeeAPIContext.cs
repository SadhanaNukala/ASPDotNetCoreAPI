using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Models;

namespace EmployeeAPI.Data
{
    public class EmployeeAPIContext : DbContext
    {
        public EmployeeAPIContext (DbContextOptions<EmployeeAPIContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeAPI.Models.Employee> Employee { get; set; }
    }
}
