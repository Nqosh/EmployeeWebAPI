using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EmployeeWebAPI.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public string EmployeeNum { get; set; }
        public DateTime EmployeeDate { get; set; }
        public Nullable<DateTime> Terminated { get; set; }
        public virtual Person Person { get; set; }
    }
}