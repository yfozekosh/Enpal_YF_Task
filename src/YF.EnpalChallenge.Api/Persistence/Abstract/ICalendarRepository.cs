using YF.EnpalChallenge.Api.Model;

namespace YF.EnpalChallenge.Api.Persistence.Abstract;

public interface ICalendarRepository
{
    Task<ManagerSlotsDto[]> GetSlotsWithingManagerProperties(DateTime date, string[] products, string language, string rating);
}