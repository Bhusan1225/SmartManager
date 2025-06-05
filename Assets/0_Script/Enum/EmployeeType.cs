[System.Flags]
public enum EmployeeType
{
    None = 0,
    Manager = 1 << 0, // 1
    Employees = 1 << 1, // 2

    All = Manager | Employees // 3
}
