using Microsoft.EntityFrameworkCore;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Repositories
{
    public class EmployeeQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeeQueryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public EmployeeQueryRepository()
        {

        }

        public virtual async Task<List<HrEmployee>> GetAllEmployees()
        {
            return await _applicationDbContext.HrEmployees.Where(e => e.IsDeleted == false).ToListAsync();
        }

        public virtual async Task<HrEmployee> GetEmployee(int id)
        {
            return await _applicationDbContext.HrEmployees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<bool> IsEmployeeExist(string Name)
        {
            var employee = await _applicationDbContext.HrEmployees.FirstOrDefaultAsync(e => e.Name == Name);
            if (employee == null)
            {
                return false;
            }
            return true;
        }

        public virtual async Task<HrEmployee> GetEmployeeByName(string Name)
        {
            var employee = await _applicationDbContext.HrEmployees.FirstOrDefaultAsync(e => e.Name == Name);
            if (employee == null)
            {
                return null;
            }
            return employee;
        }
    }
}
