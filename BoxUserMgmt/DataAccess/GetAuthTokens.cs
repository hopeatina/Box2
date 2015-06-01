using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MediatR;

namespace BoxUserMgmt.DataAccess
{
    internal class GetAuthTokens : IAsyncRequest<AuthTokens>
    {
    }

    internal class AuthTokens
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    internal class GetAuthTokensHandler : IAsyncRequestHandler<GetAuthTokens, AuthTokens>
    {
        private readonly IConnectionManager _connectionManager;

        public GetAuthTokensHandler (IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public async Task<AuthTokens> Handle(GetAuthTokens request)
        {
            using (var conn = _connectionManager.OpenConnection())
            {
                const string sql = "Select TOP 1 * From AuthTokens";

                var result = conn.Query<AuthTokens>(sql);

                return result.Single();
            }
        }

    }
}
