using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected string _message = string.Empty;
        public string ErrorMessage
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public string SuccessMessage
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(SuccessMessage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return;
            }

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}