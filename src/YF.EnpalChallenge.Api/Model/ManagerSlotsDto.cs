namespace YF.EnpalChallenge.Api.Model;

public class ManagerSlotsDto
{
    public int SalesManagerId { get; set; }

    public int SlotId { get; set; }

    public bool SlotBooked { get; set; }

    public DateTime SlotStartDate { get; set; }

    public DateTime SlotEndDate { get; set; }
}