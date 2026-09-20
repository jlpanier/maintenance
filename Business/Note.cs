using Repository.Dbo;
using Repository.Entities;

namespace Business
{
    /// <summary>
    /// Gestion des notes de la ligne de maintenance
    /// </summary>
    public class Note: Line
    {
        public Note(LineEntity item):base(item)
        {
        }
    }
}
