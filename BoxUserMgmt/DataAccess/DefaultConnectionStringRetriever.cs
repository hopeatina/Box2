using System.Configuration;

namespace BoxUserMgmt.DataAccess
{
    internal class DefaultConnectionStringRetriever : IConnectionStringRetriever
    {
        private readonly ConnectionStringSettings _connectionString;

        public DefaultConnectionStringRetriever()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["boxusermgmt"];
        }

        public ConnectionStringSettings Retrieve
        {
            get { return _connectionString; }
        }
    }
}