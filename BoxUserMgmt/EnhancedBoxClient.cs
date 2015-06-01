using Box.V2;
using Box.V2.Auth;
using Box.V2.Config;

namespace BoxUserMgmt
{
    // Only needed because box has not been updated on nuget. Remove once box is updated on nuget
    public class EnhancedBoxClient : BoxClient
    {
        public EnhancedBoxClient(IBoxConfig boxConfig) : this(boxConfig, null)
        {
        }

        public EnhancedBoxClient(IBoxConfig boxConfig, OAuthSession authSession) : base(boxConfig, authSession)
        {
            UsersManager = new EnhancedBoxUsersManager(Config, _service, _converter, Auth);
        }

        public new EnhancedBoxUsersManager UsersManager { get; private set; }
    }
}
