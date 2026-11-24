namespace Dump.Models;

public class Reservation
{
    public Guid Id { get; }
    public Guest Guest { get; }
    public Room Room { get; }
    public DateTime CheckIn { get; private set; }
    public DateTime CheckOut { get; private set; }
    public int Nights { get; private set; }
    public decimal TotalPrice { get; private set; }
    public ReservationStatus Status { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime LastUpdated { get; private set; }

    public Reservation(Guest guest, Room room, DateTime checkIn, DateTime checkOut)
    {
        this.Id = Guid.NewGuid();
        this.Guest = guest;
        this.Room = room;
        this.CheckIn = checkIn;
        this.CheckOut = checkOut;
        this.Nights = (checkOut.Date - checkIn.Date).Days;
        this.TotalPrice = this.Nights * room.PricePerNight;
        this.Status = ReservationStatus.Potvrdena;
        this.CreatedAt = DateTime.Now;
        this.LastUpdated = DateTime.Now;
    }
}
