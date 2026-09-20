using FFImageLoading.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Main.ViewModels
{
    internal class EditWorkViewModel:EditNoteViewModel
    {
        /// <summary>
        /// Sauvegarde des données
        /// </summary>
        public ICommand ClickInvoice => new Command(OnInvoice);

        /// <summary>
        /// Ensembles des lignes de maintenances
        /// </summary>
        public ObservableCollection<Business.Invoice> Invoices
        {
            get => _invoices;
            set
            {
                _invoices = value;
                NotifyPropertyChanged(nameof(Invoices));
            }
        }
        private ObservableCollection<Business.Invoice> _invoices = new ObservableCollection<Business.Invoice>();

        public Business.Invoice? SelectedInvoice
        {
            get => _selectedInvoice;
            set
            {
                if (_selectedInvoice != value)
                {
                    _selectedInvoice = value;
                    NotifyPropertyChanged(nameof(SelectedInvoice));
                }
            }
        }
        private Business.Invoice? _selectedInvoice;

        /// <summary>
        /// Initialisation de la page 
        /// </summary>
        public override void Init(int key)
        {
            _key = key;
            base.Init(Business.Line.GetLine(_key));
        }


        /// <summary>
        /// Initialisation de la page 
        /// </summary>
        protected override void Init(Business.ILine? item)
        {
            base.Init(item);
            Invoices = new ObservableCollection<Business.Invoice>(Business.Invoice.All);
            if (item is Business.Work work)
            {
                SelectedInvoice = Invoices.FirstOrDefault(i => i.Id == work.InvoiceId);
            }
        }


        /// <summary>
        /// Gestion des factures
        /// </summary>
        public async void OnInvoice()
        {
            try
            {
                await Shell.Current.GoToAsync($"{nameof(InvoicesPage)}", new Dictionary<string, object>
                {
                    ["Id"] = 0,
                });
            }
            catch (Exception ex)
            {
                await ServiceHelper.GetService<IAlertService>()!.ShowAlertAsync(ex);
            }
        }

        /// <summary>
        /// Sauvegarde des données
        /// </summary>
        public async override void OnSave()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Desc))
                {
                    throw new Exception("Une description doit être précisée.");
                }

                if (SelectedInvoice==null)
                {
                    throw new Exception("Sélection d'une facture.");
                }

                var line = Business.Line.GetLine(_key);
                if (line == null)
                {
                    Business.Work.Create(EffectiveOn, Desc, Images, SelectedInvoice);
                }
                else if (line is Business.Work item)
                {
                    item.Update(EffectiveOn, Desc, Images, SelectedInvoice);
                }

                await Shell.Current.GoToAsync(".."); // Retour à la page précédente
            }
            catch (Exception ex)
            {
                await ServiceHelper.GetService<IAlertService>()!.ShowAlertAsync(ex);
            }
        }

        /// <summary>
        /// Sauvegarde des données
        /// </summary>
        public async override void OnCancel()
        {
            await Shell.Current.GoToAsync(".."); // Retour à la page précédente
        }

        /// <summary>
        /// Suppression de la ligne
        /// </summary>
        public async override void OnDelete()
        {
            var item = Business.Line.GetLine(_key);
            if (item is Business.Work work)
            {
                work.Delete();
            }
            await Shell.Current.GoToAsync(".."); // Retour à la page précédente
        }
    }
}
