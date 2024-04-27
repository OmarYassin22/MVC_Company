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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly MainContext _context;
        public GenericRepository(MainContext mainContext)
        {
            _context = mainContext;
        }

        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task Create(T employee)
        {
            await _context.AddAsync(employee);
        }

        public void Delete(T employee)
        {
            _context.Remove(employee);
        }

        public async Task<T >Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            {

                return  (IEnumerable<T>)await _context.Employees.Include(x => x.department).ToListAsync();

            }
            return await _context.Set<T>().ToListAsync();
        }

     

        public  void Update(T employee)
        {
            _context.Update(employee);
        }

     
    }
}
