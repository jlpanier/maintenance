using Repository.Dbo;
using Repository.Entities;

namespace Business
{
    /// <summary>
    /// Gestion des lignes de maintenance
    /// </summary>
    public class Line : ILine
    {
        /// <summary>
        /// Toutes des lignes de maintenance
        /// </summary>
        public static List<ILine> All
        {
            get
            {
                if (_all == null)
                {
                    _all = new List<ILine>();
                    var items = DatabaseAccess.Instance.GetLines();
                    foreach (var item in items)
                    {
                        _all.Add(Line.From(item));
                    }
                }
                return _all;
            }
        }
        private static List<ILine>? _all = null;

        /// <summary>
        /// Conversion de la ligne de maintenance en produit, work ousimple note
        /// </summary>
        private static ILine From(LineEntity item)
        {
            if (!string.IsNullOrEmpty(item.ProductName) && item.ProductName!=Settings.Instance.WorkNameDefault)
            { 
                return new Product(item);
            }
            else if (item.InvoiceId>0)
            {
                return new Work(item);
            }
            return new Note(item);
        }

        /// <summary>
        /// Obtenir la ligne de maintenance par sa référence
        /// </summary>
        public static ILine? GetLine(int id) => All.FirstOrDefault(x => x.Id == id);

        /// <summary>
        /// Créer une ligne de maintenance par sa référence
        /// </summary>
        public static ILine Create(DateTime effectiveOn, string desc, IEnumerable<string> images)
        {
            var item = new LineEntity
            {
                EffectiveOn = effectiveOn,
                Desc = desc,
            };
            DatabaseAccess.Instance.Add(item);

            List<Media> medias = new List<Media>();
            foreach (var image in images)
            {
                medias.Add(Media.Add(image, effectiveOn, item.Id));
            }
            var result = new Line(item);
            result.Medias.AddRange(medias);
            Line.All.Add(result);
            return result;
        }

        /// <summary>
        /// Liste de tous les médias associés à la ligne (images)
        /// </summary>
        public List<Media> Medias
        {
            get
            {
                if (_medias == null)
                {
                    _medias = new List<Media>();
                    var items = Media.All.Where(x => x.LineId == Id);
                    foreach (var item in items)
                    {
                        _medias.Add(item);
                    }
                }
                return _medias;
            }
        }
        protected List<Media>? _medias = null;

        /// <summary>
        /// Référence de ligne de maintenance par sa référence
        /// </summary>
        public int Id => Item.Id;

        /// <summary>
        /// Référence à la facture si existante
        /// </summary>
        public Invoice? Invoice
        {
            get
            {
                if (_invoice==null && InvoiceId>0)
                {
                    _invoice = Invoice.GetById(InvoiceId);
                }
                return _invoice;
            }
        }
        private Invoice? _invoice = null;
             
        /// <summary>
        /// Référence de la facture de la ligne de maintenance par sa référence
        /// </summary>
        public int InvoiceId => Item.InvoiceId;

        /// <summary>
        /// Date de la ligne de maintenance par sa référence
        /// </summary>
        public DateTime EffectiveOn => Item.EffectiveOn;

        /// <summary>
        /// Image affichée par défaut pour la ligne de maintenance par sa référence
        /// </summary>
        public string ImagePath => Medias.FirstOrDefault()?.FileName ?? string.Empty;

        /// <summary>
        /// Description pour la ligne de maintenance par sa référence
        /// </summary>
        public string Desc => Item.Desc;

        /// <summary>
        /// Nom du produit pour la ligne de maintenance par sa référence
        /// </summary>
        public string ProductName => Item.ProductName;

        /// <summary>
        /// Quantité de produit pour la ligne de maintenance par sa référence
        /// </summary>
        public double Quantity => Item.Quantity;

        /// <summary>
        /// Prix unitaire du produit pour la ligne de maintenance par sa référence
        /// </summary>
        public double UnitPrice => Item.UnitPrice;

        /// <summary>
        /// Prix unitaire du produit pour la ligne de maintenance par sa référence
        /// </summary>
        public double TotalAmount => UnitPrice * Quantity;

        /// <summary>
        /// Fournisseur si facture existante
        /// </summary>
        public string Supplier
        {
            get
            {
                var result = string.Empty;
                if (Invoice != null)
                {
                    result = Invoice.Supplier;
                }
                return result;
            }
        }

        /// <summary>
        /// Largeur des images
        /// </summary>
        public int WidthRequest => Settings.Instance.ImageWidthRequest;

        /// <summary>
        /// Hauteur des images
        /// </summary>
        public int HeightRequest => Settings.Instance.ImageHeightRequest;


        /// <summary>
        /// Reference à la ligne de maintenance par sa référence
        /// </summary>
        public readonly LineEntity Item;

        protected Line(LineEntity item)
        {
            Item = item;
        }

        /// <summary>
        /// Mise à jour à la ligne de maintenance par sa référence
        /// </summary>
        public void Update(DateTime effectiveOn, string desc, IEnumerable<string> images)
        {

            foreach (var media in Medias)
            {
                if (!images.Any(_ => _ == media.FileName)) // Le media a été supprimé de la sélection
                {
                    media.Del();
                }
            }
            _medias = null;

            foreach (var image in images)
            {
                var media = Medias.FirstOrDefault(_ => _.FileName == image);
                if (media == null)
                {
                    media = Media.Add(image, effectiveOn, Id);
                    Medias.Add(media);
                }
            }
            Item.Desc = desc;
            Item.EffectiveOn= effectiveOn;
            DatabaseAccess.Instance.Update(Item);
        }

        /// <summary>
        /// Suppression des médias associés à la ligne de maintenance et suppression de la ligne de maintenance
        /// </summary>
        public void Delete()
        {
            foreach (var media in Medias)
            {
                media.Del();
            }
            All.Remove(this);
            DatabaseAccess.Instance.Remove(Item);
        }
    }
}
