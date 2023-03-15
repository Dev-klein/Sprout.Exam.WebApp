using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Models;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Repositories
{
    public class EmployeeCommandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeeCommandRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public EmployeeCommandRepository()
        {

        }

        public virtual void AddEmployee(CreateEmployeeDto hrEmployee)
        {
            HrEmployee employee = new HrEmployee();
            employee.Name = hrEmployee.FullName;
            employee.BirthDate = hrEmployee.Birthdate;
            employee.TIN = hrEmployee.Tin;
            employee.EmployeeType = hrEmployee.TypeId == 1 ? EmployeeType.Regular : EmployeeType.Contractual;
            _applicationDbContext.Add(employee);
            _applicationDbContext.SaveChanges();
        }

        public virtual void UpdateEmployee(HrEmployee hrEmployee)
        {
            _applicationDbContext.Update(hrEmployee);
            _applicationDbContext.SaveChanges();
        }
    }
}
