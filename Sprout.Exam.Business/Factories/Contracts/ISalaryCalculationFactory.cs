using Sprout.Exam.Business.Strategies.Contracts;
using Sprout.Exam.Common.Enums;

namespace Sprout.Exam.Business.Factories.Contracts
{
    public interface ISalaryCalculationFactory
    {
        ISalaryCalculationStrategy GetSalaryCalculation(EmployeeType employeeType);
    }
}
