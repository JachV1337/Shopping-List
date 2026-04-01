using CommunityToolkit.Mvvm.Input;
using Shopping_List.Model;
using System.Collections.ObjectModel;

namespace Shopping_List.ViewModel
{
    public partial class ListViewModel : BaseViewModel
    {
        public ObservableCollection<ShoppingItemModel> ShoppingItems { get; set; }

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
        }
        [RelayCommand]
        public void AddItem(string itemName)
        {
            if (!string.IsNullOrWhiteSpace(itemName))
            {
                ShoppingItems.Add(new ShoppingItemModel { Name = itemName, IsChecked = false });
            }
        }
    }
}