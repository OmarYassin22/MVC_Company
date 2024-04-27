using Demo.Busiess.Interfaces;
using Demo.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Busiess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Lazy<IDepartmentRepository> _DepartmentRepository;
        private Lazy<IEmployeeRepository> _EmployeeRepository;
        private readonly MainContext mainContext;

        public IEmployeeRepository EmployeeRepository => _EmployeeRepository.Value;

        public IDepartmentRepository DepartmentRepository => _DepartmentRepository.Value;

        public UnitOfWork(MainContext mainContext)
        {
            this.mainContext = mainContext;

            _DepartmentRepository=new Lazy<IDepartmentRepository>( new DepartmentRepository(mainContext));
            _EmployeeRepository =new Lazy<IEmployeeRepository>( new EmployeeRepository(mainContext));
        }

        public void Dispose()
        {
           mainContext.DisposeAsync();
        }
    }
}
