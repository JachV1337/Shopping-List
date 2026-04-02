using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping_List.Model
{
    public class ShoppingItemModel
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public void ToggleChecked()
        {
            IsChecked = !IsChecked;
        }
    }
}
