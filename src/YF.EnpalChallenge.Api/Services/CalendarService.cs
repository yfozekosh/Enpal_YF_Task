using YF.EnpalChallenge.Api.Model;
using YF.EnpalChallenge.Api.Persistence.Abstract;

namespace YF.EnpalChallenge.Api.Services;

public class CalendarService(ICalendarRepository calendarRepository)
{
    /// <summary>
    /// Gets the available slots for the given date, products, language and rating.
    /// </summary>
    /// <param name="date">Date to get available slots for.</param>
    /// <param name="products">Products for which slots are searched.</param>
    /// <param name="language">Language for which slots are searched.</param>
    /// <param name="rating">Customer rating for which slots are searched</param>
    /// <returns>Dictionary with available slot start times as key and available count as value.</returns>
    public async Task<Dictionary<DateTime, int>> GetAvailableSlots(DateTime date, string[] products, string language, string rating)
    {
        var allSlots = await calendarRepository.GetSlotsWithingManagerProperties(date, products, language, rating);

        var managerQueues = SplitIntoQueues(allSlots);

        // This algorithm relies on the sorting of the slots by the sales manager id, start date and end date.
        // By going through 2 queues (available and booked) we can discard available slots while they intersect with current booking
        // and accept only if the available slot is before current booked slot or there are no more booked slots in the queue.

        // This algorithm assumes that there can be only one slot for a given start time per manager.
        // In other case - hashset should be used instead of int.
        var availableCountPerSlot = new Dictionary<DateTime, int>();

        void IncreaseAvailableCount(DateTime slotStartDate)
        {
            if (!availableCountPerSlot.TryAdd(slotStartDate, 1))
            {
                availableCountPerSlot[slotStartDate]++;
            }
        }

        foreach (var (_, (bookedSlots, availableSlots)) in managerQueues)
        {
            ManagerSlotsDto? availableSlot = null;

            bookedSlots.TryDequeue(out ManagerSlotsDto? bookedSlot);
            while (availableSlots.Count > 0 || availableSlot != null)
            {
                if (availableSlot == null && !availableSlots.TryDequeue(out availableSlot))
                {
                    // No more available slots left for the manager.
                    break;
                }

                // If available slot starts and ends before the booked slot, we can increase the available count.
                if (bookedSlot == null || availableSlot.SlotEndDate <= bookedSlot.SlotStartDate)
                {
                    IncreaseAvailableCount(availableSlot.SlotStartDate);
                    availableSlot = null;
                    continue;
                }

                // If available slot is after current booked slot - take next booked slot.
                if (availableSlot.SlotStartDate >= bookedSlot.SlotEndDate && !bookedSlots.TryDequeue(out bookedSlot))
                {
                    bookedSlot = null;
                }

                if (bookedSlot == null)
                {
                    continue;
                }

                var isAvailableIntersectsBooked = availableSlot.SlotStartDate > bookedSlot.SlotStartDate && availableSlot.SlotStartDate < bookedSlot.SlotEndDate;
                var isBookedIntersectsAvailable = bookedSlot.SlotStartDate > availableSlot.SlotStartDate && bookedSlot.SlotStartDate < availableSlot.SlotEndDate;
                if (isAvailableIntersectsBooked || isBookedIntersectsAvailable)
                {
                    availableSlot = null;
                }
            }
        }

        return availableCountPerSlot;
    }

    private static Dictionary<int, (Queue<ManagerSlotsDto> BookedSlots, Queue<ManagerSlotsDto> AvailableSlots)> SplitIntoQueues(ManagerSlotsDto[] allSlots)
    {
        var managerQueues = new Dictionary<int, (Queue<ManagerSlotsDto> BookedSlots, Queue<ManagerSlotsDto> AvailableSlots)>();

        foreach (var slot in allSlots)
        {
            if (!managerQueues.ContainsKey(slot.SalesManagerId))
            {
                managerQueues[slot.SalesManagerId] = (new Queue<ManagerSlotsDto>(), new Queue<ManagerSlotsDto>());
            }

            if (slot.SlotBooked)
            {
                managerQueues[slot.SalesManagerId].BookedSlots.Enqueue(slot);
            }
            else
            {
                managerQueues[slot.SalesManagerId].AvailableSlots.Enqueue(slot);
            }
        }
        return managerQueues;
    }
}