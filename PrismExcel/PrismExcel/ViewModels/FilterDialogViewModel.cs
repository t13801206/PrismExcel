using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using PrismExcel.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Diagnostics;

namespace PrismExcel.ViewModels
{
    public class FilterDialogViewModel : BindableBase, IDialogAware
    {
        public ReactiveCommand OkCommand { get; } = new ReactiveCommand();

        public ReactiveCommand CancelCommand { get; } = new ReactiveCommand();

        public ObservableCollection<IFilterItem> ItemSource { get; set; } = new ObservableCollection<IFilterItem>();

        public ReactiveProperty<string> FilterText { get; set; } = new ReactiveProperty<string>("");

        private readonly ICollectionView _view;

        public FilterDialogViewModel()
        {
            OkCommand.Subscribe(() =>
            {
                RequestClose.Invoke(new DialogResult(ButtonResult.OK));
            });

            CancelCommand.Subscribe(() =>
            {
                RequestClose.Invoke(new DialogResult(ButtonResult.Cancel));
            });

            _view = CollectionViewSource.GetDefaultView(ItemSource);

            _view.Filter = x =>
            {
                if (FilterText.Value == "")
                    return true;

                var filterItem = x as Models.FilterItem;
                return filterItem.Content.Contains(FilterText.Value);
            };

            FilterText.Subscribe(_ => _view.Refresh());
        }

        public string Title => "列のフィルタリング";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var filterItems = parameters.GetValue<ReadOnlyCollection<IFilterItem>>("filterItems");

            ItemSource.Clear();
            ItemSource.AddRange(filterItems);
        }
    }
}
