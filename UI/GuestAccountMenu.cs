using Dump.Models;
namespace Dump.UI;

public class GuestAccountMenu
{
    private Guest guest;

    public GuestAccountMenu(Guest guest)
    {
        this.guest = guest;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Prijavljen: {guest.FullName}");
            Console.WriteLine("1 - Moje rezervacije");
            Console.WriteLine("2 - Nova rezervacija");
            Console.WriteLine("3 - Pretraživanje soba");
            Console.WriteLine("4 - Otkazivanje rezervacije");
            Console.WriteLine("5 - Povratak na prethodni izbornik");
            Console.Write("Unos:");
            switch (Console.ReadLine())
            {
                case "1": break;
                case "2": break;
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