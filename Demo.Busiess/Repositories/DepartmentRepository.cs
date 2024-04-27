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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(MainContext context):base(context)
        {
            
        }

        public async Task<IEnumerable<Department>>Search(string name)
        {
     
             return await _context.departments.Where(d => d.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }
    }
}
