using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using PrismExcel.Core;
using Reactive.Bindings;
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

        public ReactiveCommand OpenFilterDialog { get; } = new ReactiveCommand();

        private readonly ICollectionView _view;

        public MainTableViewModel(IDialogService dialogService)
        {
            Contacts = new ObservableCollection<Models.Contact>(Models.Contact.GenerateInitList());
            _view = (CollectionView)CollectionViewSource.GetDefaultView(Contacts);

            OpenFilterDialog.Subscribe(() => 
            {
                dialogService.ShowDialog(DialogNames.FilterDialog);
            });
        }
    }
}
