using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeRecordSystem.Models
{
    public class Login
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Department { get; set; }
    }
}
