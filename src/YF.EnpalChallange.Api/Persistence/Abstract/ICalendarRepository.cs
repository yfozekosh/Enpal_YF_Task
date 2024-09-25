using YF.EnpalChallange.Api.Model.Dto;

namespace YF.EnpalChallange.Api.Persistence.Abstract;

public interface ICalendarRepository
{
    Task<ManagerSlotsDto[]> GetSlotsWithingManagerProperties(DateTime date, string[] products, string language, string rating);
}