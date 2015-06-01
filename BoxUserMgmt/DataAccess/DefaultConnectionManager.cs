using System.Data.Common;

namespace BoxUserMgmt.DataAccess
{
    internal class DefaultConnectionManager : IConnectionManager
    {
        private readonly IConnectionStringRetriever _connectionStringRetriever;
        
        private readonly DbProviderFactory _factory;

        public DefaultConnectionManager(IConnectionStringRetriever connectionStringRetriever)
        {
            _connectionStringRetriever = connectionStringRetriever;

            _factory = DbProviderFactories.GetFactory(connectionStringRetriever.Retrieve.ProviderName);
        }

        public DbConnection OpenConnection()
        {
            var conn = _factory.CreateConnection();
            
            if (conn == null) return null;
            
            conn.ConnectionString = _connectionStringRetriever.Retrieve.ToString();
            
            conn.Open();
            
            return conn;
        }
    }
}