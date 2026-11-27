using Dump.Data;
using Dump.Models;

namespace Dump.Services;

public class StaffServices()
{
    public void PrintAllShifts()
    {
        var allShifts = HotelData.Shifts;
        if (allShifts.Count == 0)
        {
            System.Console.WriteLine("Nema niti jedne smjene!");
            Console.ReadKey();
            return;
        }
        foreach (var shift in allShifts)
        {
            System.Console.WriteLine($"Smjena: {shift.Name} {shift.Date:yyyy-MM-dd}");
            System.Console.WriteLine($"  Recepcionar: {shift.Receptionist.FirstName} {shift.Receptionist.LastName} ({shift.Receptionist.BirthYear})");
            foreach (var housekeeper in shift.Housekeepers)
            {
                System.Console.WriteLine($"  Sobarica: {housekeeper.FirstName} {housekeeper.LastName} ({housekeeper.BirthYear})");
            }
            System.Console.WriteLine($"  Kuhar: {shift.Cook.FirstName} {shift.Cook.LastName} ({shift.Cook.BirthYear})");
            System.Console.WriteLine();
        }
        Console.ReadKey();
        return;
    }
    public void CreateNewShift()
    {
        string name;
        while (true)
        {
            System.Console.WriteLine("Unesi naziv smjene (npr. Jutarnja):");
            name = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(name))
                break;
            System.Console.WriteLine("Moras unijeti naziv!");
        }

        DateTime date;
        while (true)
        {
            System.Console.WriteLine("Unesi datum smjene (yyyy-MM-dd):");
            if (DateTime.TryParseExact(Console.ReadLine().Trim(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out date))
                break;
            System.Console.WriteLine("Neispravan format datuma!");
        }

        var available = HotelData.Employees
            .Where(e => !HotelData.Shifts.Any(s =>
                s.Receptionist == e || s.Cook == e || s.Housekeepers.Contains(e)))
            .ToList();

        var receptionists = available.Where(e => e.Position == Position.Recepcionar).ToList();
        var housekeepers = available.Where(e => e.Position == Position.Sobarica).ToList();
        var cooks = available.Where(e => e.Position == Position.Kuhar).ToList();

        if (receptionists.Count < 1 || housekeepers.Count < 2 || cooks.Count < 1)
        {
            System.Console.WriteLine("Nema dovoljno slobodnih zaposlenika za novu smjenu!");
            System.Console.WriteLine($"Slobodno: recepcionara {receptionists.Count}/1, sobarica {housekeepers.Count}/2, kuhara {cooks.Count}/1");
            Console.ReadKey();
            return;
        }

        Employee receptionist = SelectEmployee(receptionists, "recepcionara");

        Employee housekeeper1 = SelectEmployee(housekeepers, "prvu sobaricu/sobara");
        housekeepers.Remove(housekeeper1);
        Employee housekeeper2 = SelectEmployee(housekeepers, "drugu sobaricu/sobara");

        Employee cook = SelectEmployee(cooks, "kuhara");

        System.Console.WriteLine($"Smjena: {name} {date:yyyy-MM-dd}");
        System.Console.WriteLine($"  Recepcionar: {receptionist.FirstName} {receptionist.LastName} ({receptionist.BirthYear})");
        System.Console.WriteLine($"  Sobarica: {housekeeper1.FirstName} {housekeeper1.LastName} ({housekeeper1.BirthYear})");
        System.Console.WriteLine($"  Sobarica: {housekeeper2.FirstName} {housekeeper2.LastName} ({housekeeper2.BirthYear})");
        System.Console.WriteLine($"  Kuhar: {cook.FirstName} {cook.LastName} ({cook.BirthYear})");
        System.Console.WriteLine("Jeste li sigurni da zelite kreirati smjenu? (y/n)");
        if (Console.ReadLine().Trim().ToLower() != "y")
        {
            System.Console.WriteLine("Kreiranje smjene prekinuto.");
            Console.ReadKey();
            return;
        }

        Shift shift = new Shift(name, date, receptionist, new List<Employee> { housekeeper1, housekeeper2 }, cook);
        HotelData.Shifts.Add(shift);
        System.Console.WriteLine($"Smjena {shift.Name} uspjesno kreirana.");
        Console.ReadKey();
    }

    private Employee SelectEmployee(List<Employee> employees, string title)
    {
        System.Console.WriteLine($"Odaberi {title}:");
        for (int i = 0; i < employees.Count; i++)
            System.Console.WriteLine($"{i + 1}. {employees[i].FirstName} {employees[i].LastName} ({employees[i].BirthYear})");

        System.Console.Write("Unesi redni broj: ");
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > employees.Count)
            System.Console.Write("Neispravan odabir! Unesi redni broj: ");

        return employees[choice - 1];
    }
    public void AddEmployee()
    {
        string firstName;
        while (true)
        {
            System.Console.WriteLine("Unesi ime:");
            firstName = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(firstName))
                break;
            System.Console.WriteLine("Moras unijeti ime!");
        }

        string lastName;
        while (true)
        {
            System.Console.WriteLine("Unesi prezime:");
            lastName = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(lastName))
                break;
            System.Console.WriteLine("Moras unijeti prezime!");
        }

        int birthYear;
        System.Console.WriteLine("Unesi godinu rodenja:");
        while (!int.TryParse(Console.ReadLine(), out birthYear) || birthYear < 1900 || birthYear > DateTime.Now.Year)
            System.Console.WriteLine($"Unesi ispravnu godinu (1900 - {DateTime.Now.Year})!");

        System.Console.WriteLine("1 - Musko");
        System.Console.WriteLine("2 - Zensko");
        System.Console.Write("Odaberi spol (unesi redni broj): ");
        int genderChoice;
        while (!int.TryParse(Console.ReadLine(), out genderChoice) || genderChoice < 1 || genderChoice > 2)
            System.Console.Write("Neispravan odabir! Unesi redni broj: ");
        Gender gender = (Gender)(genderChoice - 1);

        System.Console.WriteLine("1 - Recepcionar");
        System.Console.WriteLine("2 - Sobarica/Sobar");
        System.Console.WriteLine("3 - Kuhar");
        System.Console.Write("Odaberi poziciju (unesi redni broj): ");
        int positionChoice;
        while (!int.TryParse(Console.ReadLine(), out positionChoice) || positionChoice < 1 || positionChoice > 3)
            System.Console.Write("Neispravan odabir! Unesi redni broj: ");
        Position position = (Position)(positionChoice - 1);

        System.Console.WriteLine($"{firstName} {lastName} ({birthYear}) | {gender} | {position}");
        System.Console.WriteLine("Jeste li sigurni da zelite dodati zaposlenika? (y/n)");
        if (Console.ReadLine().Trim().ToLower() != "y")
        {
            System.Console.WriteLine("Dodavanje zaposlenika prekinuto.");
            Console.ReadKey();
            return;
        }

        Employee employee = new Employee(firstName, lastName, birthYear, gender, position);
        HotelData.Employees.Add(employee);
        System.Console.WriteLine($"Zaposlenik {employee.FirstName} {employee.LastName} uspjesno dodan.");
        Console.ReadKey();
    }
}