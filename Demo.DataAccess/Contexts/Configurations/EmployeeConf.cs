using Demo.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Contexts.Configurations
{
    internal class EmployeeConf : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(p=>p.Id).UseIdentityColumn();
            builder.Property(p=>p.Salary).HasColumnType("decimal(18,2)");
        }
    }
}
