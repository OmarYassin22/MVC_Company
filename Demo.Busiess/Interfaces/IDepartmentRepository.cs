using Demo.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Busiess.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
         Task<IEnumerable<Department>> Search(string name);
       
    }
}
