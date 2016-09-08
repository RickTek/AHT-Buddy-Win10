using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.ObjectModel;
using System.Collections;
using System.Windows;
using System.Threading.Tasks;
using System.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Notifications;
using Windows.Storage;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Popups;
using Windows.System;
using System.Text.RegularExpressions;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Xml.Linq;
using System.ComponentModel;
using System.Runtime.Serialization;
using Windows.Storage.Streams;
using Newtonsoft.Json;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AHT_Buddy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Returns a Rectangle instance which includes the bounds for an element within its outer container.
        /// </summary>
        /// <param name="element">The element whose bounds you wish to retieve</param>
        /// <param name="container">The elements container. Use page if there is no direct container</param>
        /// <returns></returns>
        public static Rect GetElementBounds(this FrameworkElement element, FrameworkElement container)
        {
            if (element == null || container == null)
                return Rect.Empty;

            if (element.Visibility != Visibility.Visible)
                return Rect.Empty;

            return element.TransformToVisual(container).TransformBounds(new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));
        }
    }
    public partial class MainPage : Page
    {



        #region Auto Replace Dictionary : Class
        public class AutoReplaceDictionary : ObservableCollection<WordPair>
        {
            public AutoReplaceDictionary() : base()
            {

            }

            internal bool Contains(Func<object, bool> p)
            {
                throw new NotImplementedException();
            }
        }
        public class WordPair
        {
            private string word;
            private string replaceword;
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
            public WordPair(string word, string replaceword)
            {
                this.word = word;
                this.replaceword = replaceword;

            }
        }
        #endregion             
        #region AutoSuggest List
        public static class SuggestionList
        {
            public static List<string> Device = new List<string>
            {
                //Gateways
                { "TG852G" },{"TG862G" }, { "TG1682G" }, { "DPC3939" },{ "DPC3941T" }, { "TC8350C" }, { "TC8717C" }, { "SMCD3GNV" },
                //Routers
                { "Linksys" }, { "Netgear" }, { "Belkin" }, { "Cisco" }, { "TP-Link" }, { "Asus" },
                //Apple
                { "Mac" }, { "MacBook" }, { "MacBook Pro" }, { "MacBook Air" },{ "iPad" }, { "iPhone" }, { "Apple TV" },
                //Amazon
                { "Kindle" }, { "Kindle Fire" }, { "Firestick" },
                //Streaming Device
                { "Roku" }, { "Chromecast" }, { "SlingBox" },
                //Brands
                { "Apple" }, { "Acer" }, { "Asus" }, { "Canon" }, { "HP" }, { "Dell" }, { "Sony" }, { "Lenovo" }, { "Samsung" },
                { "Vizio" }, { "RCA" }, { "Brother" }, { "LG" }, { "Google" }, { "Lexmark" },
                //Game Consoles
                { "Wii" }, { "Wii U" }, { "PS3" }, { "PS4" }, { "Xbox One" }, { "Xbox 360" },
                //Generic
                { "Laptop" }, { "Desktop" }, { "Smartphone" }, { "Tablet" }, { "Printer" }, { "TV" }, { "Custom" }, { "Thermostat" }
            };
            public static List<string> Email = new List<string>
            {
                { "@gmail.com" }, { "@yahoo.com" }, { "@hotmail.com" }, { "@outlook.com" }, { "@comcast.net" },
                { "@ymail.com" }, { "@me.com" }, { "@comcast.net" }, { "@aol.com" }
            };
            public static List<string> Breaks = new List<string>
            {
                { "Break" }, { "First Break" }, { "Second Break" }, { "Last Break"}, { "Lunch" }, { "1st Break" }, { "2nd Break" }
            };
        }
        #endregion
        public static IEnumerable<T> FindVisualChildern<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfDChild in FindVisualChildern<T>(child))
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


        AutoReplaceDictionary arList = new AutoReplaceDictionary();
        List<string> deviceSuggestion = null;
        List<string> emailSuggestion = null;
        List<string> breaksSuggestion = null;
        List<string> SortedAlarm = new List<string>();

        CallData calldata = new CallData();

        

        WirelessGateways.technicolor Technicolor = new WirelessGateways.technicolor();
        WirelessGateways.arris Arris = new WirelessGateways.arris();
        WirelessGateways.cisco Cisco = new WirelessGateways.cisco();
        WirelessGateways.dory Dory = new WirelessGateways.dory();
        WirelessGateways.smc SMC = new WirelessGateways.smc();


        StorageFolder AHTBuddy = ApplicationData.Current.LocalFolder;

        string Technicolor_file = "Technicolor.txt";
        string Arris_file = "Arris.txt";
        string Cisco_file = "Cisco.txt";
        string Dory_file = "Dory.txt";
        string SMC_file = "SMC.txt";


        string WordList_file = "WordList.dat";
        ApplicationDataContainer AppSettings = ApplicationData.Current.LocalSettings;
        


        Alarm alarm = new Alarm();
        
        string GotLastWord;
        string InsertWord;
        bool matched;
        int wordcount;
        int WordPairCount;
        

        public MainPage()
        {
            clock.Tick += Clock_Ticker;
            clock.Interval = new TimeSpan(0, 0, 1);
            clock.Start();
            AppSettings.Values["WordPairCount"] = WordPairCount;

            Loaded += Page_Loaded;
            this.InitializeComponent();

            Application.Current.Suspending += new SuspendingEventHandler(App_Suspending);
            comboPC.ItemsSource = code.ProblemCode; //bind problem code dictionary to combobox
            comboPC.DisplayMemberPath = "Value";
            comboPC.SelectedValuePath = "Key";
            comboCC.DisplayMemberPath = "Value";
            comboCC.SelectedValuePath = "Key";
            comboSC.DisplayMemberPath = "Value";
            comboSC.SelectedValuePath = "Key";
            comboPC.SelectedValue = -1;

            Main_Pivot.SelectedItem = Cx_PivotItem; //Set Customer Data as opening pivot page

        }
        #region File Operations
        
        async void App_Suspending(Object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            try
            {
                await SaveData(Technicolor_file, tbTechnicolor.Text, false);
                await SaveData(Arris_file, tbArris.Text, false);
                await SaveData(Cisco_file, tbCisco.Text, false);
                await SaveData(Dory_file, tbDory.Text, false);
                await SaveData(SMC_file, tbSMC.Text, false);
                await SaveData(WordList_file, string.Empty, true);
            }
            catch { }
            
            
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer AppSettings = ApplicationData.Current.LocalSettings;

            try
            {
                await LoadData(WordList_file, true);
            }
            catch { }
            try
            {
                tbTechnicolor.Text = await LoadData(Technicolor_file, false);
                tbArris.Text = await LoadData(Arris_file, false);
                tbCisco.Text = await LoadData(Cisco_file, false);
                tbDory.Text = await LoadData(Cisco_file, false);
                tbSMC.Text = await LoadData(SMC_file, false);
            }
            catch
            {

            }
            
        }

        private async Task SaveWordList(AutoReplaceDictionary arlist)
        {
            try
            {
                StorageFile savedStuffFile =
                    await ApplicationData.Current.LocalFolder.CreateFileAsync("WordPairs.dat", CreationCollisionOption.ReplaceExisting);

                using (Stream writeStream =
                    await savedStuffFile.OpenStreamForWriteAsync())
                {
                    DataContractSerializer stuffSerializer =
                        new DataContractSerializer(typeof(AutoReplaceDictionary));

                    stuffSerializer.WriteObject(writeStream, arlist);
                    await writeStream.FlushAsync();
                    writeStream.Dispose();
                }
                
            }
            catch (Exception e)
            {
                throw new Exception("ERROR: Cannot save Dictionary Data!", e);
            }
        }


        private async Task SaveData(string FileName, string TextFile, bool IsList)
        {

            if (IsList == false)
            {
                StorageFile storagefile = await AHTBuddy.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);
                storagefile = await AHTBuddy.GetFileAsync(FileName);
                await FileIO.WriteTextAsync(storagefile, TextFile);
            }
            else
            {
                await SaveWordList(this.arList);
            }
          
        }
        private async Task<string> LoadData(string FileName, bool IsList)
        {
            FileInfo fInfo = new FileInfo(FileName);
            if (fInfo.Exists)
            {
                if (IsList == false)
                {
                    StorageFile file = await AHTBuddy.GetFileAsync(FileName);


                    return await FileIO.ReadTextAsync(file);
                }
                else
                {
                    StorageFile file = await AHTBuddy.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);

                    using (var stream = await file.OpenStreamForReadAsync())
                    {
                        string text;
                        using (var reader = new StreamReader(stream))
                        {
                            text = await reader.ReadToEndAsync();
                        }
                        var wordlist = JsonConvert.DeserializeObject<WordPair>(text);
                        arList.Add(wordlist);
                    }


                    using (IInputStream inStream = await file.OpenSequentialReadAsync())
                    {
                        DataContractSerializer serializer = new DataContractSerializer(typeof(ObservableCollection<WordPair>));
                        var data = (ObservableCollection<WordPair>)serializer.ReadObject(inStream.AsStreamForRead());
                    }
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
            
        }
        #endregion
        #region Clock Operations
        private void Clock_Ticker(object sender, object e)
        {
            tbTime.Text = DateTime.Now.ToString("T");
            
            IsTripped();
            displayAlarm();

        }
        #endregion
        #region Remedy Code Operations
        #region Problem Code Combo Box Selection Changed
        private void comboPC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboPC.SelectedIndex == -1) { return; }

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
            else if (comboPC.SelectedValue.ToString() == "1126")
            {
                if (comboCC.ItemsSource != code.cc1126)
                {
                    comboCC.ItemsSource = code.cc1126;
                }
                else { return; }
            }
            else if (comboPC.SelectedValue.ToString() == "14179")
            {
                if (comboCC.ItemsSource != code.cc14179)
                {
                    comboCC.ItemsSource = code.cc14179;
                }
                else { return; }
            }
            else if (comboPC.SelectedValue.ToString() == "15730")
            {
                if (comboCC.ItemsSource != code.cc15730)
                {
                    comboCC.ItemsSource = code.cc15730;
                }
                else { return; }
            }
            else if (comboPC.SelectedValue.ToString() == "15731")
            {
                if (comboCC.ItemsSource != code.cc15731)
                {
                    comboCC.ItemsSource = code.cc15731;
                }
                else { return; }
            }
            else if (comboPC.SelectedValue.ToString() == "1129")
            {
                if (comboCC.ItemsSource != code.cc1129)
                {
                    comboCC.ItemsSource = code.cc1126;
                }
                else { return; }
            }
            else if (comboPC.SelectedValue.ToString() == "14194" || comboPC.SelectedValue.ToString() == "14966")
            {
                if (comboCC.ItemsSource != code.cc1400x)
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
            if (comboCC.SelectedIndex == -1) { return; }

            if (comboCC.SelectedValue.ToString() == "165")
            {
                if (comboSC.ItemsSource != code.sc2804)
                {
                    comboSC.ItemsSource = code.sc2804;
                    comboSC.SelectedIndex = 0;
                }
                else { return; }
            }
            else if (comboCC.SelectedValue.ToString() == "1559")
            {
                if (comboSC.ItemsSource != code.sc6045)
                {
                    comboSC.ItemsSource = code.sc6045;
                    comboSC.SelectedIndex = 0;
                }
                else { return; }
            }
            else if (comboCC.SelectedValue.ToString() == "347")
            {
                if (comboSC.ItemsSource != code.sc347)
                {
                    comboSC.ItemsSource = code.sc347;
                    comboSC.SelectedIndex = 0;
                }
                else { return; }
            }
            else if (comboCC.SelectedValue.ToString() == "1083")
            {
                if (comboSC.ItemsSource != code.sc2901)
                {
                    comboSC.ItemsSource = code.sc2901;
                    comboSC.SelectedIndex = 0;
                }
                else { return; }
            }
            else if (comboCC.SelectedValue.ToString() == "825")
            {
                if (comboSC.ItemsSource != code.sc9797)
                {
                    comboSC.ItemsSource = code.sc9797;
                    comboSC.SelectedIndex = 0;
                }
                else { return; }
            }
            else if (comboCC.SelectedValue.ToString() == "123")
            {
                if (comboSC.ItemsSource != code.sc6045)
                {
                    comboSC.ItemsSource = code.sc6045;
                    comboSC.SelectedIndex = 0;
                }
                else if (comboPC.SelectedValue.ToString() == "14179")
                {
                    if (comboSC.ItemsSource != code.sc6092)
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
            if (comboSC.SelectedIndex == -1) { return; }
            btnSolutionCode.Content = comboSC.SelectedValue;

        }
        #endregion
        #endregion
        #region Call Operations
        private SolidColorBrush GetSolidColorBrush(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            return myBrush;
        }

        private void NewCall_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbEmail.Text))
            {
                tbEmail.Text = "No Email Captured";
            }
            if (string.IsNullOrEmpty(tbAttempt.Text))
            {
                tbAttempt.Text = "1";
            }
            if (string.IsNullOrEmpty(tbNext.Text))
            {
                tbNext.Text = "N/A";
            }
            foreach (TextBox tb in FindVisualChildern<TextBox>(CxDataGrid))
            {
                if (string.IsNullOrEmpty(tb.Text))
                {
                    
                    tb.BorderBrush = new SolidColorBrush(Colors.Red);
                    return;
                }
                else
                {
                    tb.BorderBrush = GetSolidColorBrush("#FF7A7A7A");
                }
                
            }
            if (string.IsNullOrEmpty(tbIssue.Text))
            {
                tbIssue.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }
            else
            {
                tbIssue.BorderBrush = GetSolidColorBrush("#FF7A7A7A");
            }
            if (string.IsNullOrEmpty(tbResolution.Text))
            {
                tbResolution.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }
            else
            {
                tbResolution.BorderBrush = GetSolidColorBrush("#FF7A7A7A");
            }

            SaveCallData();
        }
        private void SaveCallData()
        {
            calldata.Add(new Customer(
                tbEmail.Text,
                tbTicket.Text,
                tbName.Text,
                tbAccount.Text,
                tbContact.Text,
                tbDevice.Text,
                tbIssue.Text,
                tbResolution.Text,
                tbNext.Text,
                tbAttempt.Text,
                cbChronic.IsChecked.Value,
                comboPC.SelectedIndex,
                comboCC.SelectedIndex,
                comboSC.SelectedIndex,
                DateTime.Now.ToString()
                )
                );
            tbCallCount.Text = calldata.Count().ToString();

            foreach (TextBox tb in FindVisualChildern<TextBox>(CxDataGrid))
            {
                tb.Text = string.Empty;
            }
            tbIssue.Text = string.Empty;
            tbResolution.Text = string.Empty;
            tbDevice.Text = string.Empty;
        }
        private void GetCallData_Click(object sender, RoutedEventArgs e)
        {
            if (calldata.Count != 0)
            {
                int si = lbPreviousCallData.SelectedIndex;
                tbEmail.Text = calldata[si].Email.ToString();
                tbTicket.Text = calldata[si].Ticket.ToString();
                tbName.Text = calldata[si].Name.ToString();
                tbAccount.Text = calldata[si].Account.ToString();
                tbContact.Text = calldata[si].Contact.ToString();
                tbDevice.Text = calldata[si].Device.ToString();
                tbIssue.Text = calldata[si].Issue.ToString();
                tbResolution.Text = calldata[si].Resolution.ToString();
                tbNext.Text = calldata[si].Next.ToString();
                tbAttempt.Text = calldata[si].Attempt.ToString();
                cbChronic.IsChecked = calldata[si].Chronic;
                comboPC.SelectedIndex = calldata[si].ProblemCode;
                comboCC.SelectedIndex = calldata[si].CauseCode;
                comboSC.SelectedIndex = calldata[si].CauseCode;
            }
            else
            {
                return;
            }

        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            string _Date = DateTime.Now.ToString("MM/dd/yyyy");
            string chronic;

            if (cbChronic.IsChecked == true) { chronic = "Yes"; } else { chronic = "No"; }
            if (string.IsNullOrEmpty(tbAttempt.Text))
            {
                tbAttempt.Text = "1";
            }
            if (string.IsNullOrEmpty(tbNext.Text))
            {
                tbNext.Text = "N/A";
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
        #endregion
        #region PoD Operations
       

        private void btnPoDcopy_Click(object sender, RoutedEventArgs e)
        {
            switch (comboWG.SelectedIndex)
            {
                case -1:
                    {
                        return;
                    }
                case 0:
                    {
                        if (Technicolor.GetExpiry == false)
                        {
                            CopyToClipboard(Technicolor.GetPoD);
                            return;
                        }
                        else
                        {
                            _MessageBox("PoD expired!/nNo current PoD available");
                            return;
                        }

                    }
                case 1:
                    {
                        if (Arris.GetExpiry == false)
                        {
                            CopyToClipboard(Arris.GetPoD);
                            return;
                        }
                        else
                        {
                            _MessageBox("PoD expired!/nNo current PoD available");
                            return;
                        }

                    }
                case 2:
                    {
                        if (Cisco.GetExpiry == false)
                        {
                            CopyToClipboard(Cisco.GetPoD);
                            return;
                        }
                        else
                        {
                            _MessageBox("PoD expired!/nNo current PoD available");
                            return;
                        }

                    }
                case 3:
                    {
                        if (Dory.GetExpiry == false)
                        {
                            CopyToClipboard(Dory.GetPoD);
                            return;
                        }
                        else
                        {
                            _MessageBox("PoD expired!/nNo current PoD available");
                            return;
                        }

                    }
                case 4:
                    {
                        if (SMC.GetExpiry == false)
                        {
                            CopyToClipboard(SMC.GetPoD);
                            return;
                        }
                        else
                        {
                            _MessageBox("PoD expired!/nNo current PoD available");
                            return;
                        }
                    }
            }
        }
        private void PODs_PivotItem_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbTechnicolor.Text))
            {
                Technicolor.PoD = tbTechnicolor.Text;
                tblockTechnicolor.Text = Technicolor.GetPoD;
            }
            if (!string.IsNullOrEmpty(tbArris.Text))
            {
                Arris.PoD = tbArris.Text;
                tblockArris.Text = Arris.GetPoD;
            }
            if (!string.IsNullOrEmpty(tbCisco.Text))
            {
                Cisco.PoD = tbCisco.Text;
                tblockCisco.Text = Cisco.GetPoD;
            }
            if (!string.IsNullOrEmpty(tbDory.Text))
            {
                Dory.PoD = tbDory.Text;
                tblockDory.Text = Dory.GetPoD;
            }
            if (!string.IsNullOrEmpty(tbSMC.Text))
            {
                SMC.PoD = tbSMC.Text;
                tblockSMC.Text = SMC.GetPoD;
            }

            if (!string.IsNullOrEmpty(Technicolor.PoD) ||
                !string.IsNullOrEmpty(Arris.PoD) ||
                !string.IsNullOrEmpty(Cisco.PoD) ||
                !string.IsNullOrEmpty(Dory.PoD) ||
                !string.IsNullOrEmpty(SMC.PoD))
            {

            }


        }

        private void cbTechnicolorPM_Checked(object sender, RoutedEventArgs e)
        {
            Technicolor.PM = true;
        }
        private void cbTechnicolorPM_Unchecked(object sender, RoutedEventArgs e)
        {
            Technicolor.PM = false;
        }
        #endregion
        #region Alarm Operations

        private void displayAlarm()
        {
            if(alarm.Count != 0)
            {
                for (int i = 0; i <= alarm.Count - 1; i++)
                {
                    if (alarm[i].Armed == true)
                    {
                        SortedAlarm.Clear();
                        SortedAlarm.Add(DateTime.Parse(alarm[i].Time).ToString("HH:mm"));
                    }
                }
                SortedAlarm.Sort();
                if(SortedAlarm.Count != 0)
                {
                    tbCurrentAlarm.Text = DateTime.Parse(SortedAlarm[0]).ToString("hh:mm tt");
                }                
            }          
        }
        private void tbAlarmName_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var selectedItem = args.SelectedItem.ToString();
            sender.Text = selectedItem;
        }

        private void tbAlarmName_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                breaksSuggestion = SuggestionList.Breaks.Where(x => x.StartsWith(sender.Text)).ToList();
                sender.ItemsSource = breaksSuggestion;
            }
        }

        private void tbAlarmName_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                tbAlarmName.Text = args.ChosenSuggestion.ToString();
            }
        }

        private void btnChangeAlarm_Click(object sender, RoutedEventArgs e)
        {
            if (lbAlarms.SelectedIndex != -1)
            {
                tbChangeAlarmName.Text = alarm[lbAlarms.SelectedIndex].Name;
                tpAlarmChange.Time = TimeSpan.Parse(DateTime.Parse(alarm[lbAlarms.SelectedIndex].Time).ToString("HH:mm"));
                popupChangeAlarm.IsOpen = true;
            }
            else
            {
                return;
            }
        }

        private void ChangeAlarm_Click(object sender, RoutedEventArgs e)
        {
            int si = lbAlarms.SelectedIndex;

            if (tbChangeAlarmName.Text == alarm[si].Name && DateTime.Parse(tpAlarmChange.Time.ToString()).ToString("T") == alarm[si].Time)
            {
                return;
            }
            else
            {
                if (tbChangeAlarmName.Text != alarm[si].Name)
                {
                    alarm[si].Name = tbChangeAlarmName.Text;
                }
                else
                {
                    return;
                }
                if (DateTime.Parse(tpAlarmChange.Time.ToString()).ToString("T") != alarm[si].Time)
                {
                    alarm[si].Time = DateTime.Parse(tpAlarmChange.Time.ToString()).ToString("T");
                }
                else
                {
                    return;
                }
            }
            popupChangeAlarm.IsOpen = false;

        }

        private void popupChangeAlarm_LostFocus(object sender, RoutedEventArgs e)
        {
            popupChangeAlarm.IsOpen = false;
        }

        private void puAlarmSet_LostFocus(object sender, RoutedEventArgs e)
        {
            puAlarmSet.IsOpen = false;
        }

        private void CancelChangeAlarm_Click(object sender, RoutedEventArgs e)
        {
            popupChangeAlarm.IsOpen = false;
        }

        private void CancelSetAlarm(object sender, RoutedEventArgs e)
        {
            puAlarmSet.IsOpen = false;
        }
    
    private void Toast(string title, string content)
        {


            ToastVisual visual = new ToastVisual()
            {

                BindingGeneric = new ToastBindingGeneric()
                {

                    Children =
                {
                    new AdaptiveText()
                    {
                        Text = title
                    },

                    new AdaptiveText()
                    {
                        Text = content
                    }
                }
                }
            };
            ToastActionsCustom actions = new ToastActionsCustom()
            {
                Buttons =
                {
                    new ToastButton("Dismiss", "{argsDismiss}")
                    {

                    }
                }
            };

            ToastContent toastcontent = new ToastContent()
            {
                Scenario = ToastScenario.Alarm,
                Visual = visual,
                Actions = actions,
            };

            var toast = new ToastNotification(toastcontent.GetXml());
            toast.ExpirationTime = DateTime.Now.AddHours(1);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }


        private void IsTripped()
        {
            if (alarm.Count != 0)
            {
                for (int i = 0; i <= alarm.Count - 1; i++)
                {
                    if (DateTime.Parse(alarm[i].Time.ToString()).ToString("T") == DateTime.Now.ToString("T") && alarm[i].Armed == true)
                    {
                        alarm[i].Armed = false;
                        Toast("Time to take your break!", alarm[i].Name);
                    }
                }
            }
        }
        private void btnNewAlarm_Click(object sender, RoutedEventArgs e)
        {
            puAlarmSet.IsOpen = true;
        }
        private void SetAlarm_Click(object sender, RoutedEventArgs e)
        {
            alarm.Add(new AlarmInfo(tbAlarmName.Text, DateTime.Parse(tpAlarm.Time.ToString()).ToString("hh:mm tt"), "Off"));


            puAlarmSet.IsOpen = false;
        }


        private void btnRemoveAlarm_Click(object sender, RoutedEventArgs e)
        {

            alarm.RemoveAt(lbAlarms.SelectedIndex);
        }
        #endregion
        #region Auto Replace Dictionary Operations
        private void AddWord()
        {
            if (string.IsNullOrEmpty(tbAddWord.Text) || string.IsNullOrEmpty(tbAddWordReplacement.Text))
            {
                return;
            }
            else
            {
                if (!arList.Any(w => w.Word == tbAddWord.Text))
                {
                    arList.Add(new WordPair(tbAddWord.Text, tbAddWordReplacement.Text));
                    tbAddWord.Text = string.Empty;
                }
                else
                {
                     _MessageBox("This Key:Value pair " + tbAddWord.Text + " :: " + tbAddWordReplacement.Text + " " + "\nIs already in the Dictionary!");
                    
                }
            }
        }

        private void btnSetWord_Click(object sender, RoutedEventArgs e)
        {
            AddWord();
        }
        private void AutoReplace(TextBox txt)
        {
            int lastword = txt.Text.LastIndexOf(" ");

            lastword += 1;
            GotLastWord = txt.Text.Substring(lastword);
            
            var wList = arList.ToList();

            if (wList.Exists(x => x.Word == GotLastWord))
            {
                int wordIndex = wList.IndexOf(wList.Where(w => w.Word == GotLastWord).FirstOrDefault());
                txt.SelectionStart = txt.Text.Length;
                wordcount = txt.Text.Length;
                lbAutoReplace.Items.Clear();
                lbAutoReplace.Items.Add(wList[wordIndex].ReplaceWord);
                InsertWord = wList[wordIndex].ReplaceWord;

                lbAutoReplace.SelectedIndex = 0;
                txt.SelectionStart = txt.Text.Length;
                var elementbound = txt.GetElementBounds(this);
                popupAR.VerticalOffset = elementbound.Bottom;
                popupAR.HorizontalOffset = elementbound.Left + txt.SelectionStart;
                matched = true;
                popupAR.IsOpen = true;
            }
        }  
        private void btnRemoveWord_Click(object sender, RoutedEventArgs e)
        {
            arList.RemoveAt(AutoReplaceList.SelectedIndex);
        }

        #endregion



        private void btnEmailCopy_Click(object sender, RoutedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            CopyToClipboard(txt.Text);
            
        }
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

        private void tbIssue_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            AutoReplace(tbIssue);
            if(tbIssue.Text.Length != wordcount)
            {
                popupAR.IsOpen = false;
            }
        }

        private void tbIssue_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            arKeyDown(tbIssue, e);
        }
        
        private void tbResolution_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            arKeyDown(tbResolution, e);
        }
        private void arKeyDown(TextBox txt, KeyRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(GotLastWord))
            {
                return;
            }
            else if (matched && e.Key == VirtualKey.Enter)
            {
                int lastword = txt.Text.LastIndexOf(GotLastWord);
                if (lastword <= 0) { lastword = 0; }
          
                txt.Text = txt.Text.Remove(lastword, GotLastWord.Length).Insert(lastword, InsertWord);
                e.Handled = true;
                txt.Text = txt.Text + " ";
                txt.SelectionStart = txt.Text.Length + 1;
                popupAR.IsOpen = false;
            }
            else 
            {
                popupAR.IsOpen = false;
            }
        }

        private void tbDevice_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                tbDevice.Text = args.ChosenSuggestion.ToString();
            }
        }

        private void tbDevice_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var selectedItem = args.SelectedItem.ToString();
            sender.Text = selectedItem;
        }

        private void tbDevice_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                deviceSuggestion = SuggestionList.Device.Where(x => x.StartsWith(sender.Text)).ToList();
                sender.ItemsSource = deviceSuggestion;
            }
        }

        private void tbEmail_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                tbEmail.Text = args.ChosenSuggestion.ToString();
            }
        }

        private void tbEmail_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var selectedItem = args.SelectedItem.ToString();
            sender.Text = sender.Text.Replace("@", selectedItem);
        }

        private void tbEmail_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                string LastChar = string.Empty;
                if (!string.IsNullOrEmpty(sender.Text)) { LastChar = sender.Text.Substring(sender.Text.Length - 1); }
                emailSuggestion = SuggestionList.Email.Where(x => x.StartsWith(LastChar)).ToList();
                sender.ItemsSource = emailSuggestion;
            }
        }

        private void tbContact_TextChanged(object sender, TextChangedEventArgs e)
        {
            string numericPhone = new string(tbContact.Text.ToCharArray().Where(c => Char.IsDigit(c)).ToArray());
            tbContact.Text = string.Format("(###) ### - ####", numericPhone);
        }

        

        private void tbResolution_TextChanged(object sender, TextChangedEventArgs e)
        {
            AutoReplace(tbResolution);
           
        }

        private void tbIssue_TextChanged(object sender, TextChangedEventArgs e)
        {
            AutoReplace(tbIssue);
          
        }

        private void tbAddWord_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == VirtualKey.Enter)
            {
                if(FocusManager.GetFocusedElement() == tbAddWord)
                {
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
                }
                else
                {
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
                }
                e.Handled = true;
            }
        }
    }
}

