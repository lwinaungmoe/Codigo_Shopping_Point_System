using Mobile.API.ServiceCollectionExtensions;
using StackExchange.Redis;

namespace Mobile.API.Caching
{
    public class ConnectionHelper
    {

        static ConnectionHelper()
        {
            ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                string connectionstring = ConfigurationManagerSetting.AppSetting["RedisURL"];
                return ConnectionMultiplexer.Connect(connectionstring);
            });
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
