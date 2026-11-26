using Dump.Models;
using Dump.Services;
namespace Dump.UI;

public class MainMenu
{
    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1 - Gosti");
            Console.WriteLine("2 - Rezervacije");
            Console.WriteLine("3 - Sobe");
            Console.WriteLine("4 - Osoblje");
            Console.WriteLine("5 - Izlaz iz programa");
            Console.Write("Unos:");
            switch (Console.ReadLine())
            {
                case "1": new GuestMenu().Show(); break;
                case "2": new ReservationMenu().Show(); break;
                case "3": break;
                case "4": break;
                case "5": return;
                default:
                    Console.WriteLine("Neispravan unos.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}