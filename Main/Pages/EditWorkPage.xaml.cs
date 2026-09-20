using Main.ViewModels;

namespace Main.Pages;

/// <summary>
/// Gestion de la page d'édition des travaux de maintenance
/// </summary>
public partial class EditWorkPage : ContentPage, IQueryAttributable
{
    /// <summary>
    /// Applique les attributs de requête
    /// </summary>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (BindingContext is EditWorkViewModel vm)
        {
            if (query.TryGetValue("Id", out var objId) && objId is int key)
            {
                vm.Init(key);
            }
        }
    }

    public EditWorkPage()
	{
		InitializeComponent();
	}
}