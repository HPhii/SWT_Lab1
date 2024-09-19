using System;

namespace DemoLab1.Lib
{
    public class Employee
{
    public string Name { get; set; }
    public string EmployeeType { get; set; } // e.g., "Full-Time", "Part-Time", "Contract"
    public double BaseSalary { get; set; }
    public double OvertimeHours { get; set; }
    public double OvertimeRate { get; set; }
    public double Bonus { get; set; }
    public double Allowances { get; set; }

    public Employee(string name, string type, double baseSalary, double overtimeHours = 0, double overtimeRate = 1.5, double bonus = 0, double allowances = 0)
    {
        Name = name;
        EmployeeType = type;
        BaseSalary = baseSalary;
        OvertimeHours = overtimeHours;
        OvertimeRate = overtimeRate;
        Bonus = bonus;
        Allowances = allowances;
    }
}

}