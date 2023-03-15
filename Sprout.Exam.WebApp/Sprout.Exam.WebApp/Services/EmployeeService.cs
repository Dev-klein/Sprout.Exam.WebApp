using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Helpers;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Services
{
    public class EmployeeService
    {
        private readonly EmployeeCommandRepository _employeeCommandRepository;
        private readonly EmployeeQueryRepository _employeeQueryRepository;

        public EmployeeService(EmployeeCommandRepository employeeCommandRepository, EmployeeQueryRepository employeeQueryRepository)
        {
            _employeeCommandRepository = employeeCommandRepository;
            _employeeQueryRepository = employeeQueryRepository;
        }

        public async virtual Task<List<EmployeeDto>> GetAllEmployees()
        {
            List<EmployeeDto> mapEmployeeList = new List<EmployeeDto>();
            var listOfEmployees = await _employeeQueryRepository.GetAllEmployees();
            foreach (var employee in listOfEmployees)
            {
                EmployeeDto employeeMap = new EmployeeDto()
                {
                    Id = employee.Id,
                    FullName = employee.Name,
                    Birthdate = employee.BirthDate.ToString("yyyy-MM-dd"),
                    Tin = employee.TIN,
                    TypeId = employee.EmployeeType == EmployeeType.Regular ? 1 : 2
                };
                mapEmployeeList.Add(employeeMap);
            }
            return mapEmployeeList;
        }

        public async virtual Task<EmployeeDto> GetEmployee(int id)
        {
            var employee = await _employeeQueryRepository.GetEmployee(id);
            if (employee == null)
            {
                return null;
            }
            EmployeeDto employeeMap = new EmployeeDto()
            {
                Id = employee.Id,
                FullName = employee.Name,
                Birthdate = employee.BirthDate.ToString("yyyy-MM-dd"),
                Tin = employee.TIN,
                TypeId = employee.EmployeeType == EmployeeType.Regular ? 1 : 2
            };
            return employeeMap;
        }

        public async virtual Task<EmployeeDto> UpdateEmployee(EditEmployeeDto editEmployeeDto)
        {
            var employee = await _employeeQueryRepository.GetEmployee(editEmployeeDto.Id);

            if (employee == null)
            {
                return null;
            }

            if(string.IsNullOrEmpty(editEmployeeDto.FullName) || string.IsNullOrEmpty(editEmployeeDto.Tin) || editEmployeeDto.Tin == null || editEmployeeDto.TypeId <= 0 || editEmployeeDto.TypeId > 2)
            {
                return null;
            }

            employee.Name = editEmployeeDto.FullName;
            employee.BirthDate = editEmployeeDto.Birthdate;
            employee.TIN = editEmployeeDto.Tin;
            employee.EmployeeType = editEmployeeDto.TypeId == 1 ? EmployeeType.Regular : EmployeeType.Contractual;

            _employeeCommandRepository.UpdateEmployee(employee);

            EmployeeDto updatedEmployee = new EmployeeDto();
            updatedEmployee.Id = employee.Id;
            updatedEmployee.FullName = employee.Name;
            updatedEmployee.Birthdate = employee.BirthDate.ToString("yyyy-MM-dd");
            updatedEmployee.Tin = employee.TIN;
            updatedEmployee.TypeId = employee.EmployeeType == EmployeeType.Regular ? 1 : 2;

            return updatedEmployee;
        }

        public async Task<int> AddEmployee(CreateEmployeeDto createEmployeeDto)
        {
            var isEmployeeExist = await _employeeQueryRepository.IsEmployeeExist(createEmployeeDto.FullName);
            if (isEmployeeExist)
            {
                //employeeExisting
                return 0;
            }

            _employeeCommandRepository.AddEmployee(createEmployeeDto);

            var createdEmployee = await _employeeQueryRepository.GetEmployeeByName(createEmployeeDto.FullName);
            if (createdEmployee == null)
            {
                return 0;
            }

            return createdEmployee.Id;
        }

        public async Task<int> RemoveEmployee(int id)
        {
            var employee = await _employeeQueryRepository.GetEmployee(id);
            if (employee == null)
            {
                return 0;
            }

            employee.IsDeleted = true;
            _employeeCommandRepository.UpdateEmployee(employee);

            return id;
        }

        public async Task<string> CalculateSalary(int id, double absentDays, double workDays)
        {
            var employee = await _employeeQueryRepository.GetEmployee(id);
            if (employee == null)
            {
                return null;
            }
            var employeeType = employee.EmployeeType;
            var salary = CalculateEmployeeSalary.ComputeSalary(employeeType, absentDays, workDays);
            if(salary == -1)
            {
                return null;
            }

            return string.Format("{0:0.00}", salary);
        }
    }
}
