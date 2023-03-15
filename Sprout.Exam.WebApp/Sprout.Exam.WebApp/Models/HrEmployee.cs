using Sprout.Exam.Common.Enums;
using System;

namespace Sprout.Exam.WebApp.Models
{
    public class HrEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string TIN { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public bool IsDeleted { get; set; }
    }
}
