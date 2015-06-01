using System.Threading.Tasks;
using Dapper;
using MediatR;

namespace BoxUserMgmt.DataAccess
{
    internal class UpdateAuthTokens : IAsyncRequest<AuthTokens>
    {
        public AuthTokens AuthTokens { get; set; }
    }

    internal class UpdateAuthTokensHandlerAsync :
        IAsyncRequestHandler<UpdateAuthTokens, AuthTokens>
    {
        private readonly IConnectionManager _connectionManager;

         public UpdateAuthTokensHandlerAsync(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public async Task<AuthTokens> Handle(UpdateAuthTokens request)
        {
            using (var conn = _connectionManager.OpenConnection())
            using (var trans = conn.BeginTransaction())
            {
                const string sql = "Update [AuthTokens] " +
                                   "Set " +
                                   "AccessToken = @AccessToken," +
                                   "RefreshToken = @RefreshToken," +
                                   "WHERE Id = @id";

                await conn.ExecuteAsync(sql, request.AuthTokens, trans);

                trans.Commit();
            }

            return request.AuthTokens;
        }
    }

}
