using Business;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Main.ViewModels
{

    /// <summary>
    /// Gestion des paramétres de la page de maintenance
    /// </summary>
    public class MaintenancesViewModel : BaseViewModel
    {
        /// <summary>
        /// Ajout d'une configuration 
        /// </summary>
        public ICommand ClickAdd => new Command(OnAdd);

        /// <summary>
        /// Maj d'une configuration 
        /// </summary>
        public ICommand ClickItem => new Command<MaintenanceObject>(OnItem);

        /// <summary>
        /// Annuler 
        /// </summary>
        public ICommand ClickCancel => new Command(OnCancel);

        /// <summary>
        /// Liste de la configuration
        /// </summary>
        public ObservableCollection<MaintenanceObject> Items
        {
            get => _items;
            set
            {
                if (_items != value)
                {
                    _items = value;
                    NotifyPropertyChanged(nameof(Items));
                }
            }
        }
        public ObservableCollection<MaintenanceObject> _items = new ObservableCollection<MaintenanceObject>();

        /// <summary>
        /// Initialisation des données
        /// </summary>
        public void Load()
        {
            Items = new ObservableCollection<MaintenanceObject>(MaintenanceObject.All);
        }

        /// <summary>
        /// Ajout 
        /// </summary>
        public async void OnAdd()
        {
            await Shell.Current.GoToAsync($"{nameof(EditMaintenancePage)}", new Dictionary<string, object>
            {
                ["Id"] = 0,
            });
        }

        /// <summary>
        /// Ajout 
        /// </summary>
        public async void OnItem(MaintenanceObject item)
        {
            await Shell.Current.GoToAsync($"{nameof(EditMaintenancePage)}", new Dictionary<string, object>
            {
                ["Id"] = item.Id,
            });
        }

        /// <summary>
        /// Annuler 
        /// </summary>
        public async void OnCancel()
        {
            await Shell.Current.GoToAsync(".."); // Retour à la page précédente
        }
    }

}
