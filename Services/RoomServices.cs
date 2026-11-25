using Dump.Models;
using Dump.Data;
namespace Dump.Services;

public class RoomServices
{
    public void SearchByNumber()
    {
        int number;
        System.Console.WriteLine("Unesi broj sobe:");
        while (!int.TryParse(Console.ReadLine(), out number))
            System.Console.WriteLine("Unesi broj!");

        var result = HotelData.Rooms.Where(r => r.Number == number).ToList();
        if (result.Count == 0)
            System.Console.WriteLine("Soba s tim brojem ne postoji!");
        else
            PrintRooms(result);

        Console.ReadKey();
    }

    public void SearchByCategory()
    {
        System.Console.WriteLine("1 - Standard");
        System.Console.WriteLine("2 - Deluxe");
        System.Console.WriteLine("3 - Suite");
        System.Console.WriteLine("Unesi redni broj:");
        int category;
        while (!int.TryParse(Console.ReadLine(), out category) || category < 1 || category > 3)
            System.Console.WriteLine("Neispravan odabir!");

        RoomCategory selectedCategory = (RoomCategory)(category - 1);
        var result = HotelData.Rooms.Where(r => r.Category == selectedCategory).ToList();
        if (result.Count == 0)
            System.Console.WriteLine("Nema soba te kategorije!");
        else
            PrintRooms(result);

        Console.ReadKey();
    }

    public void PrintRooms(List<Room> rooms)
    {
        foreach (var room in rooms)
        {
            int reservationCount = HotelData.Reservations.Count(r => r.Room == room);
            Console.WriteLine($"[{room.Id}] Soba {room.Number} | {room.Floor}. kat | {room.Category} | {room.Capacity} osobe | {room.PricePerNight:0.00} €/noć | broj rezervacija: {reservationCount}");
        }
    }
}