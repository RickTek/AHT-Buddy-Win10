using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AHT_Buddy
{
    abstract class code : ObservableCollection<code>
    {
        public static Dictionary<int, string> ProblemCode = new Dictionary<int, string>()
            {
                {15003, "WG - No Internet - All" },
                {15002, "WG - No Internet - Multiple" },
                {15001, "WG - No Internet - Single" },
                {1126, "WG - Login/PW Issues" },
                {15006, "WiFi - No Connection - All" },
                {15005, "WiFi - No Connection - Multiple" },
                {15004, "WiFi - No Connection - Single" },
                {14179, "WG - Bridge Mode Toggle" },
                {15567, "WiFi/HW - Connected to WG, No Internet" },
                {15730, "Update WiFi Password" },
                {15731, "Update WiFi SSID" },
                {1129, "Change WiFi Settings/Security" },
                {14194,"WG - Slow Connectivity" },
                {14966, "WG - Intermittent Connectivity" },
                {16176, "Stuck in Captive Portal" }
            };
        public static Dictionary<int, string> cc1500x = new Dictionary<int, string>()
            {
                {165, "Customer Education" },
                {1559, "Forgot WiFi Password" },
                {347, "Hardware/Software Failure or Configuration" },
                {1083, "Customer Ticket Follow up" },
                {825, "Truck Roll" }
            };
        public static Dictionary<int, string> cc1126 = new Dictionary<int, string>()
            {
                {1559, "Forgot WiFi Password" },
                {1083, "Customer Ticket Follow Up" },
                {825, "Truck Roll" }
            };
        public static Dictionary<int, string> cc14179 = new Dictionary<int, string>()
            {
                {123, "Configuration Error" },
                {1083, "Customer Ticket Follow Up" },
                {825, "Truck Roll" }
            };
        public static Dictionary<int, string> cc15730 = new Dictionary<int, string>()
            {
                {1559, "Forgot WiFi Password" },
                {1083, "Customer Ticket Follow Up" },
            };
        public static Dictionary<int, string> cc15731 = new Dictionary<int, string>()
            {
                {123, "Configuration Error" },
                {1083, "Customer Ticket Follow Up" }
            };
        public static Dictionary<int, string> cc1129 = new Dictionary<int, string>()
            {
                {165, "Customer Education" },
                {1559, "Forgot WiFi Password" },
                {123, "Configuration Error" },
                {1083, "Customer Ticket Follow Up" },
                {825, "Truck Roll" }
            };
        public static Dictionary<int, string> cc1400x = new Dictionary<int, string>()
            {
                {165, "Customer Education" },
                {347, "Hardware/Software Failure or Configuration" },
                {1083, "Customer Ticket Follow Up" },
                {825, "Truck Roll" }
            };
        public static Dictionary<int, string> sc2804 = new Dictionary<int, string>()
            {
                {2804, "Cx Education Hardware/Software" }
            };
        public static Dictionary<int, string> sc6045 = new Dictionary<int, string>()
            {
                {6045, "Reconfig WiFi Security Settings" }
            };
        public static Dictionary<int, string> sc6092 = new Dictionary<int, string>()
            {
                {6092, "Toggle Bridge Mode" }
            };
        public static Dictionary<int, string> sc2901 = new Dictionary<int, string>()
            {
                {2901, "Cancelled by Phone" }
            };
        public static Dictionary<int, string> sc9797 = new Dictionary<int, string>()
            {
                {9797, "Premise Truck Roll" }
            };

        public static Dictionary<int, string> sc347 = new Dictionary<int, string>()
            {
                {6085, "Power Cycle Modem" },
                {6041, "Factory Reset WG" },
                {9671, "Reset Browser Settings" },
                {6067, "Reconfig Mobile Device" },
            };
    }
}
