using Demo.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Busiess.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
    public Employee GetByAddress(string address);
        public Task<IEnumerable<Employee>> Search(string query);


    }
}
