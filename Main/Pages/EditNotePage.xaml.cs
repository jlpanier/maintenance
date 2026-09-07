using Main.ViewModels;

namespace Main.Pages;

/// <summary>
/// Gestion de la page d'édition des notes de maintenance
/// </summary>
public partial class EditNotePage : ContentPage, IQueryAttributable
{
    /// <summary>
    /// Applique les attributs de requête
    /// </summary>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (BindingContext is EditNoteViewModel vm)
        {
            if (query.TryGetValue("Id", out var objId) && objId is int key)
            {
                vm.Init(key);
            }
        }
    }
    public EditNotePage()
	{
		InitializeComponent();
	}
}