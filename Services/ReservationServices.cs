using Dump.Data;
using Dump.Models;

namespace Dump.Services;

public class ReservationServices
{
    public void PrintAllReservations()
    {
        if (HotelData.Reservations.Count == 0)
        {
            System.Console.WriteLine("Nema rezervacija!");
            Console.ReadKey();
            return;
        }
        foreach (var reservation in HotelData.Reservations)
        {
            System.Console.WriteLine($"[{reservation.Id}] {reservation.Guest.FullName} | Soba {reservation.Room.Number} ({reservation.Room.Category}) | {reservation.CheckIn:yyyy-MM-dd} -> {reservation.CheckOut:yyyy-MM-dd} | {reservation.Nights} noci | {reservation.TotalPrice:0.00} € | {reservation.Status}");
        }
        Console.ReadKey();
    }
    public void AddNewReservation()
    {
        Guest? guest = ChooseGuest();
        if (guest != null)
            new GuestServices().AddNewReservation(guest);
    }
    private Guest? ChooseGuest()
    {
        Console.Clear();

        if (HotelData.Guests.Count == 0)
        {
            System.Console.WriteLine("Nema gostiju!");
            Console.ReadKey();
            return null;
        }
        var allGuests = HotelData.Guests.ToList();
        int i = 1;
        foreach (var guest in allGuests)
        {
            System.Console.WriteLine($"{i}. [{guest.Id}] {guest.FullName}");
            i++;
        }

        int choice;
        while (true)
        {
            System.Console.Write("Odaberi gosta(upisi redni broj): ");
            int.TryParse(Console.ReadLine(), out choice);
            if (choice > 0 && choice < i)
            {
                System.Console.WriteLine($"Uspjesno odabaran {allGuests[choice - 1].FullName}");
                Console.ReadKey();
                return allGuests[choice - 1];
            }
            else
            {
                System.Console.WriteLine("Krivi unos! Pokusaj ponovo.");
            }
        }

    }
    public void SearchById()
    {
        Reservation? reservation = FindById();
        if (reservation != null)
            PrintReservationDetails(reservation);
        Console.ReadKey();
    }

    public void SearchByLastName()
    {
        System.Console.WriteLine("Unesi prezime gosta:");
        string lastName = Console.ReadLine().Trim().ToLower();

        var result = HotelData.Reservations.Where(r => r.Guest.LastName.ToLower() == lastName).ToList();
        if (result.Count == 0)
            System.Console.WriteLine("Nema rezervacija za to prezime!");
        else
            foreach (var r in result)
                PrintReservationDetails(r);

        Console.ReadKey();
    }

    public void EditReservation()
    {
        Reservation? reservation = FindById();
        if (reservation == null)
        {
            Console.ReadKey();
            return;
        }

        PrintReservationDetails(reservation);

        if (reservation.Status != ReservationStatus.Potvrdena)
        {
            System.Console.WriteLine("Moguce je urediti samo potvrdenu rezervaciju!");
            Console.ReadKey();
            return;
        }

        DateTime checkIn;
        while (true)
        {
            System.Console.WriteLine("Unesi novi datum dolaska (yyyy-MM-dd):");
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
            System.Console.WriteLine("Unesi novi datum odlaska (yyyy-MM-dd):");
            if (!DateTime.TryParseExact(Console.ReadLine().Trim(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out checkOut))
                System.Console.WriteLine("Neispravan format datuma!");
            else if (checkOut <= checkIn)
                System.Console.WriteLine("Datum odlaska mora biti nakon datuma dolaska!");
            else
                break;
        }

        bool roomTaken = HotelData.Reservations.Any(r =>
            r != reservation &&
            r.Room == reservation.Room &&
            r.Status != ReservationStatus.Otkazana &&
            checkIn < r.CheckOut &&
            checkOut > r.CheckIn);

        if (roomTaken)
        {
            System.Console.WriteLine("Soba nije slobodna u tom razdoblju!");
            Console.ReadKey();
            return;
        }

        int newNights = (checkOut - checkIn).Days;
        decimal newPrice = newNights * reservation.Room.PricePerNight;
        System.Console.WriteLine($"Novi termin: {checkIn:yyyy-MM-dd} -> {checkOut:yyyy-MM-dd} | {newNights} noći | {newPrice:0.00} €");
        System.Console.WriteLine("Jeste li sigurni da zelite urediti rezervaciju? (y/n)");
        if (Console.ReadLine().Trim().ToLower() != "y")
        {
            System.Console.WriteLine("Uredivanje prekinuto.");
            Console.ReadKey();
            return;
        }

        reservation.ChangeDates(checkIn, checkOut);
        System.Console.WriteLine("Rezervacija uspjesno uredena.");
        PrintReservationDetails(reservation);
        Console.ReadKey();
    }

    private Reservation? FindById()
    {
        System.Console.WriteLine("Unesi id rezervacije:");
        if (!Guid.TryParse(Console.ReadLine().Trim(), out Guid id))
        {
            System.Console.WriteLine("Neispravan format id-a!");
            return null;
        }

        Reservation? reservation = HotelData.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null)
            System.Console.WriteLine("Rezervacija s tim id-em ne postoji!");
        return reservation;
    }

    private void PrintReservationDetails(Reservation r)
    {
        System.Console.WriteLine($"[{r.Id}] {r.Guest.FullName} | Soba {r.Room.Number} ({r.Room.Category}) | {r.CheckIn:yyyy-MM-dd} -> {r.CheckOut:yyyy-MM-dd} | {r.Nights} noći | {r.TotalPrice:0.00} € | {r.Status}");
        System.Console.WriteLine($"  Kreirano: {r.CreatedAt:yyyy-MM-dd HH:mm} | Zadnja promjena: {r.LastUpdated:yyyy-MM-dd HH:mm}");
    }
}