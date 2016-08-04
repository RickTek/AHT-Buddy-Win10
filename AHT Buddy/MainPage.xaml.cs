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
        #endregion

        public MainPage()
        {
            this.InitializeComponent();
            abbCustomer.IsChecked = true;
            comboPC.ItemsSource = code.ProblemCode; //bind problem code dictionary to combobox
            comboPC.DisplayMemberPath = "Value";
            comboPC.SelectedValuePath = "Key";
            comboCC.DisplayMemberPath = "Value";
            comboCC.SelectedValuePath = "Key";
            comboSC.DisplayMemberPath = "Value";
            comboSC.SelectedValuePath = "Key";
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
        #region Problem Code Combo Box Selection Changed
        private void comboPC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboPC.SelectedIndex == -1) { return; }
            
            
            if (comboPC.SelectedValue.ToString() == "15003" ||
               comboPC.SelectedValue.ToString() == "15002" ||
               comboPC.SelectedValue.ToString() == "15001" ||
               comboPC.SelectedValue.ToString() == "15006" ||
               comboPC.SelectedValue.ToString() == "15005" ||
               comboPC.SelectedValue.ToString() == "15004" ||
               comboPC.SelectedValue.ToString() == "15567")
            {
                if (comboCC.ItemsSource != code.cc1500x)
                {
                    comboCC.ItemsSource = code.cc1500x;
                }
                else { return; }
             }
             else if(comboPC.SelectedValue.ToString() == "1126")
            {
                if(comboCC.ItemsSource != code.cc1126)
                {
                    comboCC.ItemsSource = code.cc1126;
                }
                else { return; }
            }
            else if(comboPC.SelectedValue.ToString() == "14179")
            {
                if(comboCC.ItemsSource != code.cc14179)
                {
                    comboCC.ItemsSource = code.cc14179;
                }
                else { return; }
            }
            else if(comboPC.SelectedValue.ToString() == "15730")
            {
                if(comboCC.ItemsSource != code.cc15730)
                {
                    comboCC.ItemsSource = code.cc15730;
                }
                else { return; }
            }
            else if (comboPC.SelectedValue.ToString() == "15731")
            {
                if(comboCC.ItemsSource != code.cc15731)
                {
                    comboCC.ItemsSource = code.cc15731;
                }
                else { return; }
            }
            else if(comboPC.SelectedValue.ToString() == "1129")
            {
                if(comboCC.ItemsSource != code.cc1129)
                {
                    comboCC.ItemsSource = code.cc1126;
                }
                else { return; }
            }
            else if(comboPC.SelectedValue.ToString() == "14194" || comboPC.SelectedValue.ToString() == "14966")
            {
                if(comboCC.ItemsSource != code.cc1400x)
                {
                    comboCC.ItemsSource = code.cc1400x;
                }
                else { return; }
            }
            btnProblemCode.Content = comboPC.SelectedValue;
        }
        #endregion
        #region Cause Code Combox Selection Changed
        private void comboCC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if(comboCC.SelectedIndex == -1) { return; }

            if(comboCC.SelectedValue.ToString() == "165")
            {
                if(comboSC.ItemsSource != code.sc2804)
                {
                    comboSC.ItemsSource = code.sc2804;
                    comboSC.SelectedIndex = 0;
                }
                else { return; }
            }
            else if(comboCC.SelectedValue.ToString() == "1559")
            {
                if(comboSC.ItemsSource != code.sc6045)
                {
                    comboSC.ItemsSource = code.sc6045;
                    comboSC.SelectedIndex = 0;
                }
                else { return; }
            }
            else if(comboCC.SelectedValue.ToString() == "347")
            {
                if(comboSC.ItemsSource != code.sc347)
                {
                    comboSC.ItemsSource = code.sc347;
                    comboSC.SelectedIndex = 0;
                }
                else { return; }
            }
            else if(comboCC.SelectedValue.ToString() == "1083")
            {
                if (comboSC.ItemsSource != code.sc2901)
                {
                    comboSC.ItemsSource = code.sc2901;
                    comboSC.SelectedIndex = 0;
                }
                else { return; }
            }
            else if(comboCC.SelectedValue.ToString() == "825")
            {
                if(comboSC.ItemsSource != code.sc9797)
                {
                    comboSC.ItemsSource = code.sc9797;
                    comboSC.SelectedIndex = 0;
                }
                else { return; }
            }
            else if(comboCC.SelectedValue.ToString() == "123")
            {
                if(comboSC.ItemsSource != code.sc6045)
                {
                    comboSC.ItemsSource = code.sc6045;
                    comboSC.SelectedIndex = 0;
                }
                else if(comboPC.SelectedValue.ToString() == "14179")
                {
                    if(comboSC.ItemsSource != code.sc6092)
                    {
                        comboSC.ItemsSource = code.sc6092;
                        comboSC.SelectedIndex = 0;
                    }
                    else { return; }
                }
                else { return; }
            }
            btnCauseCode.Content = comboCC.SelectedValue;
            btnSolutionCode.Content = comboSC.SelectedValue;
        }
        #endregion
        #region Solution Code Combo Box Selection Changed
        private void comboSC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboSC.SelectedIndex == -1) { return; }
            btnSolutionCode.Content = comboSC.SelectedValue;

        }
        #endregion
    }
}
