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
        /// Affichage par défaut des factures
        /// </summary>
        public override string ToString() => $"Facture du {EffectiveOn.ToShortDateString()} de {Supplier} ({Amount} €)";
        
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
        /// Recherche une facture par son identifiant
        /// </summary>
        public static Invoice? GetById(int id) => All.FirstOrDefault(i => i.Id == id);

        /// <summary>
        /// Créer une facture de maintenance de travail
        /// </summary>
        public static Invoice Create(DateTime effectiveOn, string supplier, string invoicePath, double amount)
        {
            var destinationfile = CopyInvoiceFile(effectiveOn, supplier, invoicePath);
            var item = new InvoiceEntity
            {
                EffectiveOn = effectiveOn,
                Supplier = supplier,
                Amount = amount,
                InvoicePath= destinationfile,
                DateMaj = DateTime.Now,
            };
            DatabaseAccess.Instance.Add(item);

            var result = new Invoice(item);
            Invoice.All.Add(result);
            return result;
        }

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
        public string Supplier => Item.Supplier;

        /// <summary>
        /// Montant de la facture
        /// </summary>
        public double Amount => Item.Amount;

        /// <summary>
        /// Chemin PDF de la facture
        /// </summary>
        public string Path => Item.InvoicePath;

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

        /// <summary>
        /// Mise à jour de la facture
        /// </summary>
        /// <param name="effectiveOn"></param>
        /// <param name="supplier"></param>
        /// <param name="invoicePath"></param>
        /// <param name="amount"></param>
        public void Update(DateTime effectiveOn, string supplier, string invoicePath, double amount)
        {
            string destinationfile = invoicePath == Item.InvoicePath ? Item.InvoicePath : CopyInvoiceFile(effectiveOn, supplier, invoicePath);

            Item.EffectiveOn = effectiveOn;
            Item.Supplier = supplier;
            Item.Amount = amount;
            Item.InvoicePath = destinationfile;
            Item.DateMaj = DateTime.Now;
            DatabaseAccess.Instance.Update(Item);
        }

        /// <summary>
        /// Suppression de la facture
        /// </summary>
        public void Delete()
        {
            if (File.Exists(Item.InvoicePath)) File.Delete(Item.InvoicePath);
            All.Remove(this);
            DatabaseAccess.Instance.Remove(Item);
        }

        /// <summary>
        /// Crée une copie du fichier de la facture dans le répertoire des factures
        /// </summary>
        /// <param name="effectiveOn"></param>
        /// <param name="supplier"></param>
        /// <param name="invoicePath"></param>
        /// <returns></returns>
        private static string CopyInvoiceFile(DateTime effectiveOn, string supplier, string invoicePath)
        {
            char[] invalid = System.IO.Path.GetInvalidFileNameChars();
            var sanitized = supplier.ToUpperInvariant().Select(c => invalid.Contains(c) ? '_' : c).ToArray();
            var sanitizedString = new string(sanitized);
            var index = 0;
            var extensible = System.IO.Path.GetExtension(invoicePath);
            var file = $"{effectiveOn.ToString("yyyyMMdd")}_INVOICE_{sanitizedString}_{index}{extensible}";
            var destinationfile = System.IO.Path.Combine(Appli.Instance.InvoicePath, file);
            while (File.Exists(destinationfile))
            {
                index++;
                file = $"{effectiveOn.ToString("yyyyMMdd")}_INVOICE_{sanitized}_{index}{extensible}";
                destinationfile = System.IO.Path.Combine(Appli.Instance.InvoicePath, file);
            }
            File.Copy(invoicePath, destinationfile, false);
            return destinationfile;
        }
    }
}
