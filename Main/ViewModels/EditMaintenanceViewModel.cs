using FFImageLoading.Helpers;
using System.Windows.Input;

namespace Main.ViewModels
{
    public class EditMaintenanceViewModel: BaseViewModel
    {
        /// <summary>
        /// Sauvegarde des données
        /// </summary>
        public ICommand ClickSave => new Command(OnSave);

        /// <summary>
        /// Sauvegarde des données
        /// </summary>
        public ICommand ClickCancel => new Command(OnCancel);

        /// <summary>
        /// Sauvegarde des données
        /// </summary>
        public ICommand ClickDelete => new Command(OnDelete);

        /// <summary>
        /// Nom
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged(nameof(Name));
                }
            }
        }
        private string _name = string.Empty;

        /// <summary>
        /// Initialisation de la page
        /// </summary>
        public void Init(int key)
        {
            _objectMaintenance = key;
            var item = Business.MaintenanceObject.GetById(key);
            if (item != null)
            {
                Name = item.Name;
            }
        }
        private int _objectMaintenance;

        /// <summary>
        /// Sauvegarde des données
        /// </summary>
        public async void OnSave()
        {
            try
            {
                if (string.IsNullOrEmpty(Name))
                {
                    throw new Exception("Nom incorrect");
                }
                var item = Business.MaintenanceObject.GetById(_objectMaintenance);
                if (item != null)
                {
                    item.Update(Name);
                }
                else
                {
                    Business.MaintenanceObject.Create(Name);
                }
                await Shell.Current.GoToAsync(".."); // Retour à la page précédente
            }
            catch (Exception ex)
            {
                await ServiceHelper.GetService<IAlertService>()!.ShowAlertAsync(ex);
            }
        }

        /// <summary>
        /// Suppression d'une image à la note
        /// </summary>
        public async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        /// <summary>
        /// Suppression d'une image à la note
        /// </summary>
        public async void OnDelete()
        {
            try
            {
                var item = Business.MaintenanceObject.GetById(_objectMaintenance);
                if (item != null)
                {
                    item.Delete();
                }
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await ServiceHelper.GetService<IAlertService>()!.ShowAlertAsync(ex);
            }
        }
    }
}
