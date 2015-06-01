using System.Data.Common;

namespace BoxUserMgmt.DataAccess
{
    interface IConnectionManager
    {
        DbConnection OpenConnection();
    }
}
