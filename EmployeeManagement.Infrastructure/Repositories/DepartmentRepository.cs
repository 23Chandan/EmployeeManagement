using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeManagementDbContext _EmployeeManagementDbContext;
        public DepartmentRepository(EmployeeManagementDbContext employeeManagementDbContext)
        {
            _EmployeeManagementDbContext = employeeManagementDbContext;
        }
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _EmployeeManagementDbContext.Departments.ToListAsync();
        }
    }
}
