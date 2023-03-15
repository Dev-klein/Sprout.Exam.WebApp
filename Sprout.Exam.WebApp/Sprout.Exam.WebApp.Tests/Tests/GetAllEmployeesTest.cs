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
    public class GetAllEmployeesTest : IClassFixture<EmployeeFixture>
    {
        private readonly EmployeeFixture _fixture;
        private readonly ServiceProvider _service;

        public GetAllEmployeesTest(EmployeeFixture fixture)
        {
            _fixture = fixture;
            _service = fixture.ServiceProvider;
        }

        [Fact]
        public async Task SuccessfullyGetAllEmployees()
        {
            var dataBuilder = new EmployeeDataBuilder();
            List<HrEmployee> employees = new List<HrEmployee>()
            {
                new HrEmployee
                {
                    Id = 3,
                    Name = "Johannah Reyes",
                    BirthDate = DateTime.Now.AddYears(-22),
                    TIN = "2424566178",
                    EmployeeType = Common.Enums.EmployeeType.Contractual,
                    IsDeleted = false
                },
                new HrEmployee
                {
                    Id = 5,
                    Name = "Samantha Smith",
                    BirthDate = DateTime.Now.AddYears(-29),
                    TIN = "19212366789",
                    EmployeeType = Common.Enums.EmployeeType.Regular,
                    IsDeleted = false
                },
                new HrEmployee
                {
                    Id = 8,
                    Name = "Jason Momoa",
                    BirthDate = DateTime.Now.AddYears(-33),
                    TIN = "11968044568",
                    EmployeeType = Common.Enums.EmployeeType.Regular,
                    IsDeleted = false
                },
            };
            dataBuilder.CreateGetAllEmployees(_fixture, employees);
            var employeeService = _service.GetService<EmployeeService>();
            var updateEmployeeResponse = await employeeService.GetAllEmployees();
            Assert.True(updateEmployeeResponse != null);
        }
    }
}
