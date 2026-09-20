using Repository.Dbo;
using Repository.Entities;

namespace Business
{
    /// <summary>
    /// Gestion des travaux de la ligne de maintenance
    /// </summary>
    public class Work: Note
    {
        /// <summary>
        /// Créer une ligne de maintenance de travail
        /// </summary>
        public static ILine Create(DateTime effectiveOn, string desc, IEnumerable<string> images, Invoice invoice)
        {
            var item = new LineEntity
            {
                EffectiveOn = effectiveOn,
                Desc = desc,
                UnitPrice = invoice.Amount,
                Quantity = 1,
                ProductName = "Travaux",
                InvoiceId = invoice.Id,
                DateMaj = DateTime.Now,
            };
            DatabaseAccess.Instance.Add(item);

            List<Media> medias = new List<Media>();
            foreach (var image in images)
            {
                medias.Add(Media.Add(image, effectiveOn, item.Id));
            }
            var result = new Work(item);
            Work.All.Add(result);
            return result;
        }

        public Work(LineEntity item):base(item)
        {
        }

        /// <summary>
        /// Mise à jour à la ligne de maintenance par sa référence
        /// </summary>
        public void Update(DateTime effectiveOn, string desc, IEnumerable<string> images, Invoice invoice)
        {
            Item.UnitPrice = invoice.Amount;
            base.Update(effectiveOn, desc, images);
        }
    }
}
