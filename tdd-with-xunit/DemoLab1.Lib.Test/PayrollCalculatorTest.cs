using System;
using Xunit;
using DemoLab1.Lib;

namespace DemoLab1.Lib.Test
{
    public class PayrollCalculatorTest
    {
        // Test case for Full-Time employee with all components (basic salary, overtime, bonus, allowances) -> Fail
        [Fact]
        public void TestFullTimeEmployeeWithAllComponents()
        {
            //Arrange
            var employee = new Employee("John Doe", "Full-Time", baseSalary: 5000, overtimeHours: 10, overtimeRate: 1.5, bonus: 1000, allowances: 500);
            var payrollCalculator = new PayrollCalculator();

            //Action
            double netPay = payrollCalculator.CalculateNetPay(employee);

            //Assert
            Assert.True(netPay > 0);
            Assert.InRange(netPay, 6000, 7000);
        }

        // Test case for Part-Time employee with overtime and bonus -> Fail
        [Fact]
        public void TestPartTimeEmployeeWithOvertimeAndBonus()
        {
            var employee = new Employee("Jane Smith", "Part-Time", baseSalary: 3000, overtimeHours: 8, overtimeRate: 1.25, bonus: 200, allowances: 150);
            var payrollCalculator = new PayrollCalculator();

            double netPay = payrollCalculator.CalculateNetPay(employee);

            Assert.InRange(netPay, 3500, 4000);
        }

        // Test case for Contract employee with no overtime, bonus, or allowances -> Pass
        [Fact]
        public void TestContractEmployeeWithBaseSalaryOnly()
        {
            var employee = new Employee("Mark Contract", "Contract", baseSalary: 1500);
            var payrollCalculator = new PayrollCalculator();

            double netPay = payrollCalculator.CalculateNetPay(employee);

            Assert.InRange(netPay, 1200, 1600);
        }

        // Test case for negative salary (invalid input) -> Pass
        [Fact]
        public void TestNegativeBaseSalary_ShouldThrowException()
        {
            var employee = new Employee("Invalid Employee", "Full-Time", baseSalary: -5000);
            var payrollCalculator = new PayrollCalculator();

            Assert.Throws<ArgumentException>(() => payrollCalculator.CalculateNetPay(employee));
        }

        // Test case for employee with zero overtime and no bonus -> Pass
        [Fact]
        public void TestFullTimeEmployeeWithNoOvertimeNoBonus()
        {
            var employee = new Employee("Alice Zero", "Full-Time", baseSalary: 4000, overtimeHours: 0, bonus: 0, allowances: 0);
            var payrollCalculator = new PayrollCalculator();

            double netPay = payrollCalculator.CalculateNetPay(employee);

            Assert.InRange(netPay, 3200, 3600);
        }

        // Test case for overtime calculation with different rates
        [Theory]
        [InlineData(1, 4, 1.5, 3000, 4500)]  // 4 overtime hours with 1.5x rate -> Fail
        [InlineData(1, 10, 2.0, 5000, 7000)] // 10 overtime hours with 2.0x rate -> Fail
        public void TestOvertimePayCalculation(double a, double overtimeHours, double overtimeRate, double baseSalary, double expectedGrossPay)
        {
            var employee = new Employee("Test Overtime", "Full-Time", baseSalary: baseSalary, overtimeHours: overtimeHours, overtimeRate: overtimeRate);
            var payrollCalculator = new PayrollCalculator();

            double grossPay = payrollCalculator.CalculateGrossPay(employee);

            Assert.Equal(expectedGrossPay, grossPay, 2);
        }

        // Test progressive tax scenario (when adding more tax logic)
        [Theory]
        [InlineData(3000, 0.10)]  // If gross pay < 4000, tax is 10% -> Fail
        [InlineData(6000, 0.15)]  // If gross pay >= 4000, tax is 15% -> Pass
        public void TestTaxBrackets(double grossPay, double expectedTaxRate)
        {
            var payrollCalculator = new PayrollCalculator();

            double calculatedTax = payrollCalculator.CalculateTax(grossPay);
            double expectedTax = grossPay * expectedTaxRate;

            Assert.Equal(expectedTax, calculatedTax, 2);
        }

        // Test for ensuring bonuses are applied correctly -> Pass
        [Fact]
        public void TestEmployeeWithBonus()
        {
            var employee = new Employee("Bonus Employee", "Full-Time", baseSalary: 4000, bonus: 1000);
            var payrollCalculator = new PayrollCalculator();

            double grossPay = payrollCalculator.CalculateGrossPay(employee);

            Assert.InRange(grossPay, 4900, 5100);
        }

        // Test employee with all allowances -> Fail
        [Fact]
        public void TestEmployeeWithAllowances()
        {
            var employee = new Employee("Allowance Employee", "Full-Time", baseSalary: 5000, allowances: 800);
            var payrollCalculator = new PayrollCalculator();

            double netPay = payrollCalculator.CalculateNetPay(employee);

            Assert.InRange(netPay, 5500, 6000);
        }
    }
}
