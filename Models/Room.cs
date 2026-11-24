namespace Dump.Models;

public class Room
{
    public Guid Id { get; }
    public int Number { get; }
    public int Floor { get; }
    public RoomCategory Category { get; }
    public int Capacity { get; }
    public decimal PricePerNight { get; }
    public DateTime CreatedAt { get; }
    public DateTime LastUpdated { get; private set; }

    public Room(int number, int floor, RoomCategory category, int capacity, decimal pricePerNight)
    {
        this.Id = Guid.NewGuid();
        this.Number = number;
        this.Floor = floor;
        this.Category = category;
        this.Capacity = capacity;
        this.PricePerNight = pricePerNight;
        this.CreatedAt = DateTime.Now;
        this.LastUpdated = DateTime.Now;
    }
}
