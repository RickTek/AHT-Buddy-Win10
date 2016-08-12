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
using Windows.UI.Notifications;
using System.Collections.ObjectModel;
using System.Collections;
using System.Windows;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using SQLite.Net;
using SQLite.Net.Attributes;
using System.Data;
using Windows.UI.Popups;
using System.ComponentModel;










// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AHT_Buddy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public partial class MainPage : Page
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
        #region Alarm : Class

        public class Alarm : ObservableCollection<SetAlarm>
        {       
            public string Name { get; set; }
            public DateTime Time { get; set; }
            public DateTime CurrentTime { get; set; }
            public bool Armed { get; set; }
            
    
            public bool Triggered()
              {
               if (Time == CurrentTime)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
              }

        }
        public class SetAlarm
        {
            private string Name;
            private DateTime Time;
            public SetAlarm(string Name, DateTime Time)
            {
                this.Name = Name;
                this.Time = Time;
            }

        }
        #endregion
        #region Auto Replace Dictionary : Class
        public class AutoReplaceDictionary : ObservableCollection<WordPair>
        {
            public AutoReplaceDictionary() : base()
            {

            }
        }
        public class WordPair
        {
            private string word;
            private string replaceword;
            public WordPair(string word, string replaceword)
            {
                this.word = word;
                this.replaceword = replaceword;
            }

            public string Word
            {
                get { return word; }
                set { word = value; }
            }
            public string ReplaceWord
            {
                get { return replaceword; }
                set { replaceword = value; }
            }
        }
        #endregion
        #region Call Data : Class
        public class Customer
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string Email { get; set; }
            public string Ticket { get; set; }
            public string Name { get; set; }
            public string Account { get; set; }
            public string Contact { get; set; }
            public string Device { get; set; }
            public string Issue { get; set; }
            public string Resolution { get; set; }
            public string Next { get; set; }
            public string Attempt { get; set; }
            public bool Chronic { get; set; }
            public int ProblemCode { get; set; }
            public int CauseCode { get; set; }
            public int SolutionCode { get; set; }

            public DateTime TimeStamp { get; set; }
        }
        #endregion
        #region Wireless Gateway PoD : Class


        public class Technicolor
        {

            public string PoD { get; set; }
            public bool PM { get; set; }
            public string GetPoD()
            {
                int pos = 0;
                if (!string.IsNullOrEmpty(PoD))
                {
                    string DayNum = DateTime.Now.ToString("dd-MMM-yy").ToUpper();

                    if (PM == true)
                    {
                        DateTime NextDay = DateTime.Now;
                        DayNum = NextDay.AddDays(1).ToString("dd-MMM-yy").ToUpper();
                    }

                    string[] sPoD = PoD.Split('\t', '\n');
                    pos = Array.FindIndex(sPoD, row => row.Contains(DayNum));

                    return PoD = sPoD.ElementAt(pos += 1);
                }
                else
                {
                    return PoD = "no password set";
                }
            }
        }
        public class Arris
        {
            
            public string PoD { get; set; }
            public string GetPoD()
            {
                int pos = 0;
                if (!string.IsNullOrEmpty(PoD))
                {
                    string DayNum = DateTime.Now.ToString("dd-MMM-yy").ToUpper();

                    string[] sPoD = PoD.Split('\t', '\n');
                    pos = Array.FindIndex(sPoD, row => row.Contains(DayNum));

                    return PoD = sPoD.ElementAt(pos += 1);
                }
                else
                {
                    return PoD = "no password set";
                }
            }
        }
        public class Cisco
        {
            public string PoD { get; set; }
            public string GetPoD()
            {
                int pos = 0;
                if (!string.IsNullOrEmpty(PoD))
                {
                    string DayNum = DateTime.Now.ToString("dd-MMM-yy").ToUpper();

                    string[] sPoD = PoD.Split('\t', '\n');
                    pos = Array.FindIndex(sPoD, row => row.Contains(DayNum));

                    return PoD = sPoD.ElementAt(pos += 1);
                }
                else
                {
                    return PoD = "no password set";
                }
            }
        }
        public class Dory
        {
            
            public string PoD { get; set; }
            public string GetPoD()
            {
                int pos = 0;
                if (!string.IsNullOrEmpty(PoD))
                {
                    string DayNum = DateTime.Now.ToString("dd-MMM-yy").ToUpper();

                    string[] sPoD = PoD.Split('\t', '\n');
                    pos = Array.FindIndex(sPoD, row => row.Contains(DayNum));

                    return PoD = sPoD.ElementAt(pos += 1);
                }
                else
                {
                    return PoD = "no password set";
                }
            }
        }
        public class SMC
        {
            
            public string PoD { get; set; }
            public string GetPoD()
            {
               
                if (!string.IsNullOrEmpty(PoD))
                {
                    int pos = 0;
                    string DayNum = DateTime.Now.ToString("dd-MMM-yy").ToUpper();
                    string[] sPoD = PoD.Split('\t', '\n');
                    pos = Array.FindIndex(sPoD, row => row.Contains(DayNum));

                    return PoD = sPoD.ElementAt(pos += 1);
                }
                else
                {
                    return PoD = "no password set";
                }
            }
        }
        #endregion
        public static IEnumerable<T> FindVisualChildern<T>(DependencyObject depObj) where T : DependencyObject
        {
            if(depObj != null)
            {
                for(int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if(child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach(T childOfDChild in FindVisualChildern<T>(child))
                    {
                        yield return childOfDChild;
                    }
                }
            }
            
        }
        private async Task<bool> CheckFileExists(string filename) 
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                return true;
            }
            catch
            {
            }
            return false;
        }
        
        public DispatcherTimer clock = new DispatcherTimer();
        public static string cData = "CallData.sqlite";
        public static string path = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, cData);
        SQLiteConnection db = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
       
        AutoReplaceDictionary arList = new AutoReplaceDictionary();

        Technicolor technicolor = new Technicolor();
        Arris arris = new Arris();
        Cisco cisco = new Cisco();
        Dory dory = new Dory();
        SMC smc = new SMC();
        Alarm alarm = new Alarm();

        public MainPage()
        {
            this.InitializeComponent();

            db.CreateTable<Customer>();

            comboPC.ItemsSource = code.ProblemCode; //bind problem code dictionary to combobox
            comboPC.DisplayMemberPath = "Value";
            comboPC.SelectedValuePath = "Key";
            comboCC.DisplayMemberPath = "Value";
            comboCC.SelectedValuePath = "Key";
            comboSC.DisplayMemberPath = "Value";
            comboSC.SelectedValuePath = "Key";
            comboPC.SelectedValue = -1;
            
            Main_Pivot.SelectedItem = Cx_PivotItem; //Set Customer Data as opening pivot page

            clock.Tick += Clock_Ticker;
            clock.Interval = new TimeSpan(0, 0, 1);
            clock.Start();
            
        }
        
        private void Clock_Ticker(object sender, object e)
        {
            tbTime.Text = DateTime.Now.ToString("T");
        }
        #region Remedy Code Operations
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
        #endregion
        #region Call Operations
        private void NewCall_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbEmail.Text) &&
               string.IsNullOrEmpty(tbTicket.Text) &&
               string.IsNullOrEmpty(tbName.Text) &&
               string.IsNullOrEmpty(tbAccount.Text) &&
               string.IsNullOrEmpty(tbContact.Text) &&
               string.IsNullOrEmpty(tbDevice.Text) &&
               string.IsNullOrEmpty(tbIssue.Text) &&
               string.IsNullOrEmpty(tbResolution.Text))
            {
                return;
            }
            else
            {
                SaveCallData();
            }
        }
        private void SaveCallData()
        {
            db.Insert(new Customer()
            {
                Email = tbEmail.Text,
                Ticket = tbTicket.Text,
                Name = tbName.Text,
                Account = tbAccount.Text,
                Contact = tbContact.Text,
                Device = tbDevice.Text,
                Issue = tbIssue.Text,
                Resolution = tbResolution.Text,
                Next = tbNext.Text,
                Attempt = tbAttempt.Text,
                Chronic = cbChronic.IsChecked.Value,
                TimeStamp = DateTime.Now
                
            });
            
            foreach(TextBox tb in FindVisualChildern<TextBox>(CxDataGrid))
            {
                tb.Text = string.Empty;                   
            }
        }
        private void btnEmailCopy_Click(object sender, RoutedEventArgs e)
        {
            CopyToClipboard(tbEmail.Text);
        }
        #endregion
        #region PoD Operations
        private void CopyPod_Click(object sender, RoutedEventArgs e)
        {
            switch (comboWG.SelectedIndex)
            {
                case -1:
                    {
                        return;
                    }
                case 0:
                    {
                        CopyToClipboard(technicolor.GetPoD());
                        return;
                    }
                case 1:
                    {
                        CopyToClipboard(arris.GetPoD());
                        return;
                    }
                case 2:
                    {
                        CopyToClipboard(cisco.GetPoD());
                        return;
                    }
                case 3:
                    {
                        CopyToClipboard(dory.GetPoD());
                        return;
                    }
                case 4:
                    {
                        CopyToClipboard(smc.GetPoD());
                        return;
                    }
            }
        }
        private void PODs_PivotItem_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(technicolor.PoD))
            {
                technicolor.PoD = tbTechnicolor.Text;
                tblockTechnicolor.Text = technicolor.GetPoD();
            }
            if (!string.IsNullOrEmpty(tbArris.Text))
            {
                arris.PoD = tbArris.Text;
                tblockArris.Text = arris.GetPoD();
            }
            if (!string.IsNullOrEmpty(tbCisco.Text))
            {
                cisco.PoD = tbCisco.Text;
                tblockCisco.Text = cisco.GetPoD();
            }
            if (!string.IsNullOrEmpty(tbDory.Text))
            {
                dory.PoD = tbDory.Text;
                tblockDory.Text = dory.GetPoD();
            }
            if (!string.IsNullOrEmpty(tbSMC.Text))
            {
                smc.PoD = tbSMC.Text;
                tblockSMC.Text = smc.GetPoD();
            }
        }

        private void cbTechnicolorPM_Checked(object sender, RoutedEventArgs e)
        {
            technicolor.PM = true;            
        }
        private void cbTechnicolorPM_Unchecked(object sender, RoutedEventArgs e)
        {
            technicolor.PM = false;
        }
        #endregion
               
        private async void _MessageBox(string msg)
        {
            MessageDialog showDialog = new MessageDialog(msg);
            showDialog.Commands.Add(new UICommand("Ok") { Id = 0 });
            showDialog.DefaultCommandIndex = 0;
            var result = await showDialog.ShowAsync();

            if ((int)result.Id == 0)
            {
                return;
            }
            else
            {
                return;
            }
        }
        private static void CopyToClipboard(string CopyString)
        {

            // Create an instance of the DataPackage and set the RequestedOperation to DataPackageOperation.Copy 

            DataPackage dataPackageobj = new DataPackage
            {

                RequestedOperation = DataPackageOperation.Copy

            };

            // Set the Text that you want to copy 

            dataPackageobj.SetText(CopyString);

            // Set the data package instance to the Clipboard 

            Clipboard.SetContent(dataPackageobj);

        }
        private void btnSetWord_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbAddWord.Text))
            {
                return;
            }
            else
            {
                bool containsDelimiter = tbAddWord.Text.Contains("~~");

                string[] SplitString = tbAddWord.Text.Split(new string[] { "~~" }, StringSplitOptions.None);

                if(!arList.Any(w => w.Word == SplitString[0]) && containsDelimiter == true)
                {
                    arList.Add(new WordPair(SplitString[0], SplitString[1]));
                }
                else
                {
                    if(containsDelimiter == false)
                    {
                        _MessageBox("Dictionary entries must use the ~~ delimiter to separate your Words!");
                    }
                    else
                    {
                        _MessageBox("This Key:Value pair " + SplitString[0] + "~~" + SplitString[1] + " " + "\nIs already in the Dictionary!");
                    }
                    
                }
            }
        }

        private void Alarms_PivotItems_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void Dictionary_PivotItem_GotFocus(object sender, RoutedEventArgs e)
        {
            
                
        
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            string _Date = DateTime.Now.ToString("MM/dd/yyyy");
            string chronic;

            if(cbChronic.IsChecked == true){chronic = "Yes";}else { chronic = "No"; }
            if (string.IsNullOrEmpty(tbAttempt.Text))
            {
                tbAttempt.Text = "1";
            }
            string formatNotes = 
                "Date: {0}\n" +
                "Ticket: {1}\n" +
                "Cutomer Name: {2}\n" +
                "Account Number: {3}\n" +
                "Chronic Account: {4}\n" +
                "Attempt Number: {5}\n" +
                "Contact Number: (6)\n" +
                "Affected Device: {7}\n" +
                "Reported Issue: {8}\n" +
                "Steps Taken to Identify and Resolve: {9}\n" +
                "Next Action: {10}\n";

            string RemedyNotes = string.Format(formatNotes, _Date, tbTicket.Text, tbName.Text, tbAccount.Text, chronic,
                                               tbAttempt.Text, tbContact.Text, tbDevice.Text, tbIssue.Text, tbResolution.Text, tbNext.Text);
            CopyToClipboard(RemedyNotes);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
           
            
        }
        private void Toast()
        {
            var xmlToastTemplate = "<toast launch=\"app-defined-string\">" +
                        "<visual>" +
                          "<binding template =\"ToastGeneric\">" +
                            "<text>Time for Break!</text>" +
                            "<text>" +
                              "This is a sample toast notification from kunal-chowdhury.com" +
                            "</text>" +
                          "</binding>" +
                        "</visual>" +
                        "<audio src=\"ms-winsoundevent:Notification.Looping.Alarm9\" Loop=\"false\"/>" +
                      "</toast>";

            // load the template as XML document
            var xmlDocument = new Windows.Data.Xml.Dom.XmlDocument();
            xmlDocument.LoadXml(xmlToastTemplate);

            // create the toast notification and show to user
            var toastNotification = new ToastNotification(xmlDocument);
            var notification = ToastNotificationManager.CreateToastNotifier();
            notification.Show(toastNotification);

        }
    }
}
