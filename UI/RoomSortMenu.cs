using Dump.Services;
namespace Dump.UI;

public class RoomSortMenu
{
    RoomServices services = new RoomServices();
    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1 - Po vremenu dodavanja");
            Console.WriteLine("2 - Po cijeni nocenja uzlazno");
            Console.WriteLine("3 - Po cijeni nocenja silazno");
            Console.WriteLine("4 - Po broju rezervacija uzlazno");
            Console.WriteLine("5 - Po broju rezervacija silazno");
            Console.WriteLine("6 - Povratak na prethodni izbornik");
            Console.Write("Unos:");
            switch (Console.ReadLine())
            {
                case "1": services.PrintRoomsByCreated(); break;
                case "2": services.PrintRoomsByPrice(false); break;
                case "3": services.PrintRoomsByPrice(true); break;
                case "4": services.PrintRoomsByReservationCount(false); break;
                case "5": services.PrintRoomsByReservationCount(true); break;
                case "6": return;
                default:
                    Console.WriteLine("Neispravan unos.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}