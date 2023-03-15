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
    public class GetEmployeeTest : IClassFixture<EmployeeFixture>
    {
        private readonly EmployeeFixture _fixture;
        private readonly ServiceProvider _service;

        public GetEmployeeTest(EmployeeFixture fixture)
        {
            _fixture = fixture;
            _service = fixture.ServiceProvider;
        }

        [Fact]
        public async Task SuccessfullyGetEmployee()
        {
            var dataBuilder = new EmployeeDataBuilder();
            int id = 3;
            dataBuilder.CreateGetEmployee(_fixture, id, true);
            var employeeService = _service.GetService<EmployeeService>();
            var updateEmployeeResponse = await employeeService.GetEmployee(id);
            Assert.True(updateEmployeeResponse != null);
        }

        [Fact]
        public async Task FailedGetEmployee()
        {
            var dataBuilder = new EmployeeDataBuilder();
            int id = 0;
            dataBuilder.CreateGetEmployee(_fixture, id, false);
            var employeeService = _service.GetService<EmployeeService>();
            var updateEmployeeResponse = await employeeService.GetEmployee(id);
            Assert.False(updateEmployeeResponse != null);
        }
    }
}
