using Demo.Busiess.Interfaces;
using Demo.DataAccess.Contexts;
using Demo.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Busiess.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(MainContext context):base(context) { }

        public Employee GetByAddress(string address)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Employee>> Search(string query)
         {

            return await _context.Employees.Include(e=>e.department).Where(e=>e.Name.ToLower().Contains(query.ToLower())).ToListAsync();
        }

    }
}
