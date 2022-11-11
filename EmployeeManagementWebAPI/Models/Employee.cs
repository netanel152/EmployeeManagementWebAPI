using System;
using System.Collections.Generic;

namespace EmployeeManagementWebAPI.Models;

public partial class Employee
{
    public string EmployeeId { get; set; } = null!;

    public string? EmployeeName { get; set; }

    public string? EmployeeRole { get; set; }

    public string? ManagerName { get; set; }
}
