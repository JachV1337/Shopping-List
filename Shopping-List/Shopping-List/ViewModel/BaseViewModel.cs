using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping_List.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        bool isBusy;
    }
}
