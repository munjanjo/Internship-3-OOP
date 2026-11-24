namespace Dump.Models;

public class Shift
{
    public Guid Id { get; }
    public string Name { get; }
    public DateTime Date { get; }
    public Employee Receptionist { get; }
    public List<Employee> Housekeepers { get; }
    public Employee Cook { get; }
    public DateTime CreatedAt { get; }
    public DateTime LastUpdated { get; private set; }

    public Shift(string name, DateTime date, Employee receptionist, List<Employee> housekeepers, Employee cook)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.Date = date;
        this.Receptionist = receptionist;
        this.Housekeepers = housekeepers;
        this.Cook = cook;
        this.CreatedAt = DateTime.Now;
        this.LastUpdated = DateTime.Now;
    }
}
