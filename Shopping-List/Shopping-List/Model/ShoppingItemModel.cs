using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping_List.Model
{
    public partial class ShoppingItemModel : ObservableObject
    {
        public string Name { get; set; }
        [ObservableProperty]
        private bool isChecked;

        public void ToggleChecked()
        {
            IsChecked = !IsChecked;
        }
    }
}
