using EmployeeManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Interfaces
{
    public interface IDepartmentServices
    {
        Task<IEnumerable<Department>> GetAllDepartment();
    }
}
