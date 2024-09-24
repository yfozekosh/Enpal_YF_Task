using System.Data;
using Dapper;
using YF.EnpalChallange.Api.Model.Contract;

namespace YF.EnpalChallange.Api.Persistence;

public class IdTypeHandler<T>(Func<int, T> factory) : SqlMapper.TypeHandler<T>
    where T : IIdType
{
    public override void SetValue(IDbDataParameter parameter, T? value)
    {
        parameter.Value = value?.Value;
    }

    public override T Parse(object value)
    {
        if (value is int id)
        {
            return factory(id);
        }

        throw new DataException("Invalid id type. Only int is supported.");
    }
}