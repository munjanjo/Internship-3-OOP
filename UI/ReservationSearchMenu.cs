using Dump.Services;
namespace Dump.UI;

public class ReservationSearchMenu
{
    ReservationServices services = new ReservationServices();
    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1 - Po id-u");
            Console.WriteLine("2 - Po prezimenu gosta");
            Console.WriteLine("3 - Povratak na prethodni izbornik");
            Console.Write("Unos:");
            switch (Console.ReadLine())
            {
                case "1": services.SearchById(); break;
                case "2": services.SearchByLastName(); break;
                case "3": return;
                default:
                    Console.WriteLine("Neispravan unos.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}