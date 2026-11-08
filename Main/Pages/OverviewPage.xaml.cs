using Main.ViewModels;

namespace Main.Pages;


/// <summary>
/// Affichage des dépenses annuelles
/// </summary>
public partial class OverviewPage : ContentPage
{
    public OverviewPage(OverviewViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    public OverviewPage()
	{
		InitializeComponent();
        BindingContext = new OverviewViewModel();
    }
    

}