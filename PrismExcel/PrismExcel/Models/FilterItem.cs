using PrismExcel.Services.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PrismExcel.Models
{
    public class FilterItem : IFilterItem, INotifyPropertyChanged
    {
        public string Content { get; set; }

        private bool _isChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (value != _isChecked)
                {
                    _isChecked = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
