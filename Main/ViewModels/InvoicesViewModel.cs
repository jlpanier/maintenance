using Business;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Main.ViewModels
{
    public class InvoicesViewModel : BaseViewModel
    {
        /// <summary>
        /// Ajout d'une facture
        /// </summary>
        public ICommand ClickAddInvoice => new Command(OnAdd);

        /// <summary>
        /// Ajout d'une facture
        /// </summary>
        public ICommand ClickEditInvoice => new Command<Invoice>(OnEditInvoice);

        /// <summary>
        /// Ajout d'une facture
        /// </summary>
        public async void OnAdd()
        {
            await Shell.Current.GoToAsync($"{nameof(EditInvoicePage)}", new Dictionary<string, object>
            {
                ["Id"] = 0,
            });
        }

        /// <summary>
        /// Ajout d'une facture
        /// </summary>
        public async void OnEditInvoice(Invoice invoice)
        {
            await Shell.Current.GoToAsync($"{nameof(EditInvoicePage)}", new Dictionary<string, object>
            {
                ["Id"] = invoice.Id,
            });
        }

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

        public void Load()
        {
            Invoices = new ObservableCollection<Business.Invoice>(Business.Invoice.All.OrderByDescending(_=>_.EffectiveOn));
        }
    }
}
