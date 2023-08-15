using GalaSoft.MvvmLight.Messaging;
using PairsGame.Commands;
using PairsGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PairsGame.ViewModel
{
    public class CustomSizeViewModel : ViewModelBase
    {
        private int rows;
        public int Rows
        {
            get { return rows; }
            set
            {
                rows = value;
                SetPropertyChanged("Rows");

            }
        }
        private int cols;
        public int Cols
        {
            get { return cols; }
            set
            {
                cols = value;
                SetPropertyChanged("Cols");

            }
        }
        private ICommand oKCommand;
        public ICommand OKCommand
        {
            get
            {
                if (oKCommand == null)
                    oKCommand = new RelayCommand(OKCom);
                return oKCommand;
            }
        }

        private void OKCom(object obj)
        {
            Messenger.Default.Send(new CustomSizeMessage { Rows = rows, Columns = cols });
            DependencyObject view = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (view != null)
            {
                ((Window)view).Close();
            }
        }

        public CustomSizeViewModel() { }


    }
}
