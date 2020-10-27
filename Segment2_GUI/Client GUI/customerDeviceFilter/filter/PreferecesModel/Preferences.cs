using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace filter.PreferecesModel
{
    public static class Preferences
    {
        static string getLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return null;
        }
        public static void SaveFilterPreferencesForIp(DataModels.FilterDataModel fil)
        {
            string ip = getLocalIpAddress();
            ServerConnection.Filters.AddFilterPreferencesToServer(ip,fil);
        }
        
        
        public static DataModels.FilterDataModel getFilterPreferencesByIp()
        {
            string ip = getLocalIpAddress();
            return ServerConnection.Filters.getFilterPreferenceByIpFromServer(ip);
        }
        

    }
}
