namespace Dump.Models;

public class Employee
{
    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public int BirthYear { get; }
    public Gender Gender { get; }
    public Position Position { get; }
    public DateTime CreatedAt { get; }
    public DateTime LastUpdated { get; private set; }

    public Employee(string firstName, string lastName, int birthYear, Gender gender, Position position)
    {
        this.Id = Guid.NewGuid();
        this.FirstName = firstName;
        this.LastName = lastName;
        this.BirthYear = birthYear;
        this.Gender = gender;
        this.Position = position;
        this.CreatedAt = DateTime.Now;
        this.LastUpdated = DateTime.Now;
    }
}
