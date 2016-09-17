using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_Buddy
{
    public class AutoReplaceDictionary : INotifyPropertyChanged
    {
        private string word;
        private string replaceword;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Word
        {
            get { return word; }
            set { word = value; OnPropertyChanged("Word"); }
        }

        public string ReplaceWord
        {
            get { return replaceword; }
            set { replaceword = value; OnPropertyChanged("ReplaceWord"); }
        }

        protected void OnPropertyChanged(string changeName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(changeName));
            }
        }


    }
}
