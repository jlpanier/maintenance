using Main.ViewModels;

namespace Main.Pages;

/// <summary>
/// Gestion de la page d'édition des produits de maintenance
/// </summary>
public partial class EditProductPage : ContentPage, IQueryAttributable
{
    /// <summary>
    /// Applique les attributs de requête
    /// </summary>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (BindingContext is EditProductViewModel vm)
        {
            if (query.TryGetValue("Id", out var objId) && objId is int key)
            {
                vm.Init(key);
            }
        }
    }

    public EditProductPage()
	{
		InitializeComponent();
	}
}