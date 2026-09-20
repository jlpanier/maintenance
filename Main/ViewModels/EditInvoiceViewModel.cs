using FFImageLoading.Helpers;
using System.Windows.Input;

namespace Main.ViewModels
{
    public class EditInvoiceViewModel:BaseViewModel
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
        /// Sauvegarde des données
        /// </summary>
        public ICommand ClickInvoice => new Command(OnInvoice);

        /// <summary>
        /// Visualisation de la facture courante
        /// </summary>
        public ICommand ClickView => new Command(OnView);


        /// <summary>
        /// Date d'effet de la note
        /// </summary>
        public DateTime EffectiveOn
        {
            get { return _effectiveOn; }
            set
            {
                if (_effectiveOn != value)
                {
                    _effectiveOn = value;
                    NotifyPropertyChanged(nameof(EffectiveOn));
                }
            }
        }
        private DateTime _effectiveOn = DateTime.Today;

        /// <summary>
        /// Fournisseur de la facturre
        /// </summary>
        public string Supplier
        {
            get { return _supplier; }
            set
            {
                if (_supplier != value)
                {
                    _supplier = value;
                    NotifyPropertyChanged(nameof(Supplier));
                }
            }
        }
        private string _supplier = string.Empty;

        /// <summary>
        /// Fournisseur de la facturre
        /// </summary>
        public string Amount
        {
            get { return _amount; }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    NotifyPropertyChanged(nameof(Amount));
                }
            }
        }
        private string _amount = string.Empty;
        
        /// <summary>
        /// chemin physique de la facture
        /// </summary>
        public string InvoicePath
        {
            get { return _invoicePath; }
            set
            {
                if (_invoicePath != value)
                {
                    _invoicePath = value;
                    NotifyPropertyChanged(nameof(InvoicePath));
                    IsInvoiceSelected = File.Exists(InvoicePath);
                }
            }
        }
        private string _invoicePath = string.Empty;

        /// <summary>
        /// VRAI, si la facture a été choisie
        /// </summary>
        public bool IsInvoiceSelected
        {
            get { return _isInvoiceSelected; }
            set
            {
                if (_isInvoiceSelected != value)
                {
                    _isInvoiceSelected = value;
                    NotifyPropertyChanged(nameof(IsInvoiceSelected));
                }
            }
        }
        private bool _isInvoiceSelected = false;
        
        /// <summary>
        /// Initialisation de la page
        /// </summary>
        public virtual void Init(int key)
        {
            _invoicekey = key;
            var invoice = Business.Invoice.GetById(key);
            if (invoice != null)
            {
                EffectiveOn = invoice.EffectiveOn;
                Supplier = invoice.Supplier;
                InvoicePath = invoice.Path;
                Amount = invoice.Amount.ToString();
            }  
            else
            {
                EffectiveOn = DateTime.Today;
                Supplier = Business.Settings.Instance.InvoiceSupplierDefault;
                InvoicePath = string.Empty;
                Amount = string.Empty;
            }
        }
        private int _invoicekey;

        /// <summary>
        /// Sauvegarde des données
        /// </summary>
        public async virtual void OnSave()
        {
            try
            {
                if (!double.TryParse(Amount.Replace(",", "."), out double amount))
                {
                    throw new Exception("Montant incorrecte");
                }
                if (amount <= 0)
                {
                    throw new Exception("Montant incorrecte");
                }
                if (string.IsNullOrEmpty(Supplier))
                {
                    throw new Exception("Fournisseur incorrecte");
                }
                if (string.IsNullOrEmpty(InvoicePath))
                {
                    throw new Exception("Facture incorrecte");
                }
                if (!File.Exists(InvoicePath))
                {
                    throw new Exception("Chemin de la facture incorrecte");
                }
                var invoice = Business.Invoice.GetById(_invoicekey);
                if (invoice != null)
                {
                    invoice.Update(EffectiveOn, Supplier, InvoicePath, amount);
                }
                else
                {
                    Business.Invoice.Create(EffectiveOn, Supplier, InvoicePath, amount);
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
                var invoice = Business.Invoice.GetById(_invoicekey);
                if (invoice != null)
                {
                    invoice.Delete();
                }

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await ServiceHelper.GetService<IAlertService>()!.ShowAlertAsync(ex);
            }
        }

        /// <summary>
        /// Sélection de la copie de la facture
        /// </summary>
        public async void OnInvoice()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Choisir une facture au format PDF",
                    FileTypes = FilePickerFileType.Pdf
                });

                if (result != null)
                {
                    InvoicePath = result.FullPath;
                }
            }
            catch (Exception ex)
            {
                await ServiceHelper.GetService<IAlertService>()!.ShowAlertAsync(ex);
            }
        }

        /// <summary>
        /// Visualisation de la facture
        /// </summary>
        public async void OnView()
        {
            await Shell.Current.GoToAsync($"{nameof(WebViewPage)}", new Dictionary<string, object>
            {
                ["Path"] = InvoicePath,
            });
        }
    }
}
