using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_Buddy
{
    abstract class WirelessGateways
    {

        private string technicolorPoD;
        private bool pm;
        private string arrisPoD;
        private string ciscoPoD;
        private string doryPoD;
        private string smcPoD;

        private string getpod(string PoD)
        {
            try
            {
                int pos = 0;
                if (!string.IsNullOrEmpty(PoD))
                {
                    string DayNum = DateTime.Now.ToString("dd-MMM-yy").ToUpper();

                    if (pm == true)
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
                    return PoD = null;
                }
            }
            catch
            {
                return PoD = "EXPIRED";
            }

        }
        private bool getexpiry(string PoD)
        {
            int CurrentDay = int.Parse(DateTime.Now.ToString("dd"));
            int podDay;

            int pos = 0;
            if (!string.IsNullOrEmpty(PoD))
            {
                string DayNum = DateTime.Now.ToString("dd-MMM-yy").ToUpper();

                if (pm == true)
                {
                    DateTime NextDay = DateTime.Now;
                    DayNum = NextDay.AddDays(1).ToString("dd-MMM-yy").ToUpper();
                }
                string[] spod = PoD.Split('\t', '\n');

                pos = Array.FindIndex(spod, row => row.Contains(DayNum));

                podDay = int.Parse(DateTime.Parse(spod.ElementAt(pos += 1)).ToString("d"));
            }
            else { return true; }

            if (CurrentDay - podDay == 0) { return false; }
            else { return true; }
        }

        public class technicolor : WirelessGateways
        {
            public string PoD
            {
                get { return technicolorPoD; }
                set { technicolorPoD = PoD; }
            }
            public string GetPoD
            {
                get { return getpod(technicolorPoD); }
            }
            public bool PM
            {
                get { return pm; }
                set { pm = PM; }
            }
            public bool GetExpiry
            {
                get { return getexpiry(technicolorPoD); }
            }

        }
        public class arris : WirelessGateways
        {
            public string PoD
            {
                get { return arrisPoD; }
                set { arrisPoD = PoD; }
            }
            public string GetPoD
            {
                get { return getpod(arrisPoD); }
            }
            public bool GetExpiry
            {
                get { return getexpiry(arrisPoD); }
            }
        }
        public class cisco : WirelessGateways
        {
            public string PoD
            {
                get { return ciscoPoD; }
                set { ciscoPoD = PoD; }
            }
            public string GetPoD
            {
                get { return getpod(ciscoPoD); }
            }
            public bool GetExpiry
            {
                get { return getexpiry(ciscoPoD); }
            }
        }
        public class dory : WirelessGateways
        {
            public string PoD
            {
                get { return doryPoD; }
                set { doryPoD = PoD; }
            }
            public string GetPoD
            {
                get { return getpod(doryPoD); }
            }
            public bool GetExpiry
            {
                get { return getexpiry(doryPoD); }
            }
        }
        public class smc : WirelessGateways
        {
            public string PoD
            {
                get { return smcPoD; }
                set { smcPoD = PoD; }
            }
            public string GetPoD
            {
                get { return getpod(smcPoD); }
            }
            public bool GetExpiry
            {
                get { return getexpiry(smcPoD); }
            }
        }



    }
}


