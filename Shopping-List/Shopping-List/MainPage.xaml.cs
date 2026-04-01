using Shopping_List.ViewModel;

namespace Shopping_List
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = App.Services?.GetService<ListViewModel>();
        }
    }

}
