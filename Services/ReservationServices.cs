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
}