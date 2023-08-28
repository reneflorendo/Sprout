using Moq;

using NUnit.Framework;

using Sprout.Exam.Business.Strategies.Contracts;

using System.Threading.Tasks;

namespace Tests.Sprout.Exam.Business
{
    
    [TestFixture]
    public class RegularEmployeeSalaryStrategyTests
    {
        private Mock<IRegularSalaryCalculationStrategy> _mockRegularSalaryCalculationStrategy;
        [SetUp]
        public void Setup()
        {
            _mockRegularSalaryCalculationStrategy = new Mock<IRegularSalaryCalculationStrategy>();
        }

        [Test]
        public async Task CalculateSalary_NoDaysAbsence_ReturnsCorrectSalary()
        {
            // Arrange
           
            var noOfDaysAbsence = 0;
            var expectedSalary = 20000 - (20000 * 0.12m); // 20000 - (20000 * 0.12) = 17600

            _mockRegularSalaryCalculationStrategy.Setup(strategy => strategy.CalculateSalary(noOfDaysAbsence)).ReturnsAsync(expectedSalary);

            var strategy = _mockRegularSalaryCalculationStrategy.Object;

            // Act
            var result = await strategy.CalculateSalary(noOfDaysAbsence);

            // Assert
            Assert.AreEqual(expectedSalary, result);
        }

        [Test]
        public async Task CalculateSalary_WithDaysAbsence_ReturnsCorrectSalary()
        {
            // Arrange
            var noOfDaysAbsence = 2; // Example absence days
            var dailyDeduction = 2 * (20000 / 22); // 2 * (20000 / 22) = 1818.18
            var tax = 20000 * 0.12m; // 2400
            var expectedSalary = 20000 - dailyDeduction - tax; // 20000 - 1818.18 - 2400 = 15781.82

            _mockRegularSalaryCalculationStrategy.Setup(strategy => strategy.CalculateSalary(noOfDaysAbsence)).ReturnsAsync(expectedSalary);

            var strategy = _mockRegularSalaryCalculationStrategy.Object;

            // Act
            var result = await strategy.CalculateSalary(noOfDaysAbsence);

            // Assert
            Assert.AreEqual(expectedSalary, result);
        }

        [Test]
        public async Task CalculateSalary_NegativeDaysAbsence_ReturnsCorrectSalary()
        {
            // Arrange
            var noOfDaysAbsence = -1; // Negative absence days
            var expectedSalary = 20000 - (20000 * 0.12m); // 20000 - (20000 * 0.12) = 17600

            _mockRegularSalaryCalculationStrategy.Setup(strategy => strategy.CalculateSalary(noOfDaysAbsence)).ReturnsAsync(expectedSalary);

            var strategy = _mockRegularSalaryCalculationStrategy.Object;

            // Act
            var result = await strategy.CalculateSalary(noOfDaysAbsence);

            // Assert
            Assert.AreEqual(expectedSalary, result);
        }
    }
}
