using Microsoft.Extensions.DependencyInjection;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Services;
using Sprout.Exam.WebApp.Tests.DataBuilders;
using Sprout.Exam.WebApp.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.WebApp.Tests.Tests
{
    public class CalculateSalaryTest : IClassFixture<EmployeeFixture>
    {
        private readonly EmployeeFixture _fixture;
        private readonly ServiceProvider _service;

        public CalculateSalaryTest(EmployeeFixture fixture)
        {
            _fixture = fixture;
            _service = fixture.ServiceProvider;
        }

        [Fact]
        public async Task SuccessfullyCalculatedRegularEmployeeSalary()
        {
            var dataBuilder = new EmployeeDataBuilder();
            HrEmployee employee = new HrEmployee()
            {
                Id = 3,
                Name = "Sam Smith",
                BirthDate = DateTime.Now.AddYears(-40),
                TIN = "888657220902",
                EmployeeType = Common.Enums.EmployeeType.Regular,
                IsDeleted = false
            };
            double absentDays = 3;
            double workDays = 15;
            dataBuilder.CreateCalculateSalaryTestData(_fixture, employee, true);
            var employeeService = _service.GetService<EmployeeService>();
            var updateEmployeeResponse = await employeeService.CalculateSalary(employee.Id, absentDays, workDays);
            Assert.True(updateEmployeeResponse != null);
        }

        [Fact]
        public async Task SuccessfullyCalculatedContractualEmployeeSalary()
        {
            var dataBuilder = new EmployeeDataBuilder();
            HrEmployee employee = new HrEmployee()
            {
                Id = 6,
                Name = "Amanda Reyes",
                BirthDate = DateTime.Now.AddYears(-30),
                TIN = "119876453",
                EmployeeType = Common.Enums.EmployeeType.Contractual,
                IsDeleted = false
            };
            double absentDays = 3;
            double workDays = 15;
            dataBuilder.CreateCalculateSalaryTestData(_fixture, employee, true);
            var employeeService = _service.GetService<EmployeeService>();
            var updateEmployeeResponse = await employeeService.CalculateSalary(employee.Id, absentDays, workDays);
            Assert.True(updateEmployeeResponse != null);
        }

        [Fact]
        public async Task FailedEmployeeDoesntExist()
        {
            var dataBuilder = new EmployeeDataBuilder();
            HrEmployee employee = new HrEmployee()
            {
                Id = 0,
                Name = "Johnannez Smith",
                BirthDate = DateTime.Now.AddYears(-10),
                TIN = "112345678902",
                IsDeleted = false
            };
            double absentDays = 3;
            double workDays = 15;
            dataBuilder.CreateCalculateSalaryTestData(_fixture, employee, false);
            var employeeService = _service.GetService<EmployeeService>();
            var updateEmployeeResponse = await employeeService.CalculateSalary(employee.Id, absentDays, workDays);
            Assert.False(updateEmployeeResponse != null);
        }

        [Fact]
        public async Task FailedInvalidEmployeeType()
        {
            var dataBuilder = new EmployeeDataBuilder();
            HrEmployee employee = new HrEmployee()
            {
                Id = 1,
                Name = "Johnannez Smith",
                BirthDate = DateTime.Now.AddYears(-10),
                TIN = "112345678902",
                IsDeleted = false
            };
            double absentDays = 3;
            double workDays = 15;
            dataBuilder.CreateCalculateSalaryTestData(_fixture, employee, false);
            var employeeService = _service.GetService<EmployeeService>();
            var updateEmployeeResponse = await employeeService.CalculateSalary(employee.Id, absentDays, workDays);
            Assert.False(updateEmployeeResponse != null);
        }
    }
}
