using Main.ViewModels;

namespace Main.Pages;

public partial class MaintenancesPage : ContentPage
{
	public MaintenancesPage()
	{
		InitializeComponent();
	}

    /// <summary>
    /// customize behavior immediately prior to the page becoming visible.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MaintenancesViewModel vm)
        {
            vm.Load();
        }
    }
}