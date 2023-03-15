using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Services;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _employeeService.GetAllEmployees();
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _employeeService.GetEmployee(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            var updatedEmployee = await _employeeService.UpdateEmployee(input);
            if (updatedEmployee == null)
            {
                return BadRequest();
            }
            return Ok(updatedEmployee);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            var newEmployee = await _employeeService.AddEmployee(input);
            if (newEmployee == 0)
            {
                return BadRequest();
            }

            return Created($"/api/employees/{newEmployee}", newEmployee);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.RemoveEmployee(id);
            if (result == 0)
            {
                return NotFound();
            }
            return Ok(id);
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate/{absentDays}/{workedDays}")]
        public async Task<IActionResult> Calculate(int id, decimal absentDays, decimal workedDays)
        {
            if(absentDays < 0 || workedDays < 0)
            {
                return BadRequest();
            }
            double newAbsentDays = Decimal.ToDouble(absentDays);
            double newWorkDays = Decimal.ToDouble(workedDays);
            var salary = await _employeeService.CalculateSalary(id, newAbsentDays, newWorkDays);
            if (salary == null)
            {
                return NotFound();
            }

            return Ok(salary);

        }

    }
}
