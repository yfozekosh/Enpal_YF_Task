using System.Data;
using Dapper;
using YF.EnpalChallenge.Api.Model;
using YF.EnpalChallenge.Api.Persistence.Abstract;

namespace YF.EnpalChallenge.Api.Persistence;

public class CalendarRepository(IDbConnection dbConnection) : ICalendarRepository
{
    /// <summary>
    /// Gets the slots with managerId within the manager properties.
    /// The products, language and rating are used to filter the sales managers and should be present in the sales manager properties.
    /// Each property should match exact values in the sales manager properties.
    /// The result is sorted by sales manager id, start date and end date.
    /// </summary>
    /// <param name="date">Date to filter the slots. Time is ignored.</param>
    /// <param name="products">Products to filter the sales managers. Manager is taken if he matches all the passed products.</param>
    /// <param name="language">Language to filther the sales manages.</param>
    /// <param name="rating">Rating to filter the sales managers.</param>
    /// <returns>Dtos with the sales managers that match the properties and all the slots for a given day.</returns>
    public async Task<ManagerSlotsDto[]> GetSlotsWithingManagerProperties(DateTime date, string[] products, string language, string rating)
    {
        var query = @"
            SELECT
                sm.id as sales_manager_id,
                s.id as slot_id,
                s.booked as slot_booked,
                s.start_date as slot_start_date,
                s.end_date as slot_end_date
            FROM
                sales_managers sm
            inner join slots s on sm.id = s.sales_manager_id
            WHERE
                sm.languages @> ARRAY[@Languages]::varchar[]
                AND sm.products @> ARRAY[@Products]::varchar[]
                AND sm.customer_ratings @> ARRAY[@CustomerRatings]::varchar[]
                AND (DATE(@date) = DATE(s.start_date) OR DATE(@date) = DATE(s.end_date))
            ORDER BY sm.id, s.start_date, s.end_date;
        ";

        return (await dbConnection.QueryAsync<ManagerSlotsDto>(query, new
        {
            Languages = language,
            Products = products,
            CustomerRatings = rating,
            Date = date
        })).ToArray();
    }
}