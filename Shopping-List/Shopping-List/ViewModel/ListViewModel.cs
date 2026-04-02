using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shopping_List.Model;
using System.Collections.ObjectModel;

namespace Shopping_List.ViewModel
{
    public partial class ListViewModel : BaseViewModel
    {
        public ObservableCollection<ShoppingItemModel> ShoppingItems { get; set; }

        [ObservableProperty]
        private int checkedCount;

        public double CheckedProgress => ShoppingItems.Count == 0 ? 0 : (double)CheckedCount / ShoppingItems.Count;
        public ListViewModel()
        {
            ShoppingItems = new ObservableCollection<ShoppingItemModel>();
            LoadItems();
        }
        
        private void LoadItems()
        {
            ShoppingItems.Add(new ShoppingItemModel { Name = "Milk", IsChecked = false });
            ShoppingItems.Add(new ShoppingItemModel { Name = "Bread", IsChecked = false });
            ShoppingItems.Add(new ShoppingItemModel { Name = "Eggs", IsChecked = false });
        }

        [RelayCommand]
        public void ClearItems()
        {
            ShoppingItems.Clear();
            UpdateCheckedCount();
        }

        [RelayCommand]
        public void RemoveItem(ShoppingItemModel item)
        {
            if (item != null)
            {
                ShoppingItems.Remove(item);
                UpdateCheckedCount();
            }
        }

        [RelayCommand]
        public void AddItem(string itemName)
        {
            if (!string.IsNullOrWhiteSpace(itemName))
            {
                ShoppingItems.Add(new ShoppingItemModel { Name = itemName, IsChecked = false });
                UpdateCheckedCount();
            }
        }
        [RelayCommand]
        public void ToggleItemChecked(ShoppingItemModel item)
        {
            if (item != null)
            {
                //item.ToggleChecked();
                UpdateCheckedCount();
            }
        }
        public void UpdateCheckedCount()
        {
            CheckedCount = ShoppingItems.Count(item => item.IsChecked);
            OnPropertyChanged(nameof(CheckedProgress));
        }
    }
}