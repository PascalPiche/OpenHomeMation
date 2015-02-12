using OpenZWaveDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWaveLib.Tools
{
    public class NotificationTool
    {
        public static string GetNodeName(ZWNotification n, ZWManager mng)
        {
            string result = mng.GetNodeName(n.GetHomeId(), n.GetNodeId());
            if (String.IsNullOrEmpty(result))
            {
                result = mng.GetNodeProductName(n.GetHomeId(), n.GetNodeId());
            }
            if (string.IsNullOrEmpty(result))
            {
                result = "Unknow ZWave device";
            }
            return result;
        }

        public static string MakeNodeKey(ZWNotification n)
        {
            return n.GetHomeId().ToString() + "-" + n.GetNodeId().ToString();
        }
    }
}
