using Repository.Entities;

namespace Business
{
    /// <summary>
    /// Gestion des travaux de la ligne de maintenance
    /// </summary>
    public class Work: Note
    {
        public Work(LineEntity item):base(item)
        {
        }
    }
}
