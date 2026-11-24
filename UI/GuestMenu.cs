using Dump.Services;
namespace Dump.UI;

public class GuestMenu
{
    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1 - Registracija");
            Console.WriteLine("2 - Prijava");
            Console.WriteLine("3 - Povratak na prethodni izbornik");
            Console.Write("Unos:");
            switch (Console.ReadLine())
            {
                case "1": new GuestServices().Register(); break;
                case "2": new GuestServices().Login(); break;
                case "3": return;
                default:
                    Console.WriteLine("Neispravan unos.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
