using System.Data;
using Dapper;
using YF.EnpalChallange.Api.Model;

namespace YF.EnpalChallange.Api.Persistence;

public class ManagerRepository(IDbConnection dbConnection)
{
    public async Task<SalesManager[]> FindManagersByFeatures(string[] products, string language, string rating)
    {
        var query = @"
            SELECT
                sm.id,
                sm.name,
                sm.languages,
                sm.products,
                sm.customer_ratings
            FROM
                sales_managers sm
            WHERE
                sm.languages @> ARRAY[@Languages]::varchar[]
                AND sm.products @> ARRAY[@Products]::varchar[]
                AND sm.customer_ratings @> ARRAY[@CustomerRatings]::varchar[]
        ";

        return (await dbConnection.QueryAsync<SalesManager>(query, new
        {
            Languages = language,
            Products = products,
            CustomerRatings = rating
        })).ToArray();
    }
}