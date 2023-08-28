using System;
using System.Threading.Tasks;

using Sprout.Exam.Business.Strategies.Contracts;

namespace Sprout.Exam.Business.Strategies
{
    /// <summary>
    /// Calculate contractual employees salary
    /// </summary>
    public class ContractualEmployeeSalaryStrategy : IContractualSalaryCalculationStrategy
    {
        private const decimal _ratePerDay = 500; //Just declared as constant for now.
        public Task<decimal> CalculateSalary(decimal noOfWorkDays)
        {
            var salary = Math.Round(_ratePerDay * noOfWorkDays, 2);

            return Task.FromResult(salary);
        }
    }
}
