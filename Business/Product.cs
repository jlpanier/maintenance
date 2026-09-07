using Repository.Entities;

namespace Business
{
    /// <summary>
    /// Gestion des produits de la ligne de maintenance
    /// </summary>
    public class Product: Work
    {
        public Product(LineEntity item) : base(item)
        {
        }
    }
}
