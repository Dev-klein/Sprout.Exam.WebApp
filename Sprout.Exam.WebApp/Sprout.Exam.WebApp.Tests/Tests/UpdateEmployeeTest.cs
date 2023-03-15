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
    public class UpdateEmployeeTest : IClassFixture<EmployeeFixture>
    {
        private readonly EmployeeFixture _employeeFixture;
        private readonly ServiceProvider _service;

        public UpdateEmployeeTest(EmployeeFixture employeeFixture)
        {
            _employeeFixture = employeeFixture;
            _service = employeeFixture.ServiceProvider;
        }

        [Fact]
        public async Task SuccessfullyUpdatedEmployee()
        {
            var dataBuilder = new EmployeeDataBuilder();
            var employeeUpdateRequest = new EditEmployeeDto()
            {
                Id = 2,
                FullName = "John Doe",
                Birthdate = DateTime.Now.AddYears(-30),
                Tin = "2134579864908",
                TypeId = 1
            };
            dataBuilder.CreateUpdateEmployeeTestData(_employeeFixture, employeeUpdateRequest, true);
            var employeeService = _service.GetService<EmployeeService>();
            var updateEmployeeResponse = await employeeService.UpdateEmployee(employeeUpdateRequest);
            Assert.True(updateEmployeeResponse != null);
        }

        [Fact]
        public async Task FailedEmployeeDoesntExist()
        {
            var dataBuilder = new EmployeeDataBuilder();
            var employeeUpdateRequest = new EditEmployeeDto()
            {
                Id = 11,
                FullName = "Jeric Raval",
                Birthdate = DateTime.Now.AddYears(-10),
                Tin = "2134579864908",
                TypeId = 2
            };
            dataBuilder.CreateUpdateEmployeeTestData(_employeeFixture, employeeUpdateRequest, false);
            var employeeService = _service.GetService<EmployeeService>();
            var updateEmployeeResponse = await employeeService.UpdateEmployee(employeeUpdateRequest);
            Assert.False(updateEmployeeResponse != null);
        }
    }
}
