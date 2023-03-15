using Sprout.Exam.Common.Enums;
using System;

namespace Sprout.Exam.WebApp.Helpers
{
    public static class CalculateEmployeeSalary
    {
        public static double ComputeSalary(EmployeeType employeeType, double absentDays, double workedDays)
        {
            if (employeeType == EmployeeType.Regular)
            {
                var monthlyPay = 20000.00;
                var perDaySalary = monthlyPay / 22;
                var totalSalary = monthlyPay - (perDaySalary * absentDays) - (monthlyPay * 0.12);
                return Math.Round(totalSalary, 2);
            }
            else if (employeeType == EmployeeType.Contractual)
            {
                var dailyPay = 500.00;
                var totalSalary = dailyPay * workedDays;
                return Math.Round(totalSalary, 2);
            }
            else
            {
                //employeetype doesnt exist
                return -1;
            }
            
        }
        public static double ComputeRegularSalary(double absentDays)
        {
            var monthlyPay = 20000.00;
            var perDaySalary = monthlyPay / 22;
            var totalSalary = monthlyPay - (perDaySalary * absentDays) - (monthlyPay * 0.12);
            return Math.Round(totalSalary, 2);
        }

        public static double ComputeContractualSalary(double workDays)
        {
            var dailyPay = 500.00;
            var totalSalary = dailyPay * workDays;
            return Math.Round(totalSalary, 2);
        }
    }
}
