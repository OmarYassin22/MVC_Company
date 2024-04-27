using Demo.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Busiess.Interfaces
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        public  Task<T> Get(int id);
        public Task<IEnumerable<T>> GetAll();
        public void Update(T employee);
        public void Delete(T employee);
        public Task Create(T employee);
        public Task<int> Commit();
    
    }
}
