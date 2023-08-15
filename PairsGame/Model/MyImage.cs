using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PairsGame.Model
{
    public class MyImage:INotifyPropertyChanged
    {
        private ImageSource imageSource;
        public ImageSource ImageSource
        {
            get
            {
                return imageSource;
            }
            set
            {
                imageSource = value;
                NotifyPropertyChanged("ImageSource");
            }
        }
        private bool enabled;
        public bool Enabled {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
                NotifyPropertyChanged("Enabled");
            }
        }

        public MyImage(ImageSource imgSource,bool enabledc) {
            ImageSource = imgSource;
            Enabled = enabledc;
        }
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }

}
