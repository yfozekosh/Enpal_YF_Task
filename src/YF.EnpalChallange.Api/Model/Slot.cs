namespace YF.EnpalChallange.Api.Model;

public class Slot
{
    public SlotId Id { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool Booked { get; set; }

    public int SalesManagerId { get; set; }
}