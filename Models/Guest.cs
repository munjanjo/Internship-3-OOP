namespace Dump.Models;

public class Guest
{
    public Guid Id { get; }
    public required string FirstName { get; }
    public required string LastName { get; }
    public required int BirthYear { get; }
    public required string Email { get; }
    private required string password;
    public DateTime CreatedAt { get; }
    public DateTime LastUpdated { get; private set; }

    public Guest(string firstName, string lastName, int birthYear, string email, string password)
    {
        if (password.Length < 6)
            throw new ArgumentException("Lozinka mora imati barem 6 znakova.");
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