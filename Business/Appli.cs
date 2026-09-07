using Repository.Dbo;

namespace Business
{
    /// <summary>
    /// Application
    /// </summary>
    public class Appli
    {
        /// <summary>
        /// Instance de l'application
        /// </summary>
        public static Appli Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance=new Appli();
                }
                return _instance;
            }
        }
        private static Appli? _instance = null;

        /// <summary>
        /// Répertoire interne de l'application
        /// </summary>
        public string AppPath { get; private set; } = "";

        /// <summary>
        /// Répertoire de stockage des cartes
        /// </summary>
        public string MapPath { get; private set; } = "";

        /// <summary>
        /// Chemin des images 
        /// </summary>
        public string ImagePath { get; private set; } = "";

        /// <summary>
        /// Chemin de la base de données
        /// </summary>
        public string DbPath { get; private set; } = "";

        /// <summary>
        /// Chemin complet de la base de données
        /// </summary>
        public string DbFilePath { get; private set; } = "";

        /// <summary>
        /// Chemin de partage des fichiers
        /// </summary>
        public string FilePath { get; private set; } = "";

        /// <summary>
        /// Chemin temporaire des fichiers
        /// </summary>
        public string TmpPath { get; private set; } = "";

        /// <summary>
        /// Chemin des fichiers de factures
        /// </summary>
        public string InvoicePath { get; private set; } = "";

        /// <summary>
        /// Initialisation
        /// </summary>
        public async Task InitialiseAsync()
        {
            AppPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            DbPath = Path.Combine(AppPath, "db");
            ImagePath = Path.Combine(AppPath, "images");
            FilePath = Path.Combine(AppPath, "file");
            TmpPath = Path.Combine(AppPath, "tmp");
            MapPath = Path.Combine(AppPath, "maps");
            InvoicePath = Path.Combine(AppPath, "invoices");
            DbFilePath = Path.Combine(DbPath, BaseDbo.DatabaseName);

            Directory.CreateDirectory(AppPath);
            Directory.CreateDirectory(DbPath);
            Directory.CreateDirectory(FilePath);
            Directory.CreateDirectory(TmpPath);
            Directory.CreateDirectory(ImagePath);
            Directory.CreateDirectory(MapPath);
            Directory.CreateDirectory(InvoicePath);

            DatabaseAccess.Instance.Init(DbFilePath);
        }
    }
}
