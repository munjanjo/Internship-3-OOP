using Dump.Services;

namespace Dump.UI;

public class StaffMenu
{
    StaffServices services = new StaffServices();
    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1 - Prikaz svih smjena");
            Console.WriteLine("2 - Kreiranje nove smjene");
            Console.WriteLine("3 - Dodavanje zaposlenika");
            Console.WriteLine("4 - Povratak na prethodni izbornik");
            Console.Write("Unos:");
            switch (Console.ReadLine())
            {
                case "1": services.PrintAllShifts(); break;
                case "2": services.CreateNewShift(); break;
                case "3": break;
                case "4": return;
                default:
                    Console.WriteLine("Neispravan unos.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}