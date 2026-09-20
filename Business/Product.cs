using Repository.Dbo;
using Repository.Entities;

namespace Business
{
    /// <summary>
    /// Gestion des produits de la ligne de maintenance
    /// </summary>
    public class Product: Work
    {
        /// <summary>
        /// Créer une ligne de maintenance de travail
        /// </summary>
        public static ILine Create(DateTime effectiveOn, string desc, IEnumerable<string> images, Invoice invoice, string productname, double quantity, double unitPrice)
        {
            var item = new LineEntity
            {
                EffectiveOn = effectiveOn,
                Desc = desc,
                UnitPrice = unitPrice,
                Quantity = quantity,
                ProductName = productname,
                InvoiceId = invoice.Id,
                DateMaj = DateTime.Now,
            };
            DatabaseAccess.Instance.Add(item);

            List<Media> medias = new List<Media>();
            foreach (var image in images)
            {
                medias.Add(Media.Add(image, effectiveOn, item.Id));
            }
            var result = new Product(item);
            Product.All.Add(result);
            return result;
        }

        public Product(LineEntity item) : base(item)
        {
        }

        /// <summary>
        /// Mise à jour à la ligne de maintenance par sa référence
        /// </summary>
        public void Update(DateTime effectiveOn, string desc, IEnumerable<string> images, Invoice invoice, string productname, double quantity, double unitPrice)
        {
            Item.ProductName = productname;
            Item.Quantity = quantity;
            Item.UnitPrice = unitPrice;
            base.Update(effectiveOn, desc, images, invoice, unitPrice);
        }
    }
}
