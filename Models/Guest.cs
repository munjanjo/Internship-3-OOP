namespace Dump.Models;

public class Guest
{
    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public int BirthYear { get; }
    public string Email { get; }
    private string password;
    public DateTime CreatedAt { get; }
    public DateTime LastUpdated { get; private set; }

    public Guest(string firstName, string lastName, int birthYear, string email, string password)
    {
        this.Id = Guid.NewGuid();
        this.FirstName = firstName;
        this.LastName = lastName;
        this.BirthYear = birthYear;
        this.Email = email.Trim().ToLower();
        this.password = password;
        this.CreatedAt = DateTime.Now;
        this.LastUpdated = DateTime.Now;
    }

    public string FullName => $"{FirstName} {LastName}";

    public bool CheckPassword(string password)
    {
        return this.password == password;
    }

    private void UpdateTimestamp()
    {
        this.LastUpdated = DateTime.Now;
    }
}