using Dump.Data;
using Dump.Services;
namespace Dump.UI;

public class RoomMenu
{
    RoomServices services = new RoomServices();
    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1 - Prikaz svih soba");
            Console.WriteLine("2 - Dodavanje nove sobe");
            Console.WriteLine("3 - Pretraživanje soba");
            Console.WriteLine("4 - Brisanje sobe");
            Console.WriteLine("5 - Povratak na prethodni izbornik");
            Console.Write("Unos:");
            switch (Console.ReadLine())
            {
                case "1": new RoomSortMenu().Show(); break;
                case "2": services.CreateNewRoom(); break;
                case "3": new RoomSearchMenu().Show(); break;
                case "4": new RoomDeleteMenu().Show(); break;
                case "5": return;
                default:
                    Console.WriteLine("Neispravan unos.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}