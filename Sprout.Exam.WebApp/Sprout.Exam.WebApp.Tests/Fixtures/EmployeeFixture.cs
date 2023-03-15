using Microsoft.Extensions.DependencyInjection;
using Moq;
using Sprout.Exam.WebApp.Repositories;
using Sprout.Exam.WebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Tests.Fixtures
{
    public class EmployeeFixture
    {
        private readonly Mock<EmployeeCommandRepository> _employeeCommandRepository;
        private readonly Mock<EmployeeQueryRepository> _employeeQueryRepository;

        public ServiceProvider ServiceProvider { get; private set; }

        public Mock<EmployeeCommandRepository> EmployeeCommandRepository { get { return _employeeCommandRepository; } }
        public Mock<EmployeeQueryRepository> EmployeeQueryRepository { get { return _employeeQueryRepository; } }

        public EmployeeFixture()
        {
            var services = new ServiceCollection();
            _employeeCommandRepository = new Mock<EmployeeCommandRepository>();
            _employeeQueryRepository = new Mock<EmployeeQueryRepository>();

            services.AddScoped(x => _employeeCommandRepository.Object);
            services.AddScoped(x => _employeeQueryRepository.Object);
            services.AddScoped<EmployeeService>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
