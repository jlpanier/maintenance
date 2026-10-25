using Repository.Dbo;
using Repository.Entities;

namespace Business
{
    public class MaintenanceObject
    {
        /// <summary>
        /// Affichage par défaut des maintenances
        /// </summary>
        public override string ToString() => Name;

        /// <summary>
        /// Tous les objets de maintenance
        /// </summary>
        public static List<MaintenanceObject> All
        {
            get
            {
                if (_objects == null)
                {
                    _objects = new List<MaintenanceObject>();
                    var items = DatabaseAccess.Instance.GetMaintenanceObjects();
                    foreach (var item in items)
                    {
                        _objects.Add(new MaintenanceObject(item));
                    }
                }
                return _objects;
            }
        }
        private static List<MaintenanceObject>? _objects = null;

        /// <summary>
        /// Recherche de la maintenance
        /// </summary>
        public static MaintenanceObject? GetById(int id) => All.FirstOrDefault(i => i.Id == id);

        /// <summary>
        /// Créer une objet de maintenance de maintenance de travail
        /// </summary>
        public static MaintenanceObject Create(string name)
        {
            var item = new ObjectsEntity
            {
                Name = name,
                DateMaj = DateTime.Now,
            };
            DatabaseAccess.Instance.Add(item);

            var result = new MaintenanceObject(item);
            MaintenanceObject.All.Add(result);
            return result;
        }

        /// <summary>
        /// Sélection de l'objet de maintenance
        /// </summary>
        public static MaintenanceObject? Current
        {
            get
            {
                if (_current==null)
                {
                    _current = All.FirstOrDefault(_=>_.Selected);
                    if (_current==null)
                    {
                        if (All.Any())
                        {
                            _current = All.First();
                            _current.SetSelection();
                        }
                    }
                }
                return _current;
            }
        }
        private static MaintenanceObject? _current;

        public static void Next()
        {
            if (Current!=null)
            {
                var index = All.IndexOf(Current);
                if (index < All.Count - 1)
                {
                    var selection = All[index + 1];
                    selection.SetSelection();
                    _current = selection;
                }
            }
        }

        public static void Previous()
        {
            if (Current != null)
            {
                var index = All.IndexOf(Current);
                if (index > 0)
                {
                    var selection = All[index - 1];
                    selection.SetSelection();
                    _current = selection;
                }
            }
        }

        /// <summary>
        /// Reference de la objet de maintenance
        /// </summary>
        public int Id => Item.Id;

        /// <summary>
        /// Origine de la objet de maintenance
        /// </summary>
        public string Name => Item.Name;

        /// <summary>
        /// Montant de la objet de maintenance
        /// </summary>
        public bool Selected => Item.Selected;


        /// <summary>
        /// Reference de l'entity de la objet de maintenance
        /// </summary>
        public readonly ObjectsEntity Item;

        /// <summary>
        /// constructeur privé pour créer une instance de objet de maintenance à partir d'une entity
        /// </summary>
        private MaintenanceObject(ObjectsEntity item)
        {
            Item = item;
        }

        /// <summary>
        /// Mise à jour de la objet de maintenance
        /// </summary>
        public void Update(string name)
        {
            Item.Name = name;
            Item.Selected = false;
            Item.DateMaj = DateTime.Now;
            DatabaseAccess.Instance.Update(Item);
        }

        /// <summary>
        /// Mise à jour de la objet de maintenance
        /// </summary>
        public void SetSelection()
        {
            All.ForEach(_=> _.Unselect());
            Item.Selected = true;
            Item.DateMaj = DateTime.Now;
            DatabaseAccess.Instance.Update(Item);
        }

        /// <summary>
        /// Mise à jour de la objet de maintenance
        /// </summary>
        private void Unselect()
        {
            Item.Selected = false;
            DatabaseAccess.Instance.Update(Item);
        }

        /// <summary>
        /// Suppression de la objet de maintenance
        /// </summary>
        public void Delete()
        {
            All.Remove(this);
            DatabaseAccess.Instance.Remove(Item);
        }


    }
}
