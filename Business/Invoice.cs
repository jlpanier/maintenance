using Repository.Dbo;
using Repository.Entities;

namespace Business
{
    /// <summary>
    /// Gestion des factures
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Toutes les factures
        /// </summary>
        public static List<Invoice> All
        {
            get
            {
                if (_invoices==null)
                {
                    _invoices = new List<Invoice>();
                    var items = DatabaseAccess.Instance.GetInvoices();
                    foreach (var item in items)
                    {
                        _invoices.Add(new Invoice(item));
                    }
                }
                return _invoices;
            }
        }
        private static List<Invoice>? _invoices = null;

        /// <summary>
        /// Date de la facture
        /// </summary>
        public DateTime EffectiveOn => Item.EffectiveOn;

        /// <summary>
        /// Reference de la facture
        /// </summary>
        public int Id => Item.Id;

        /// <summary>
        /// Origine de la facture
        /// </summary>
        public string From => Item.From;

        /// <summary>
        /// Chemin PDF de la facture
        /// </summary>
        public string Path => Item.Path;

        /// <summary>
        /// Reference de l'entity de la facture
        /// </summary>
        public readonly InvoiceEntity Item;

        /// <summary>
        /// constructeur privé pour créer une instance de facture à partir d'une entity
        /// </summary>
        private Invoice(InvoiceEntity item)
        {
            Item = item;
        }
    }
}
