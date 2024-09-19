// using System;

// namespace DemoLab1.Lib
// {
//     public class PayrollCalculator
// {
//     private const double TaxRate = 0.15; // 15% flat tax rate for simplicity
//     private const double InsuranceRate = 0.05; // 5% insurance deduction
//     private const double FullTimeHourlyRate = 50; // $50/hour for full-time
//     private const double PartTimeHourlyRate = 30; // $30/hour for part-time
//     private const double ContractHourlyRate = 20; // $20/hour for contract

//     public double CalculateGrossPay(Employee employee)
//     {
//         double hourlyRate = GetHourlyRate(employee.EmployeeType);
//         double overtimePay = employee.OvertimeHours * employee.OvertimeRate * hourlyRate;
//         double grossPay = employee.BaseSalary + overtimePay + employee.Bonus + employee.Allowances;
//         return grossPay;
//     }

//     public double CalculateTax(double grossPay)
//     {
//         return grossPay * TaxRate;
//     }

//     public double CalculateInsurance(double grossPay)
//     {
//         return grossPay * InsuranceRate;
//     }

//     public double CalculateNetPay(Employee employee)
//     {
//         double grossPay = CalculateGrossPay(employee);
//         double tax = CalculateTax(grossPay);
//         double insurance = CalculateInsurance(grossPay);
//         double netPay = grossPay - tax - insurance;
//         return netPay;
//     }

//     private double GetHourlyRate(string employeeType)
//     {
//         return employeeType switch
//         {
//             "Full-Time" => FullTimeHourlyRate,
//             "Part-Time" => PartTimeHourlyRate,
//             "Contract" => ContractHourlyRate,
//             _ => throw new ArgumentException("Unknown employee type")
//         };
//     }
// }

// }

using System;

namespace DemoLab1.Lib
{
    public class PayrollCalculator
    {
        private const double TaxRateLow = 0.10; // 10% tax rate for gross pay < $4000
        private const double TaxRateHigh = 0.15; // 15% tax rate for gross pay >= $4000
        private const double InsuranceRate = 0.05; // 5% insurance deduction
        private const double FullTimeHourlyRate = 50; // $50/hour for full-time
        private const double PartTimeHourlyRate = 30; // $30/hour for part-time
        private const double ContractHourlyRate = 20; // $20/hour for contract

        public double CalculateGrossPay(Employee employee)
        {
            double hourlyRate = GetHourlyRate(employee.EmployeeType);
            double overtimePay = employee.OvertimeHours * employee.OvertimeRate * hourlyRate;
            double grossPay = employee.BaseSalary + overtimePay + employee.Bonus + employee.Allowances;
            return grossPay;
        }

        public double CalculateTax(double grossPay)
        {
            // Progressive tax calculation
            if (grossPay < 4000)
            {
                return grossPay * TaxRateLow;
            }
            else
            {
                return grossPay * TaxRateHigh;
            }
        }

        public double CalculateInsurance(double grossPay)
        {
            return grossPay * InsuranceRate;
        }

        public double CalculateNetPay(Employee employee)
        {
            if (employee.BaseSalary < 0)
            {
                throw new ArgumentException("Base salary cannot be negative");
            }
            double grossPay = CalculateGrossPay(employee);
            double tax = CalculateTax(grossPay);
            double insurance = CalculateInsurance(grossPay);
            double netPay = grossPay - tax - insurance;
            return netPay;
        }

        private double GetHourlyRate(string employeeType)
        {
            return employeeType switch
            {
                "Full-Time" => FullTimeHourlyRate,
                "Part-Time" => PartTimeHourlyRate,
                "Contract" => ContractHourlyRate,
                _ => throw new ArgumentException("Unknown employee type")
            };
        }
    }
}
