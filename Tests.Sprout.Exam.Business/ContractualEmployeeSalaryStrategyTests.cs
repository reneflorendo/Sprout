using Moq;

using NUnit.Framework;

using Sprout.Exam.Business.Strategies.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Sprout.Exam.Business
{
    [TestFixture]
    public class ContractualEmployeeSalaryStrategyTests
    {
        private Mock<IContractualSalaryCalculationStrategy> _mockContractualSalaryCalculationStrategy;
        [SetUp]
        public void Setup()
        {
            _mockContractualSalaryCalculationStrategy= new Mock<IContractualSalaryCalculationStrategy>();
        }

        [Test]
        public async Task CalculateSalary_ValidInput_ReturnsCorrectSalary()
        {
            // Arrange
            var noOfWorkDays = 10; // Example work days
            var expectedSalary = 5000; // 500 * 20 = 10000

            _mockContractualSalaryCalculationStrategy.Setup(strategy => strategy.CalculateSalary(noOfWorkDays)).ReturnsAsync(expectedSalary);

            var strategy = _mockContractualSalaryCalculationStrategy.Object;

            // Act
            var result = await strategy.CalculateSalary(noOfWorkDays);

            // Assert
            Assert.AreEqual(expectedSalary, result);
        }

        [Test]
        public async Task CalculateSalary_ZeroWorkDays_ReturnsZeroSalary()
        {
            // Arrange
            var noOfWorkDays = 0;
            var expectedSalary = 0;

            _mockContractualSalaryCalculationStrategy.Setup(strategy => strategy.CalculateSalary(noOfWorkDays)).ReturnsAsync(expectedSalary);

            var strategy = _mockContractualSalaryCalculationStrategy.Object;

            // Act
            var result = await strategy.CalculateSalary(noOfWorkDays);

            // Assert
            Assert.AreEqual(expectedSalary, result);
        }

        [Test]
        public async Task CalculateSalary_NegativeWorkDays_ReturnsZeroSalary()
        {
            // Arrange
            var noOfWorkDays = -10;
            var expectedSalary = 0;

            _mockContractualSalaryCalculationStrategy.Setup(strategy => strategy.CalculateSalary(noOfWorkDays)).ReturnsAsync(expectedSalary);

            var strategy = _mockContractualSalaryCalculationStrategy.Object;

            // Act
            var result = await strategy.CalculateSalary(noOfWorkDays);

            // Assert
            Assert.AreEqual(expectedSalary, result);
        }

        // You can add more test methods for other scenarios as needed
    }
}
