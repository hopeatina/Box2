using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Box.V2;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Models;
using BoxUserMgmt.DataAccess;
using MediatR;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using CommonServiceLocator.NinjectAdapter.Unofficial;
using NLog;
using System.Linq;

namespace BoxUserMgmt
{
    public sealed class BoxUserMgmtClient
    {
         private static readonly Lazy<BoxUserMgmtClient> LazyInstance = new Lazy<BoxUserMgmtClient>(() => new BoxUserMgmtClient());

        private readonly IMediator _mediator;

        private static readonly Logger Log = LogManager.GetLogger(typeof (BoxUserMgmtClient).Name);

        private BoxUserMgmtClient()
        {
            var kernel = new StandardKernel(new DataAccessNinjectModule());

            var serviceLocator = new NinjectServiceLocator(kernel);

            var serviceLocatorProvider = new ServiceLocatorProvider(() => serviceLocator);
            
            kernel.Bind<ServiceLocatorProvider>().ToConstant(serviceLocatorProvider);

            _mediator = kernel.Get<IMediator>();

        }

        public static BoxUserMgmtClient Instance
        {
            get { return LazyInstance.Value; }
        }


        public async Task AssignUserIdTask()

        {  Log.Info("Assigning Employee Id");

            await Task.Delay(1000);

            var authTokens = await _mediator.SendAsync(new  GetAuthTokens());

            var session = new OAuthSession(authTokens.AccessToken, authTokens.RefreshToken, 0, string.Empty);

            var config = new BoxConfig(string.Empty, string.Empty, null);

            var client = new EnhancedBoxClient(config, session);

            for (int i = 0, beg = 500; i > 0; i++)
            {

                var limit = (uint) (int)beg;
                var offset = limit - 500; 
                var fields = new List<string>(new String[] {"FieldLogin", "FieldName", "FieldStatus"} );

                var trackcodes = new List<string>(new String[] {"tracking_codes"} );
                var collection = await client.UsersManager.GetEnterpriseUsersAsync(null, offset, limit, fields);

                    if (collection.Entries == null) 
                    break;
                // Linq query filter BoxCollection for activity

                var querycoll = from entries in collection.Entries where entries.TrackingCodes == null select entries;

                foreach (var entries in querycoll)
                {
                  var userrequest = new BoxUserRequest();
         
                  var userinfo = await Box.V2.Managers.BoxUsersManager.UpdateUserInformationAsync(userrequest, trackcodes);

                    userinfo.TrackingCodes = LDAPfoundvalue; 
                }
                
                // LDAP ID for same fieldlogin

                

                limit = limit + 500;
            } 

        }

    }
}
