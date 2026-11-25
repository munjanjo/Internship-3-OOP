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
    public void PrintMyReservations(Guest guest)
    {
        var reservations = HotelData.Reservations.Where(r => r.Guest == guest).ToList();
        if (reservations.Count != 0)
        {

            System.Console.WriteLine($"Rezervacije od {guest.FullName}:");
            foreach (var r in reservations)
            {
                Console.WriteLine($"[{r.Id}] Soba {r.Room.Number} ({r.Room.Category}) | {r.CheckIn:yyyy-MM-dd} -> {r.CheckOut:yyyy-MM-dd} | {r.Nights} noći | {r.TotalPrice:0.00} € | {r.Status}");
            }
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Nema postojecih rezervacija!");
            Console.ReadKey();
        }

    }
    public void AddNewReservation(Guest guest)
    {
        DateTime checkIn;
        while (true)
        {
            System.Console.WriteLine("Unesi datum dolaska (yyyy-MM-dd):");
            if (!DateTime.TryParseExact(Console.ReadLine().Trim(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out checkIn))
                System.Console.WriteLine("Neispravan format datuma!");
            else if (checkIn < DateTime.Today)
                System.Console.WriteLine("Datum dolaska ne moze biti u proslosti!");
            else
                break;
        }

        DateTime checkOut;
        while (true)
        {
            System.Console.WriteLine("Unesi datum odlaska (yyyy-MM-dd):");
            if (!DateTime.TryParseExact(Console.ReadLine().Trim(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out checkOut))
                System.Console.WriteLine("Neispravan format datuma!");
            else if (checkOut <= checkIn)
                System.Console.WriteLine("Datum odlaska mora biti nakon datuma dolaska!");
            else
                break;
        }
        Room? selectedRoom = new RoomServices().SelectFreeRoom(checkIn, checkOut);
        if (selectedRoom == null)
            return;
        Reservation reservation = new Reservation(guest, selectedRoom, checkIn, checkOut);
        System.Console.WriteLine($"Jeste li sigurni da zelite napraviti rezervaciju?\n[{reservation.Id}] Soba {reservation.Room.Number} ({reservation.Room.Category}) | {reservation.CheckIn:yyyy-MM-dd} -> {reservation.CheckOut:yyyy-MM-dd} | {reservation.Nights} noći | {reservation.TotalPrice:0.00} € \n (y/n)");
        if (Console.ReadLine().Trim().ToLower() != "y")
        {
            System.Console.WriteLine("Rezervacija prekinuta.");
            Console.ReadKey();
            return;
        }
        Console.Clear();
        HotelData.Reservations.Add(reservation);
        System.Console.WriteLine($"Uspjesno rezervirano! [{reservation.Id}] Soba {reservation.Room.Number} ({reservation.Room.Category}) | {reservation.CheckIn:yyyy-MM-dd} -> {reservation.CheckOut:yyyy-MM-dd} | {reservation.Nights} noći | {reservation.TotalPrice:0.00} € | {reservation.Status}");


        Console.ReadKey();
    }
    public void CancelReservation(Guest guest)
    {
        var reservations = HotelData.Reservations.Where(r => r.Guest == guest).ToList();
        if (reservations.Count == 0)
        {
            System.Console.WriteLine("Nema postojecih rezervacija!");
            Console.ReadKey();
            return;
        }

        for (int i = 0; i < reservations.Count; i++)
        {
            var r = reservations[i];
            Console.WriteLine($"{i + 1}. [{r.Id}] Soba {r.Room.Number} ({r.Room.Category}) | {r.CheckIn:yyyy-MM-dd} -> {r.CheckOut:yyyy-MM-dd} | {r.Nights} noći | {r.TotalPrice:0.00} € | {r.Status}");
        }
        int choice;
        System.Console.WriteLine("Odaberi rezervaciju za otkazivanje (Moguce je samo takazati 48h unaprijed. Redni broj):");
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > reservations.Count)
            System.Console.WriteLine("Neispravan odabir!");

        Reservation selected = reservations[choice - 1];

        if (selected.Status != ReservationStatus.Potvrdena)
        {
            System.Console.WriteLine("Moguce je otkazati samo potvrdenu rezervaciju!");
            Console.ReadKey();
            return;
        }
        if (selected.CheckIn - DateTime.Now <= TimeSpan.FromHours(48))
        {
            System.Console.WriteLine("Rezervaciju je moguce otkazati samo ako je dolazak za vise od 48h!");
            Console.ReadKey();
            return;
        }

        System.Console.WriteLine($"Jeste li sigurni da zelite otkazati rezervaciju? (y/n)\n [{selected.Id}] Soba {selected.Room.Number} ({selected.Room.Category}) | {selected.CheckIn:yyyy-MM-dd} -> {selected.CheckOut:yyyy-MM-dd} | {selected.Nights} noći | {selected.TotalPrice:0.00} €");
        if (Console.ReadLine().Trim().ToLower() != "y")
        {
            System.Console.WriteLine("Otkazivanje prekinuto.");
            Console.ReadKey();
            return;
        }

        selected.Cancel();
        System.Console.WriteLine("Rezervacija uspjesno otkazana.");
        Console.ReadKey();
    }




}