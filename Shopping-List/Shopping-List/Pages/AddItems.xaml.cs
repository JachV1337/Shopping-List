using Shopping_List.ViewModel;

namespace Shopping_List.Pages;

public partial class AddItems : ContentPage
{
	public AddItems()
	{
		InitializeComponent();
		BindingContext = App.Services?.GetService<ListViewModel>();
	}
}