using System.Threading.Tasks;

namespace Sprout.Exam.Business.Strategies.Contracts
{
    public interface ISalaryCalculationStrategy
    {
        Task<decimal> CalculateSalary(decimal noOfDays);
    }
}
