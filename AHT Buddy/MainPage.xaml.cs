using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.Collections;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AHT_Buddy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        #region Remedy Codes : Class
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
            public static Dictionary<int, string> SolutionCodes = new Dictionary<int, string>()
            {
                {2804, "Cx Education Hardware/Software" },
                {6045, "Reconfig WiFi Security Settings" },
                {6092, "Toggle Bridge Mode" },
                {2901, "Cancelled by Phone" },
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
        #endregion

        public MainPage()
        {
            this.InitializeComponent();
            abbCustomer.IsChecked = true;
            comboPC.ItemsSource = code.ProblemCode; //bind problem code dictionary to combobox
            comboPC.DisplayMemberPath = "Value";
            comboPC.SelectedValuePath = "Key";
            comboPC.SelectedValue = -1;
            
        }

        private void AppBar_Opening(object sender, object e)
        {
            NavigateBar.Visibility = Visibility.Visible;
        }
        private void AppBar_Closing(object sender, object e)
        {
            NavigateBar.Visibility = Visibility.Collapsed;
        }
     
        private void comboSC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSolutionCode.Content = comboSC.SelectedValue;
        }

        private void btnProblemCode_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnCauseCode_Click(object sender, RoutedEventArgs e)
        {
            comboPC.ItemsSource = code.CauseCode;
            comboPC.DisplayMemberPath = "Value";
            comboPC.SelectedValuePath = "Key";
        }
    }
}
