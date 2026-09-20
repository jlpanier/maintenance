using Business;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace Main.ViewModels
{
    /// <summary>
    /// Gestion de la page principale
    /// </summary>
    public partial class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// Gestion de la configuration
        /// </summary>
        public ICommand ClicSettings => new Command(OnSettings);

        /// <summary>
        /// Gestion des factures
        /// </summary>
        public ICommand ClicInvoices => new Command(OnInvoices);

        /// <summary>
        /// Evenement de la mise à jour du paramétrages
        /// </summary>
        public ICommand ClickSettingsCommand => new Command(OnSettings);

        /// <summary>
        /// Clic sur le bouton du menu
        /// </summary>
        public ICommand ClicMenu => new Command(OnMenu);

        /// <summary>
        /// Evenement d'ajout d'un produit de maintenance
        /// </summary>
        public ICommand ClicProduct => new Command(OnProduct);

        /// <summary>
        /// Evenement d'une modification d'une note de maintenance
        /// </summary>
        public ICommand ClickNote => new Command<Note>(OnNote);

        /// <summary>
        /// Evenement d'ajout d'un nouvelle note de maintenance
        /// </summary>
        public ICommand ClicNewNote => new Command(OnNewNote);

        /// <summary>
        /// Evenement d'ajout d'un travail de maintenance
        /// </summary>
        public ICommand ClicWork => new Command(OnWork);

        /// <summary>
        /// Evenement d'ajout d'un nouvelle note de maintenance
        /// </summary>
        public ICommand ClicNewWork => new Command(OnNewWork);

        /// <summary>
        /// VRAI, si le popup menu doit être visible
        /// </summary>
        public bool MenuVisible
        {
            get => _menuVisible;
            set
            {
                _menuVisible = value;
                NotifyPropertyChanged(nameof(MenuVisible));
            }
        }
        private bool _menuVisible;

        /// <summary>
        /// Ensembles des lignes de maintenances
        /// </summary>
        public ObservableCollection<Business.ILine> Lines
        {
            get => _lines;
            set
            {
                if (_lines != value)
                {
                    _lines = value;
                    NotifyPropertyChanged(nameof(Lines));
                }
            }
        }
        public ObservableCollection<Business.ILine> _lines = new ObservableCollection<Business.ILine>();

        public MainViewModel()
        {
        }

        /// <summary>
        /// Edition du paramétrage
        /// </summary>
        private async void OnSettings()
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }

        /// <summary>
        /// Affichage du menu
        /// </summary>
        private async void OnMenu()
        {
            MenuVisible = !MenuVisible;
        }

        /// <summary>
        /// Affichage de la page de l'édition produit
        /// </summary>
        private async void OnProduct()
        {
            MenuVisible = false;
        }

        /// <summary>
        /// Affichage de la page de l'édition d'un travail
        /// </summary>
        private async void OnWork()
        {
            MenuVisible = false;
        }

        /// <summary>
        /// Affichage de la page de l'édition d'une nouvelle note
        /// </summary>
        private async void OnNewNote()
        {
            MenuVisible = false;
            await Shell.Current.GoToAsync($"{nameof(EditNotePage)}", new Dictionary<string, object>
            {
                ["Id"] = 0,
            });
        }

        /// <summary>
        /// Affichage de la page de l'édition d'une note existante
        /// </summary>
        private async void OnNote(Note item)
        {
            MenuVisible = false;
            await Shell.Current.GoToAsync($"{nameof(EditNotePage)}", new Dictionary<string, object>
            {
                ["Id"] = item.Id,
            });
        }

        /// <summary>
        /// Affichage de la page de l'édition d'une nouvelle ligne de travail
        /// </summary>
        private async void OnNewWork()
        {
            MenuVisible = false;
            await Shell.Current.GoToAsync($"{nameof(EditWorkPage)}", new Dictionary<string, object>
            {
                ["Id"] = 0,
            });
        }

        /// <summary>
        /// Gestion des factures
        /// </summary>
        private async void OnInvoices()
        {
            MenuVisible = false;
            await Shell.Current.GoToAsync($"{nameof(InvoicesPage)}", new Dictionary<string, object>
            {
                ["Id"] = 0,
            });
        }

        /// <summary>
        /// Chargement de toutes les lignes de maintenance
        /// </summary>
        public void Load()
        {
            Lines = new ObservableCollection<Business.ILine>(Business.Line.All);
        }
    }
}
