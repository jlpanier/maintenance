using Main.ViewModels;

namespace Main.Pages;

public partial class EditMaintenancePage : ContentPage, IQueryAttributable
{
    /// <summary>
    /// Applique les attributs de requête
    /// </summary>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (BindingContext is EditMaintenanceViewModel vm)
        {
            if (query.TryGetValue("Id", out var objId) && objId is int key)
            {
                vm.Init(key);
            }
        }
    }

    public EditMaintenancePage()
	{
		InitializeComponent();
	}
}