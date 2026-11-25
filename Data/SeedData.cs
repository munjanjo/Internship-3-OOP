using Dump.Models;
namespace Dump.Data;

public static class SeedData
{
    public static void Seed()
    {
        Guest ana = new Guest("Ana", "Anić", 1995, "ana@gmail.com", "ana123");
        Guest ivan = new Guest("Ivan", "Horvat", 1988, "ivan@gmail.com", "ivan123");
        Guest maja = new Guest("Maja", "Babić", 2000, "maja@gmail.com", "maja123");
        HotelData.Guests.Add(ana);
        HotelData.Guests.Add(ivan);
        HotelData.Guests.Add(maja);

        Room room101 = new Room(101, 1, RoomCategory.Standard, 2, 70m);
        Room room102 = new Room(102, 1, RoomCategory.Standard, 1, 55m);
        Room room204 = new Room(204, 2, RoomCategory.Deluxe, 2, 120m);
        Room room205 = new Room(205, 2, RoomCategory.Deluxe, 3, 140m);
        Room room301 = new Room(301, 3, RoomCategory.Suite, 4, 250m);
        HotelData.Rooms.Add(room101);
        HotelData.Rooms.Add(room102);
        HotelData.Rooms.Add(room204);
        HotelData.Rooms.Add(room205);
        HotelData.Rooms.Add(room301);

        DateTime today = DateTime.Today;
        HotelData.Reservations.Add(new Reservation(ana, room204, today.AddDays(10), today.AddDays(13)));
        HotelData.Reservations.Add(new Reservation(ana, room101, today.AddDays(1), today.AddDays(3)));
        HotelData.Reservations.Add(new Reservation(ivan, room301, today.AddDays(20), today.AddDays(25)));
        HotelData.Reservations.Add(new Reservation(ivan, room102, today.AddDays(-7), today.AddDays(-4)));
        HotelData.Reservations.Add(new Reservation(maja, room205, today.AddDays(5), today.AddDays(7)));

        HotelData.Employees.Add(new Employee("Marko", "Marić", 1990, Gender.Musko, Position.Recepcionar));
        HotelData.Employees.Add(new Employee("Iva", "Ivić", 1995, Gender.Zensko, Position.Sobarica));
        HotelData.Employees.Add(new Employee("Petra", "Perić", 1988, Gender.Zensko, Position.Sobarica));
        HotelData.Employees.Add(new Employee("Luka", "Lukić", 1985, Gender.Musko, Position.Kuhar));
    }
}