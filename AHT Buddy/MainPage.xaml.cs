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
            public static Dictionary<int, string> CauseCode = new Dictionary<int, string>()
            {
                {165, "Customer Education" },
                {1559, "Forgotten WiFi Password" },
                {347, "Hardware/Software Failure or Configuration" },
                {1083, "Customer Ticket Follow up" },
                {825, "Truck Roll" },
                {123, "Configuration Error" }

            };
            public static Dictionary<int, string> SolutionCode = new Dictionary<int, string>()
            {
                {2804,"Cx Education Hardware/Software" },
                {6045, "Reconfig Wifi Security Setting" },
                //347 Specific
                { 6085, "Reset Modem/Powercycle Router" },
                { 6041, "Factory Reset WG" },
                { 9871, "Reset Cx Browser Settings" },
                { 6067, "Reconfig Mobile Device Settings" },
                //
                {2901, "Appointment Cancelled by Phone" },
                {9797, "Premise Truck Roll" },
                {6092, "Toggle Bride Mode" },


            };
          
        }
        #endregion

        public MainPage()
        {
            this.InitializeComponent();
            abbCustomer.IsChecked = true;
            comboPC.ItemsSource = code.ProblemCode;
            comboPC.DisplayMemberPath = "Value";
            comboPC.SelectedValuePath = "Key";
            
        }

        private void AppBar_Opening(object sender, object e)
        {
            NavigateBar.Visibility = Visibility.Visible;
        }
        private void AppBar_Closing(object sender, object e)
        {
            NavigateBar.Visibility = Visibility.Collapsed;
        }
        #region Load ComboBox Data
        private void cc1500x()
        {
            comboCC.Items.Clear();
            comboCC.Items.Add(code.CauseCode[165]);
            comboCC.Items.Add(code.CauseCode[1559]);
            comboCC.Items.Add(code.CauseCode[347]);
            comboCC.Items.Add(code.CauseCode[1083]);
            comboCC.Items.Add(code.CauseCode[825]);
        }
        private void cc1126()
        {
            comboCC.Items.Clear();
            comboCC.Items.Add(code.CauseCode[1559]);
            comboCC.Items.Add(code.CauseCode[1803]);
            comboCC.Items.Add(code.CauseCode[1083]);
        }
        private void cc14179()
        {
            comboCC.Items.Clear();
            comboCC.Items.Add(code.CauseCode[123]);
            comboCC.Items.Add(code.CauseCode[1083]);
            comboCC.Items.Add(code.CauseCode[825]);
        }
        private void cc15730()
        {
            comboCC.Items.Clear();
            comboCC.Items.Add(code.CauseCode[1559]);
            comboCC.Items.Add(code.CauseCode[1083]);
        }
        private void cc15731()
        {
            comboCC.Items.Clear();
            comboCC.Items.Add(code.CauseCode[123]);
            comboCC.Items.Add(code.CauseCode[1083]);
        }
        private void cc1129()
        {
            comboCC.Items.Clear();
            comboCC.Items.Add(code.CauseCode[165]);
            comboCC.Items.Add(code.CauseCode[1559]);
            comboCC.Items.Add(code.CauseCode[123]);
            comboCC.Items.Add(code.CauseCode[1083]);
            comboCC.Items.Add(code.CauseCode[825]);
        }
        private void cc14xxx()
        {
            comboCC.Items.Clear();
            comboCC.Items.Add(code.CauseCode[165]);
            comboCC.Items.Add(code.CauseCode[347]);
            comboCC.Items.Add(code.CauseCode[1083]);
            comboCC.Items.Add(code.CauseCode[1559]);
        }
        private void cc16176()
        {
            comboCC.Items.Clear();
            comboCC.Items.Add(code.CauseCode[347]);
            comboCC.Items.Add(code.CauseCode[1083]);
            comboCC.Items.Add(code.CauseCode[825]);
        }
        private void cc347()
        {
            comboSC.Items.Clear();
            comboSC.Items.Add(code.SolutionCode[6085]);
            comboSC.Items.Add(code.SolutionCode[6041]);
            comboSC.Items.Add(code.SolutionCode[9871]);
            comboSC.Items.Add(code.SolutionCode[6067]);
        }
        #endregion
        #region ComboBox Selection Change
        private void comboPC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            btnProblemCode.Content = comboPC.SelectedValue;

            if (comboPC.SelectedValue.ToString() == "15003" ||
                comboPC.SelectedValue.ToString() == "15002" ||
                comboPC.SelectedValue.ToString() == "15001" ||
                comboPC.SelectedValue.ToString() == "15006" ||
                comboPC.SelectedValue.ToString() == "15005" ||
                comboPC.SelectedValue.ToString() == "15004" ||
                comboPC.SelectedValue.ToString() == "15567")
            {
                cc1500x();
            }
            else if (comboPC.SelectedValue.ToString() == "1126") { cc1126(); }
            else if (comboPC.SelectedValue.ToString() == "14179") { cc14179(); }
            else if (comboPC.SelectedValue.ToString() == "15730") { cc15730(); }
            else if (comboPC.SelectedValue.ToString() == "15731") { cc15731(); }
            else if (comboPC.SelectedValue.ToString() == "1129") { cc1129(); }
            else if (comboPC.SelectedValue.ToString() == "14194" || comboPC.SelectedValue.ToString() == "14968") { cc14xxx(); }
            else if (comboPC.SelectedValue.ToString() == "16176") { cc16176};
        }

        private void comboCC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboCC.SelectedIndex == -1) { return; }
            else { btnCauseCode.Content = comboCC.SelectedValue; }

            if (comboCC.SelectedValue.ToString() == "165")
            {
                comboSC.Items.Clear();
                comboSC.Items.Add(code.SolutionCode[2804]);
                comboSC.SelectedIndex = 0;
            }
            else if (comboCC.SelectedValue.ToString() == "1559")
            {
                comboSC.Items.Clear();
                comboSC.Items.Add(code.SolutionCode[6045]);
                comboSC.SelectedIndex = 0;
            }
            else if (comboCC.SelectedValue.ToString() == "347")
            {
                comboSC.Items.Clear();
                comboSC.Items.Add(code.SolutionCode[6085]);
                comboSC.Items
            }
        }
        #endregion
    }
}
