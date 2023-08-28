using System;
using System.Threading.Tasks;

using Sprout.Exam.Business.Strategies.Contracts;

namespace Sprout.Exam.Business.Strategies
{
    /// <summary>
    /// Calculate regular employees salary 
    /// </summary>
    public class RegularEmployeeSalaryStrategy : IRegularSalaryCalculationStrategy
    {
        private readonly decimal _salary = 20000;//Just declared as constant for now.
        private readonly decimal _tax = 0.12m;//Just declared as constant for now.
        private readonly int _noOfWorkDays = 22;//Just declared as constant for now.
        public Task<decimal> CalculateSalary(decimal noOfDaysAbsence)
        {
            //Calculate employee's salary deduction
            decimal dailyDeduction = noOfDaysAbsence > 0 
                                     ? Math.Round(noOfDaysAbsence * (_salary / _noOfWorkDays), 2)
                                     : 0;
            decimal tax = _salary * _tax;
            var salary = Math.Round(_salary - dailyDeduction - tax, 2);

            return Task.FromResult(salary);
        }
    }
}
