using Sprout.Exam.Business.Factories.Contracts;
using Sprout.Exam.Business.Strategies.Contracts;
using Sprout.Exam.Common.Enums;

using System;
using System.Collections.Generic;

namespace Sprout.Exam.Business.Factories
{
    public class SalaryCalculationFactory : ISalaryCalculationFactory
    {
        private readonly Dictionary<EmployeeType, ISalaryCalculationStrategy> _strategyMap;

        public SalaryCalculationFactory(IRegularSalaryCalculationStrategy regularSalaryCalculation,
                                        IContractualSalaryCalculationStrategy contractualSalaryCalculation)
        {
            _strategyMap = new Dictionary<EmployeeType, ISalaryCalculationStrategy>
            {
                { EmployeeType.Regular, regularSalaryCalculation },
                { EmployeeType.Contractual, contractualSalaryCalculation }
            };
        }
        public ISalaryCalculationStrategy GetSalaryCalculation(EmployeeType employeeType)
        {
            if (_strategyMap.TryGetValue(employeeType, out var strategy))
            {
                return strategy;
            }

            throw new ArgumentException("Unsupported employee type");
        }

    }
}
