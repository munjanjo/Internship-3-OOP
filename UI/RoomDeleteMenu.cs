using Dump.Services;
namespace Dump.UI;

public class RoomDeleteMenu
{
    RoomServices services = new RoomServices();
    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1 - Po id-u");
            Console.WriteLine("2 - Po broju sobe");
            Console.WriteLine("3 - Povratak na prethodni izbornik");
            Console.Write("Unos:");
            switch (Console.ReadLine())
            {
                case "1": services.DeleteRoomById(); break;
                case "2": services.DeleteRoomByNumber(); break;
                case "3": return;
                default:
                    Console.WriteLine("Neispravan unos.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
