using System.Collections.Concurrent;

namespace RPSLS.Api.Data.Constants
{
    public class Connection
    {
        public static ConcurrentDictionary<string, List<string>> ConnectedUsers = new ConcurrentDictionary<string, List<string>>();
    }
}
