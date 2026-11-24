using Dump.Models;
using Dump.Data;
namespace Dump.Services;

public class GuestServices
{
    public void Register()
    {
        string FirstName;
        while (true)
        {
            System.Console.WriteLine("Unesi ime: ");
            FirstName = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(FirstName))
                break;
            else
                System.Console.WriteLine("Moras unjeti ime!");

        }

        string LastName;
        while (true)
        {
            System.Console.WriteLine("Unesi prezime: ");
            LastName = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(LastName))
                break;
            else
                System.Console.WriteLine("Moras unjeti prezime!");

        }

        int BirthYear;
        while (true)
        {
            System.Console.WriteLine("Unesi godinu rodenja");
            if (int.TryParse(Console.ReadLine(), out BirthYear))
                break;
            else
                System.Console.WriteLine("Unesi broj!");
        }


        string Email;
        while (true)
        {
            System.Console.WriteLine("Unesi email:");
            Email = Console.ReadLine().Trim().ToLower();
            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains('@'))
                System.Console.WriteLine("Moras valjani unijeti email!");
            else if (HotelData.Guests.Any(g => g.Email == Email))
                System.Console.WriteLine("Gost s tim emailom vec postoji!");
            else
                break;
        }
        string Password;

        while (true)
        {
            System.Console.WriteLine("Unesi lozinku(min 6 znakova):");
            Password = Console.ReadLine().Trim();
            if (Password.Length >= 6)
                break;
            else
                System.Console.WriteLine("Unesi lozinku duzu od 5 znakova!");

        }

        System.Console.WriteLine("Jeste li sigurni da zelite registrirati gosta? (y/n)");
        if (Console.ReadLine().Trim().ToLower() != "y")
        {
            System.Console.WriteLine("Registracija prekinuta.");
            Console.ReadKey();
            return;
        }

        Guest guest = new Guest(FirstName, LastName, BirthYear, Email, Password);
        HotelData.Guests.Add(guest);
        System.Console.WriteLine($"{guest.FullName} je uspjesno registriran.");
        Console.ReadKey();
    }
    public Guest Login()
    {
        while (true)
        {
            System.Console.WriteLine("Unesi email:");
            string Email = Console.ReadLine().Trim().ToLower();
            System.Console.WriteLine("Unesi lozinku:");
            string Password = Console.ReadLine().Trim();

            Guest? guest = HotelData.Guests.FirstOrDefault(g => g.Email == Email);
            if (guest != null && guest.CheckPassword(Password))
            {
                System.Console.WriteLine($"Uspjesna prijava: {guest.FullName}");
                Console.ReadKey();
                return guest;
            }

            System.Console.WriteLine("Pogresan email ili lozinka! Pokusaj ponovno.");
        }
    }


}