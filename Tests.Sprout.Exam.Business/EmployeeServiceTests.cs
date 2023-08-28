using AutoMapper;

using Moq;

using NUnit.Framework;

using Sprout.Exam.Business.Factories.Contracts;
using Sprout.Exam.Business.Services;
using Sprout.Exam.Business.Strategies.Contracts;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.Repositories.Contracts;

using System;
using System.Threading.Tasks;

namespace Tests.Sprout.Exam.Business
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        private Mock<IEmployeeRepository> _mockEmployeeRepository;
        private Mock<ISalaryCalculationFactory> _mockSalaryCalculationFactory;
        private Mock<IMapper> _mockMapper;
        private EmployeeService _employeeService;

        [SetUp]
        public void Setup()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _mockSalaryCalculationFactory = new Mock<ISalaryCalculationFactory>();
            _mockMapper = new Mock<IMapper>();
            _employeeService = new EmployeeService(_mockEmployeeRepository.Object, _mockSalaryCalculationFactory.Object, _mockMapper.Object);
        }

        [Test]
        public async Task CalculateSalaryAsync_ValidInput_ReturnsCalculatedSalary()
        {
            // Arrange
            var employeeType = EmployeeType.Regular;
            var noOfAbsenceorWorkDays = 0;
            var expectedSalary = 20000;
            var mockStrategy = new Mock<IRegularSalaryCalculationStrategy>();
            mockStrategy.Setup(strategy => strategy.CalculateSalary(noOfAbsenceorWorkDays)).ReturnsAsync(expectedSalary);
            _mockSalaryCalculationFactory.Setup(factory => factory.GetSalaryCalculation(employeeType)).Returns(mockStrategy.Object);

            // Act
            var result = await _employeeService.CalculateSalaryAsync(employeeType, noOfAbsenceorWorkDays);

            // Assert
            Assert.AreEqual(expectedSalary, result);
        }

        [Test]
        public void CalculateSalaryAsync_NegativeAbsenceOrWorkDays_ThrowsArgumentException()
        {
            // Arrange
            var employeeType = EmployeeType.Regular;
            var negativeDays = -5;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _employeeService.CalculateSalaryAsync(employeeType, negativeDays));
        }

        [Test]
        public async Task CalculateSalaryAsync_StrategyThrowsException_PropagatesException()
        {
            // Arrange
            var employeeType = EmployeeType.Regular;
            var noOfAbsenceorWorkDays = 20;
            var mockStrategy = new Mock<ISalaryCalculationStrategy>();
            mockStrategy.Setup(strategy => strategy.CalculateSalary(noOfAbsenceorWorkDays)).ThrowsAsync(new Exception("Strategy exception"));
            _mockSalaryCalculationFactory.Setup(factory => factory.GetSalaryCalculation(employeeType)).Returns(mockStrategy.Object);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _employeeService.CalculateSalaryAsync(employeeType, noOfAbsenceorWorkDays));
        }
    }
}