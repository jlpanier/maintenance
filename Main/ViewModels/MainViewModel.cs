using Business;
using Microsoft.Maui.Controls.Shapes;
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
        public ICommand ClickMaintenance => new Command(OnMaintenance);
        

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
        public ICommand ClicUp => new Command(OnUp);

        /// <summary>
        /// Clic sur le bouton du menu
        /// </summary>
        public ICommand ClicDown => new Command(OnDown);

        /// <summary>
        /// Clic sur le bouton du menu
        /// </summary>
        public ICommand ClicMenu => new Command(OnMenu);

        /// <summary>
        /// Evenement d'une modification d'une note de maintenance
        /// </summary>
        public ICommand ClickProduct => new Command<Product>(OnProduct);

        /// <summary>
        /// Evenement d'ajout d'un produit de maintenance
        /// </summary>
        public ICommand ClicNewProduct => new Command(OnNewProduct);

        /// <summary>
        /// Evenement d'une modification d'une note de maintenance
        /// </summary>
        public ICommand ClickNote => new Command<Business.Line>(OnNote);

        /// <summary>
        /// Evenement d'ajout d'un nouvelle note de maintenance
        /// </summary>
        public ICommand ClicNewNote => new Command(OnNewNote);

        /// <summary>
        /// Evenement d'ajout d'un travail de maintenance
        /// </summary>
        public ICommand ClickWork => new Command<Work>(OnWork);

        /// <summary>
        /// Evenement d'ajout d'un nouvelle note de maintenance
        /// </summary>
        public ICommand ClicNewWork => new Command(OnNewWork);

        /// <summary>
        /// Evenement de sélection de la maintenance précédente
        /// </summary>
        public ICommand PreviousCommand => new Command(OnPrevious);

        /// <summary>
        /// Evenement de sélection de la maintenance suivante
        /// </summary>
        public ICommand NextCommand => new Command(OnNext);

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
        /// Nom de l'objet de maintenance
        /// </summary>
        public string Name
        {
            get => _name;
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
        /// Montant total dépensé pour le bateau
        /// </summary>
        public string TotalAmount
        {
            get => _totalAmount;
            set
            {
                if (_totalAmount != value)
                {
                    _totalAmount = value;
                    NotifyPropertyChanged(nameof(TotalAmount));
                }
            }
        }
        private string _totalAmount = string.Empty;

        /// <summary>
        /// Montant annuel dépensé pour le bateau
        /// </summary>
        public string AnnualAmount
        {
            get => _annualAmount;
            set
            {
                if (_annualAmount != value)
                {
                    _annualAmount = value;
                    NotifyPropertyChanged(nameof(AnnualAmount));
                }
            }
        }
        private string _annualAmount = string.Empty;

        /// <summary>
        /// Montant annuel dépensé pour le bateau
        /// </summary>
        public double LastScrollY
        {
            get => _lastScrollY;
            set
            {
                if (_lastScrollY != value)
                {
                    _lastScrollY = value;
                    NotifyPropertyChanged(nameof(LastScrollY));
                }
            }
        }
        private double _lastScrollY = 0;

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
            MenuVisible = false;
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }

        /// <summary>
        /// Choix de la maintenance
        /// </summary>
        private async void OnMaintenance()
        {
            MenuVisible = false;
            await Shell.Current.GoToAsync(nameof(MaintenancesPage));
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
        private async void OnNewProduct()
        {
            MenuVisible = false;
            await Shell.Current.GoToAsync($"{nameof(EditProductPage)}", new Dictionary<string, object>
            {
                ["Id"] = 0,
            });
        }

        /// <summary>
        /// Affichage de la page de l'édition produit
        /// </summary>
        private async void OnProduct(Product item)
        {
            MenuVisible = false;
            await Shell.Current.GoToAsync($"{nameof(EditProductPage)}", new Dictionary<string, object>
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
        /// Affichage de la page de l'édition d'un travail
        /// </summary>
        private async void OnWork(Work item)
        {
            MenuVisible = false;
            await Shell.Current.GoToAsync($"{nameof(EditWorkPage)}", new Dictionary<string, object>
            {
                ["Id"] = item.Id,
            });
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
        private async void OnNote(Business.Line item)
        {
            MenuVisible = false;
            await Shell.Current.GoToAsync($"{nameof(EditNotePage)}", new Dictionary<string, object>
            {
                ["Id"] = item.Id,
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
            if (MaintenanceObject.Current!=null)
            {
                Name = MaintenanceObject.Current.Name;
                Lines = new ObservableCollection<Business.ILine>(Business.Line.All.Where(_ => _.MaintenanceObjectId == MaintenanceObject.Current.Id).OrderByDescending(_ => _.EffectiveOn).Take(Settings.Instance.DisplayItems));
                TotalAmount = $"Total {Lines.Sum(_ => _.TotalAmount):N2} €";
                AnnualAmount = $"Annuel {Lines.Where(_=>_.EffectiveOn > DateTime.Now.AddYears(-1)).Sum(_ => _.TotalAmount):N2} €";
            }
        }

        /// <summary>
        /// Evenement de sélection de la maintenance précédente
        /// </summary>
        private void OnPrevious()
        {
            MaintenanceObject.Previous();
            Load();
        }

        /// <summary>
        /// Evenement de sélection de la maintenance suivante
        /// </summary>
        private void OnNext()
        {
            MaintenanceObject.Next();
            Load();
        }

        public void OnScroll(double scrollY)
        {
            if (_is_running) return;

            _is_running = true;
            if (scrollY > LastScrollY)
            {
                // Gérer le scroll vers le bas
                var line = Lines.Last();
                if (line!=null)
                {
                    var since = line.EffectiveOn;
                    Lines = new ObservableCollection<Business.ILine>(Business.Line.All.Where(_ => _.MaintenanceObjectId == MaintenanceObject.Current.Id && _.EffectiveOn < since).OrderByDescending(_ => _.EffectiveOn).Take(Settings.Instance.DisplayItems));
                }
            }
            else
            {
                // L’utilisateur scroll vers le haut
                var line = Lines.First();
                if (line != null)
                {
                    var since = line.EffectiveOn;
                    Lines = new ObservableCollection<Business.ILine>(Business.Line.All.Where(_ => _.MaintenanceObjectId == MaintenanceObject.Current.Id && _.EffectiveOn > since).OrderByDescending(_ => _.EffectiveOn).Take(Settings.Instance.DisplayItems));
                }
            }

            LastScrollY = scrollY;
            _is_running = false;
        }
        private bool _is_running = false;


        public void OnUp()
        {
            if (_is_running) return;

            _is_running = true;
            // L’utilisateur scroll vers le haut
            var line = Lines.First();
            if (line != null)
            {
                var since = line.EffectiveOn;
                var items = Business.Line.All.Where(_ => _.MaintenanceObjectId == MaintenanceObject.Current.Id && _.EffectiveOn > since);
                if (items.Any())
                {
                    Lines = new ObservableCollection<Business.ILine>(items.OrderByDescending(_ => _.EffectiveOn).Take(Settings.Instance.DisplayItems));
                }
            }
            _is_running = false;
        }

        public void OnDown()
        {
            if (_is_running) return;

            _is_running = true;

            var line = Lines.Last();
            if (line != null)
            {
                var since = line.EffectiveOn;
                var items = Business.Line.All.Where(_ => _.MaintenanceObjectId == MaintenanceObject.Current.Id && _.EffectiveOn < since);
                if (items.Any())
                {
                    Lines = new ObservableCollection<Business.ILine>(items.OrderByDescending(_ => _.EffectiveOn).Take(Settings.Instance.DisplayItems));
                }
            }

            _is_running = false;
        }
    }
}
