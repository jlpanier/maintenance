using Business;
using FFImageLoading.Helpers;
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
        /// Exporter les factures dans un fichier zip
        /// </summary>
        public ICommand ClickZip => new Command(OnZip);

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

        /// <summary>
        /// Zone de recherche du texte de recherche
        /// </summary>
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    NotifyPropertyChanged(nameof(SearchText));
                    Load(_searchText);
                }
            }
        }
        private string _searchText = string.Empty;

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
        public void Load(string filtertext="")
        {
            if (MaintenanceObject.Current!=null)
            {
                if (string.IsNullOrWhiteSpace(filtertext))
                {
                    Lines = new ObservableCollection<Business.ILine>(Business.Line.All.Where(_ => _.MaintenanceObjectId == MaintenanceObject.Current.Id).OrderByDescending(_ => _.EffectiveOn));
                }
                else 
                {
                    string search = filtertext.ToLower();
                    var selecteditems = new List<Business.ILine>();
                    foreach(var line in Business.Line.All.Where(_ => _.MaintenanceObjectId == MaintenanceObject.Current.Id))
                    {
                        if (line.Name.ToLower().Contains(search))
                        {
                            selecteditems.Add(line);
                        }
                    }
                    Lines = new ObservableCollection<Business.ILine>(selecteditems.OrderByDescending(_ => _.EffectiveOn));
                }

                Name = MaintenanceObject.Current.Name;
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

        /// <summary>
        /// Externaliser les factures dans un fichier zip
        /// </summary>
        private async void OnZip()
        {
            try
            {
                MenuVisible = !MenuVisible;

                var files = Directory.GetFiles(Appli.Instance.FilePath, "MAINTENANCE_*.zip");
                foreach (var file in files)
                {
                    File.Delete(file);
                }
#if ANDROID
                var filename = $"MAINTENANCE_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.zip";
                string zipFile = System.IO.Path.Combine(DownloadFolderService.GetDownloadFolder(), System.IO.Path.GetFileName(filename));

                if (File.Exists(zipFile))
                {
                    File.Delete(zipFile);
                }

                System.IO.Compression.ZipFile.CreateFromDirectory(Appli.Instance.AppPath, zipFile);
                await ServiceHelper.GetService<IAlertService>()!.ShowAlertAsync($"Le fichier {filename} a été créé avec succès.");

#endif

            }
            catch (Exception ex)
            {
                await ServiceHelper.GetService<IAlertService>()!.ShowAlertAsync(ex);
            }
        }
    }
}
