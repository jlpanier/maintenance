using System.Text.RegularExpressions;

namespace Business
{
    /// <summary>
    /// Gestion des médias associés
    /// </summary>
    public class Media
    {
        /// <summary>
        /// Liste de tous les médias associés
        /// </summary>
        public static List<Media> All
        {
            get
            {
                if (_all == null)
                {
                    _all = new List<Media>();
                    if (!string.IsNullOrEmpty(Appli.Instance.FilePath) && Directory.Exists(Appli.Instance.FilePath))
                    {
                        var images = Directory.GetFiles(Appli.Instance.FilePath);
                        foreach (var image in images)
                        {
                            var media = Media.Parse(image);
                            if (media != null)
                            {
                                _all.Add(media);
                            }
                        }
                    }
                }
                return _all;
            }
        }
        private static List<Media>? _all = null;

        /// <summary>
        /// Conversion du fichier en média
        /// </summary>
        private static Media? Parse(string fullpath)
        {
            Media? result = null;
            var regex = new Regex(@"^(?<date>\d{8})_LINE_(?<lineid>\d+)_(?<index>\d+)\.(png|jpe?g|gif|bmp|tiff?|webp)$", RegexOptions.IgnoreCase);

            var match = regex.Match(Path.GetFileName(fullpath));
            if (match.Success)
            {
                // Extraction
                var datestr = match.Groups["date"].Value;
                var lineidstr = match.Groups["lineid"].Value;
                var indexstr = match.Groups["index"].Value;

                // Conversion
                if (DateTime.TryParseExact(datestr, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime dateon) && int.TryParse(lineidstr, out int lineId) && int.TryParse(indexstr, out int index))
                {
                    result = new Media(fullpath, dateon, lineId, index);
                }
            }
            return result;
        }

        /// <summary>
        /// Ajout d'un média
        /// </summary>
        public static Media Add(string sourcefile, DateTime effectiveOn, int lineid)
        {
            var index = 0;
            var extensible = Path.GetExtension(sourcefile);
            var file = $"{effectiveOn.ToString("yyyyMMdd")}_LINE_{lineid}_{index}{extensible}";
            var destinationfile = Path.Combine(Appli.Instance.FilePath, file);

            while (File.Exists(destinationfile))
            {
                index++;
                file = $"{effectiveOn.ToString("yyyyMMdd")}_LINE_{lineid}_{index}{extensible}";
                destinationfile = Path.Combine(Appli.Instance.FilePath, file);
            }
            File.Copy(sourcefile, destinationfile, false);
            var result = new Media(destinationfile, effectiveOn, lineid, index);
            All.Add(result);
            return result;
        }

        /// <summary>
        /// Suppression d'un média
        /// </summary>
        public bool Del()
        {
            var result = false; 
            var media = All.FirstOrDefault(x => x.FileName == FileName);
            if (media != null)
            {
                if (File.Exists(media.FileName))
                {
                    File.Delete(media.FileName);
                    result = true;
                }
                All.Remove(media);
            }
            return result;   
        }

        /// <summary>
        /// Nom du fichier média
        /// </summary>
        public readonly string FileName;

        /// <summary>
        /// Date du média
        /// </summary>
        public readonly DateTime EffectiveOn;

        /// <summary>
        /// Référence de la ligne associée au média
        /// </summary>
        public readonly int LineId;

        /// <summary>
        /// Index du média pour la ligne associée (permet de gérer plusieurs médias pour une même ligne)
        /// </summary>
        public readonly int Index;

        /// <summary>
        /// Conversion
        /// </summary>
        private Media(string filename, DateTime effectiveOn, int lineId, int index)
        {
            FileName = filename;
            EffectiveOn = effectiveOn;
            LineId = lineId;
            Index = index;
        }
    }
}
