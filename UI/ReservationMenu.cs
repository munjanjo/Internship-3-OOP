using Dump.Services;

namespace Dump.UI;

public class ReservationMenu
{
    ReservationServices reservationServices = new ReservationServices();
    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1 - Prikaz svih rezervacija");
            Console.WriteLine("2 - Dodavanje rezervacije");
            Console.WriteLine("3 - Pretraživanje rezervacija");
            Console.WriteLine("4 - Uređivanje rezervacije");
            Console.WriteLine("5 - Brisanje rezervacije");
            Console.WriteLine("6 - Povratak na prethodni izbornik");
            Console.Write("Unos:");
            switch (Console.ReadLine())
            {
                case "1": reservationServices.PrintAllReservations(); break;
                case "2": reservationServices.AddNewReservation(); break;
                case "3": break;
                case "4": break;
                case "5": break;
                case "6": return;
                default:
                    Console.WriteLine("Neispravan unos.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}