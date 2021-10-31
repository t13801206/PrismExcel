using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using PrismExcel.Services.Interfaces;
using System.Collections.ObjectModel;

namespace PrismExcel.ViewModels
{
    public class FilterDialogViewModel : BindableBase, IDialogAware
    {
        public ReactiveCommand OkCommand { get; } = new ReactiveCommand();

        public ReactiveCommand CancelCommand { get; } = new ReactiveCommand();

        public List<IFilterItem> ItemSource { get; set; } = new List<IFilterItem>();

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
