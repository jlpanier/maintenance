using Main.ViewModels;

namespace Main.Pages;

/// <summary>
/// Gestion de la page d'édition des paramétre de maintenance
/// </summary>
public partial class EditSettingPage : ContentPage, IQueryAttributable
{
    /// <summary>
    /// Applique les attributs de requête
    /// </summary>
    /// <param name="query">Les attributs de requête</param>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (BindingContext is EditSettingViewModel vm)
        {
            if (query.TryGetValue("EffectiveOn", out var objId) && objId is string key)
            {
                vm.Init(key);
            }
        }
    }

    public EditSettingPage()
	{
		InitializeComponent();
	}
}