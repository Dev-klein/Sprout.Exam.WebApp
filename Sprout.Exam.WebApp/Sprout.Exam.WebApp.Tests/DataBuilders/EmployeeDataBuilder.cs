using Moq;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Tests.DataBuilders
{
    public class EmployeeDataBuilder
    {
        public int CreateAddEmployyeTestData(EmployeeFixture fixture, CreateEmployeeDto addEmployee, bool isExist)
        {
            fixture.EmployeeQueryRepository.Setup(x => x.IsEmployeeExist(It.IsAny<string>()))
                .Returns(Task.FromResult(isExist ? true : false));

            fixture.EmployeeCommandRepository.Setup(x => x.AddEmployee(It.IsAny<CreateEmployeeDto>()));

            fixture.EmployeeQueryRepository.Setup(x => x.GetEmployeeByName(It.IsAny<string>()))
                .Returns(Task.FromResult(new HrEmployee()
                {
                    Id = 1,
                    Name = "John Doe",
                    BirthDate = DateTime.Now.AddYears(-30),
                    TIN = "2134579864908",
                    EmployeeType = Common.Enums.EmployeeType.Regular,
                    IsDeleted = false
                }));

            return 1;
        }

        public EmployeeDto CreateUpdateEmployeeTestData(EmployeeFixture fixture, EditEmployeeDto employeeDto, bool isEmployeeExist)
        {
            fixture.EmployeeQueryRepository.Setup(x => x.GetEmployee(It.IsAny<int>()))
                .Returns(Task.FromResult(isEmployeeExist ? new HrEmployee
                {
                    Id = employeeDto.Id,
                    Name = employeeDto.FullName,
                    BirthDate = employeeDto.Birthdate,
                    TIN = employeeDto.Tin,
                    EmployeeType = employeeDto.TypeId == 1 ? Common.Enums.EmployeeType.Regular : Common.Enums.EmployeeType.Contractual,
                    IsDeleted = false
                } : null));

            fixture.EmployeeCommandRepository.Setup(x => x.UpdateEmployee(It.IsAny<HrEmployee>()));

            EmployeeDto updatedEmployee = new EmployeeDto();
            updatedEmployee.Id = employeeDto.Id;
            updatedEmployee.FullName = employeeDto.FullName;
            updatedEmployee.Birthdate = employeeDto.Birthdate.ToShortDateString();
            updatedEmployee.Tin = employeeDto.Tin;
            updatedEmployee.TypeId = employeeDto.TypeId;

            return updatedEmployee;
        }

        public int CreateRemoveEmployeeTestData(EmployeeFixture fixture, int id, bool isExist)
        {
            fixture.EmployeeQueryRepository.Setup(x => x.GetEmployee(It.IsAny<int>()))
                .Returns(Task.FromResult(isExist ? new HrEmployee
                {
                    Id = id,
                    Name = "Johannah Reyes",
                    BirthDate = DateTime.Now.AddYears(-22),
                    TIN = "2424566178",
                    EmployeeType = Common.Enums.EmployeeType.Regular,
                    IsDeleted = false
                } : null));

            fixture.EmployeeCommandRepository.Setup(x => x.UpdateEmployee(It.IsAny<HrEmployee>()));

            return id;
        }

        public List<HrEmployee> CreateGetAllEmployees(EmployeeFixture fixture, List<HrEmployee> employees)
        {
            fixture.EmployeeQueryRepository.Setup(x => x.GetAllEmployees())
                .Returns(Task.FromResult(employees));

            return employees;
        }

        public HrEmployee CreateGetEmployee(EmployeeFixture fixture, int id, bool isExist)
        {
            fixture.EmployeeQueryRepository.Setup(x => x.GetEmployee(It.IsAny<int>()))
                .Returns(Task.FromResult(isExist ? new HrEmployee
                {
                    Id = 3,
                    Name = "Johannah Reyes",
                    BirthDate = DateTime.Now.AddYears(-22),
                    TIN = "2424566178",
                    EmployeeType = Common.Enums.EmployeeType.Contractual,
                    IsDeleted = false
                } : null));
            HrEmployee employee = new HrEmployee
            {
                Id = 3,
                Name = "Johannah Reyes",
                BirthDate = DateTime.Now.AddYears(-22),
                TIN = "2424566178",
                EmployeeType = Common.Enums.EmployeeType.Contractual,
                IsDeleted = false
            };

            return employee;
        }

        public bool CreateCalculateSalaryTestData(EmployeeFixture fixture, HrEmployee hrEmployee, bool isExist)
        {
            fixture.EmployeeQueryRepository.Setup(x => x.GetEmployee(It.IsAny<int>()))
                .Returns(Task.FromResult(isExist ? hrEmployee : null));

            return isExist;
        }
    }
}
