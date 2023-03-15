using Microsoft.Extensions.DependencyInjection;
using Sprout.Exam.Business.DataTransferObjects;
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
    public class EmployeeAddTest : IClassFixture<EmployeeFixture>
    {
        private readonly EmployeeFixture _fixture;
        private readonly ServiceProvider _service;

        public EmployeeAddTest(EmployeeFixture fixture)
        {
            _fixture = fixture;
            _service = fixture.ServiceProvider;
        }

        [Fact]
        public async Task SuccessfullyAddedEmployee()
        {
            var dataBuilder = new EmployeeDataBuilder();
            var employeeRequest = new CreateEmployeeDto()
            {
                FullName = "John Doe",
                Birthdate = DateTime.Now.AddYears(-30),
                Tin = "2134579864908",
                TypeId = 1
            };
            dataBuilder.CreateAddEmployyeTestData(_fixture, employeeRequest, false);
            var employeeService = _service.GetService<EmployeeService>();
            var addEmployeeResponse = await employeeService.AddEmployee(employeeRequest);
            Assert.True(addEmployeeResponse != 0);
        }

        [Fact]
        public async Task FailedAddedEmployee()
        {
            var dataBuilder = new EmployeeDataBuilder();
            var employeeRequest = new CreateEmployeeDto()
            {
                FullName = "John Doe",
                Birthdate = DateTime.Now.AddYears(-30),
                Tin = "2134579864908",
                TypeId = 1
            };
            dataBuilder.CreateAddEmployyeTestData(_fixture, employeeRequest, true);
            var employeeService = _service.GetService<EmployeeService>();
            var addEmployeeResponse = await employeeService.AddEmployee(employeeRequest);
            Assert.False(addEmployeeResponse != 0);
        }
    }
}
