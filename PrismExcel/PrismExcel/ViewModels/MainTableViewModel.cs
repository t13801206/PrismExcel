using Prism.Mvvm;
using Prism.Services.Dialogs;
using PrismExcel.Core;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using PrismExcel.Services.Interfaces;

namespace PrismExcel.ViewModels
{
    public class MainTableViewModel : BindableBase
    {
        public ObservableCollection<Models.Contact> Contacts { get; set; }

        public ReactiveCommand<string> OpenFilterDialog { get; } = new ReactiveCommand<string>();

        public List<IFilterItem>[] filterItems = new List<IFilterItem>[]
        {
            new List<IFilterItem>(), new List<IFilterItem>(), new List<IFilterItem>(),
        };

        private readonly ICollectionView _view;

        private readonly Dictionary<string, int> headerNameToIndex = new Dictionary<string, int>()
        {
            {"名前", 0}, {"年齢", 1}, {"住所", 2},
        };

        public MainTableViewModel(IDialogService dialogService)
        {
            Contacts = new ObservableCollection<Models.Contact>(Models.Contact.GenerateInitList());
            _view = (CollectionView)CollectionViewSource.GetDefaultView(Contacts);

            UpdateAllFilterItems();

            OpenFilterDialog.Subscribe(x =>
            {
                UpdateAllFilterItems();

                dialogService.ShowDialog(
                    DialogNames.FilterDialog,
                    new DialogParameters
                    {
                        { "filterItems", new ReadOnlyCollection<IFilterItem>(filterItems[headerNameToIndex[x]]) }
                    },
                    res => Debug.WriteLine($"{res.Result}")
                    );

                _view.Filter = OnFilterTriggered;
            });
        }

        private bool OnFilterTriggered(object obj)
        {
            UpdateAllFilterItems();

            var contact = obj as Models.Contact;

            for (int i = 0; i < headerNameToIndex.Count; i++)
            {
                var checkStatus = filterItems[i].Find(x => x.Content == contact[i]).IsChecked;

                if (!checkStatus)
                    return false;
            }

            return true;
        }

        private void UpdateAllFilterItems()
        {
            for (int i = 0; i < headerNameToIndex.Count; i++)
            {
                UpdateFilterItems(i);
            }
        }

        private void UpdateFilterItems(int i)
        {
            var keyList = Contacts.GroupBy(x => x[i]).Select(x => x.Key).ToArray();

            var newFilterItems = keyList.GroupJoin(
                filterItems[i],
                x => x,
                y => y.Content,
                (key, names) => new Models.FilterItem
                {
                    IsChecked = !names.Select(x => x.Content).Contains(key) || names.FirstOrDefault(x => x.Content == key).IsChecked,
                    Content = key
                }
            ).ToArray();

            filterItems[i].Clear();

            var defaultFirstItem = new Models.FilterItem { Content = "（すべて選択）", IsChecked = true };
            defaultFirstItem.PropertyChanged += (s, _) => filterItems[i].ForEach(x => x.IsChecked = ((Models.FilterItem)s).IsChecked);

            filterItems[i].Add(defaultFirstItem);
            filterItems[i].AddRange(newFilterItems);
        }
    }
}
