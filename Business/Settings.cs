using Repository.Dbo;
using System.Diagnostics;

namespace Business
{
    /// <summary>
    /// Gestion de la configuration
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Settings();
                }
                return _instance;
            }
        }
        private static Settings? _instance;

        private Settings()
        {
        }

        /// <summary>
        /// Chargement de la configuration
        /// </summary>
        public List<Setting> All
        {
            get
            {
                if (_all == null)
                {
                    _all = new List<Setting>();
                    DatabaseAccess.Instance.GetSettings().ToList().ForEach(_ => _all.Add(Setting.From(_)));
                }
                return _all;
            }
        }
        private List<Setting>? _all;

        /// <summary>
        /// Création d'une nouvelle entité
        /// </summary>
        public Setting Add(string key, string val, string descr)
        {
            var item = All.FirstOrDefault(_=>_.Key==key);
            if (item == null) 
            {
                item = Setting.Create(key, val, descr);
                All.Add(item);
            }
            else
            {
                item.Save(key, val, descr);
            }
            return item;
        }

        /// <summary>
        /// Obtenir la configuration d'une string
        /// </summary>
        private string GetString(string key, string defaultvalue)
        {
            var setting = All.FirstOrDefault(_=>_.Key == key);
            return setting == null ? defaultvalue : setting.Val;
        }

        /// <summary>
        /// Obtenir la configuration d'un entier
        /// </summary>
        private int GetInt(string key, int defaultvalue)
        {
            int result = defaultvalue;
            var setting = All.FirstOrDefault(_ => _.Key == key);
            if (setting != null)
            {
                if (int.TryParse(setting.Val, out int val))
                {
                    result = val;
                }
            }
            return result;
        }

        /// <summary>
        /// Obtenir la configuration d'un double
        /// </summary>
        private double GetDouble(string key, double defaultvalue)
        {
            double result = defaultvalue;
            var setting = All.FirstOrDefault(_ => _.Key == key);
            if (setting != null)
            {
                if (double.TryParse(setting.Val, out double val))
                {
                    result = val;
                }
            }
            return result;
        }

        /// <summary>
        /// Nom par defaut du fournisseur pour les factures
        /// </summary>
        public string InvoiceSupplierDefault => GetString("invoice.supplier.default", "Hunyvers Nautic - AD Nautic");

    }
}
