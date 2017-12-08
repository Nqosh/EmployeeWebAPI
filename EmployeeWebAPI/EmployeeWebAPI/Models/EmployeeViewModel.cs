using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeWebAPI.Models
{
    public class EmployeeViewModel
    {
        public int PersonId { get; set; }
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDate { get; set; }
        public string EmployeeNum { get; set; }
        public DateTime EmployeeDate { get; set; }
        public Nullable<DateTime> Terminated { get; set; }
    }
}