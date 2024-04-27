using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Models
{
    public class Department: BaseEntity
    {

        public string Code { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
