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
        {
            System.Console.WriteLine("Soba s tim brojem ne postoji!");
            Console.ReadKey();
        }
        else
            PrintRooms(result);
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
        {
            System.Console.WriteLine("Nema soba te kategorije!");
            Console.ReadKey();
        }
        else
            PrintRooms(result);
    }

    public void PrintRooms(List<Room> rooms)
    {
        if (rooms.Count == 0)
        {
            System.Console.WriteLine("Nema niti jedna soba.");
            Console.ReadKey();
            return;
        }
        foreach (var room in rooms)
        {
            int reservationCount = HotelData.Reservations.Count(r => r.Room == room);
            Console.WriteLine($"[{room.Id}] Soba {room.Number} | {room.Floor}. kat | {room.Category} | {room.Capacity} osobe | {room.PricePerNight:0.00} €/noć | broj rezervacija: {reservationCount}");

        }
        Console.ReadKey();
        return;
    }
    public Room? SelectFreeRoom(DateTime checkIn, DateTime checkOut)
    {
        var freeRooms = HotelData.Rooms
            .Where(room => !HotelData.Reservations.Any(r =>
                r.Room == room &&
                r.Status != ReservationStatus.Otkazana &&
                checkIn < r.CheckOut &&
                checkOut > r.CheckIn))
            .ToList();

        if (freeRooms.Count == 0)
        {
            Console.WriteLine("Nema slobodnih soba u tom razdoblju!");
            Console.ReadKey();
            return null;
        }

        for (int i = 0; i < freeRooms.Count; i++)
        {
            var room = freeRooms[i];
            int reservationCount = HotelData.Reservations.Count(r => r.Room == room);
            Console.WriteLine($"{i + 1}. [{room.Id}] Soba {room.Number} | {room.Floor}. kat | {room.Category} | {room.Capacity} osobe | {room.PricePerNight:0.00} €/noć | broj rezervacija: {reservationCount}");
        }

        int choice;
        Console.WriteLine("Odaberi sobu (redni broj sobe):");
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > freeRooms.Count)
            Console.WriteLine("Neispravan odabir!");

        return freeRooms[choice - 1];
    }
    public void CreateNewRoom()
    {

        int roomNumber;
        while (true)
        {
            System.Console.WriteLine("Unesi broj sobe(jedinstven): ");
            if (int.TryParse(Console.ReadLine(), out roomNumber) && roomNumber > 0)
            {
                var exists = HotelData.Rooms.Any(r => r.Number == roomNumber);
                if (exists)
                {
                    System.Console.WriteLine("Postoji soba s tim brojem!");
                }
                else
                    break;
            }
            else
            {
                System.Console.WriteLine("Moras unijeti pozitivan broj!");
            }
        }

        System.Console.WriteLine("Unesi kat: ");
        int floor;
        while (!int.TryParse(Console.ReadLine(), out floor) || floor < 0)
            System.Console.WriteLine("Moras unijeti broj (0 ili vise)!");

        System.Console.WriteLine("1 - Standard");
        System.Console.WriteLine("2 - Deluxe");
        System.Console.WriteLine("3 - Suite");
        System.Console.WriteLine("Odaberi kategoriju (redni broj):");
        int category;
        while (!int.TryParse(Console.ReadLine(), out category) || category < 1 || category > 3)
            System.Console.WriteLine("Neispravan odabir!");
        RoomCategory selectedCategory = (RoomCategory)(category - 1);

        System.Console.WriteLine("Unesi kapacitet (broj osoba): ");
        int capacity;
        while (!int.TryParse(Console.ReadLine(), out capacity) || capacity <= 0)
            System.Console.WriteLine("Moras unijeti pozitivan broj!");

        System.Console.WriteLine("Unesi cijenu nocenja (npr. 120.50): ");
        decimal price;
        while (!decimal.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out price) || price <= 0)
            System.Console.WriteLine("Moras unijeti cijenu vecu od 0 (s tockom, npr. 120.50)!");

        System.Console.WriteLine($"Soba {roomNumber} | {floor}. kat | {selectedCategory} | {capacity} osobe | {price:0.00} €/noć");
        System.Console.WriteLine("Jeste li sigurni da zelite dodati sobu? (y/n)");
        if (Console.ReadLine().Trim().ToLower() != "y")
        {
            System.Console.WriteLine("Dodavanje sobe prekinuto.");
            Console.ReadKey();
            return;
        }

        Room room = new Room(roomNumber, floor, selectedCategory, capacity, price);
        HotelData.Rooms.Add(room);
        System.Console.WriteLine($"Soba {room.Number} uspjesno dodana.");
        Console.ReadKey();
    }

    public void DeleteRoomById()
    {
        System.Console.WriteLine("Unesi id sobe:");
        if (!Guid.TryParse(Console.ReadLine().Trim(), out Guid id))
        {
            System.Console.WriteLine("Neispravan format id-a!");
            Console.ReadKey();
            return;
        }

        Room? room = HotelData.Rooms.FirstOrDefault(r => r.Id == id);
        DeleteRoom(room);
    }

    public void DeleteRoomByNumber()
    {
        System.Console.WriteLine("Unesi broj sobe:");
        int number;
        while (!int.TryParse(Console.ReadLine(), out number))
            System.Console.WriteLine("Unesi broj!");

        Room? room = HotelData.Rooms.FirstOrDefault(r => r.Number == number);
        DeleteRoom(room);
    }

    private void DeleteRoom(Room? room)
    {
        if (room == null)
        {
            System.Console.WriteLine("Soba ne postoji!");
            Console.ReadKey();
            return;
        }

        bool hasActiveReservation = HotelData.Reservations.Any(r => r.Room == room && r.Status == ReservationStatus.Potvrdena);
        if (hasActiveReservation)
        {
            System.Console.WriteLine("Soba ima aktivnu (potvrdenu) rezervaciju i ne moze se obrisati!");
            Console.ReadKey();
            return;
        }

        System.Console.WriteLine($"Soba {room.Number} | {room.Floor}. kat | {room.Category} | {room.Capacity} osobe | {room.PricePerNight:0.00} €/noć");
        System.Console.WriteLine("Jeste li sigurni da zelite obrisati sobu? (y/n)");
        if (Console.ReadLine().Trim().ToLower() != "y")
        {
            System.Console.WriteLine("Brisanje sobe prekinuto.");
            Console.ReadKey();
            return;
        }

        HotelData.Rooms.Remove(room);
        System.Console.WriteLine($"Soba {room.Number} uspjesno obrisana.");
        Console.ReadKey();
    }
}
