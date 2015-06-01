using System.Configuration;

namespace BoxUserMgmt.DataAccess
{
    internal interface IConnectionStringRetriever
    {
        ConnectionStringSettings Retrieve { get; }
    }
}