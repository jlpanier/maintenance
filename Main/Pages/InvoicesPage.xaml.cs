using Main.ViewModels;

namespace Main.Pages;

public partial class InvoicesPage : ContentPage
{
	public InvoicesPage()
	{
		InitializeComponent();
	}
    /// <summary>
    /// customize behavior immediately prior to the page becoming visible.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is InvoicesViewModel vm)
        {
            vm.Load();
        }
    }

}