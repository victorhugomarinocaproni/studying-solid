using System.Data.Common;

namespace Solid.Contracts;

public interface IDbContext
{
    DbConnection Connection { get; }
    void Connect();
}