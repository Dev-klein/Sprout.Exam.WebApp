using Microsoft.Extensions.DependencyInjection;
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
    public class RemoveEmployeeTest : IClassFixture<EmployeeFixture>
    {
        private readonly EmployeeFixture _employeeFixture;
        private readonly ServiceProvider _service;

        public RemoveEmployeeTest(EmployeeFixture employeeFixture)
        {
            _employeeFixture = employeeFixture;
            _service = employeeFixture.ServiceProvider;
        }

        [Fact]
        public async Task SuccessfullyRemovedEmployee()
        {
            var dataBuilder = new EmployeeDataBuilder();
            int id = 3;
            dataBuilder.CreateRemoveEmployeeTestData(_employeeFixture, id, true);
            var employeeService = _service.GetService<EmployeeService>();
            var updateEmployeeResponse = await employeeService.RemoveEmployee(id);
            Assert.True(updateEmployeeResponse != 0);
        }

        [Fact]
        public async Task FailedEmployeeDoesntExist()
        {
            var dataBuilder = new EmployeeDataBuilder();
            int id = 0;
            dataBuilder.CreateRemoveEmployeeTestData(_employeeFixture, id, false);
            var employeeService = _service.GetService<EmployeeService>();
            var updateEmployeeResponse = await employeeService.RemoveEmployee(id);
            Assert.False(updateEmployeeResponse != 0);
        }
    }
}
