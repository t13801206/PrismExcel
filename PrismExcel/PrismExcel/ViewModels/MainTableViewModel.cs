using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismExcel.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace PrismExcel.ViewModels
{
    public class MainTableViewModel : BindableBase
    {
        public ObservableCollection<Models.Contact> Contacts { get; set; }

        private readonly ICollectionView _view;

        public MainTableViewModel()
        {
            Contacts = new ObservableCollection<Models.Contact>(Models.Contact.GenerateInitList());
            _view = (CollectionView)CollectionViewSource.GetDefaultView(Contacts);
        }
    }
}
