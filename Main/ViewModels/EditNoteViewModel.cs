using FFImageLoading.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Main.ViewModels
{
    /// <summary>
    /// Gestion de l'édition d'une note
    /// </summary>
    public class EditNoteViewModel:BaseViewModel
    {
        /// <summary>
        /// Sauvegarde des données
        /// </summary>
        public ICommand ClickSaveCommand => new Command(OnSave);

        /// <summary>
        /// Ajout d'une image
        /// </summary>
        public ICommand ClickAddImageCommand => new Command(OnAdd);

        /// <summary>
        /// Suppression d'une image
        /// </summary>
        public ICommand ClickRemoveImageCommand => new Command<string>(OnRemove);

        /// <summary>
        /// Date d'effet de la note
        /// </summary>
        public DateTime EffectiveOn
        {
            get { return _effectiveOn; }
            set
            {
                if (_effectiveOn != value)
                {
                    _effectiveOn = value;
                    NotifyPropertyChanged(nameof(EffectiveOn));
                }
            }
        }
        private DateTime _effectiveOn = DateTime.Today;

        /// <summary>
        /// Description de la note
        /// </summary>
        public string Desc
        {
            get => _desc;
            set
            {
                if (_desc != value)
                {
                    _desc = value;
                    NotifyPropertyChanged(nameof(Desc));
                }
            }
        }
        private string _desc = "";

        /// <summary>
        /// Ensemble des images associées de la note
        /// </summary>
        public ObservableCollection<string> Images
        {
            get => _images;
            set
            {
                _images = value;
                NotifyPropertyChanged(nameof(Images));
            }
        }
        private ObservableCollection<string> _images = new ObservableCollection<string>();

        /// <summary>
        /// Référence de la note
        /// </summary>
        private int _key;

        /// <summary>
        /// Initialisation de la page note
        /// </summary>
        public void Init(int key)
        {
            _key=key;
            var item = Business.Line.GetLine(_key);
            if (item == null)
            {
                EffectiveOn = DateTime.Now.Date;
                Desc = string.Empty;
            }
            else if (item is Business.Note note)
            {
                EffectiveOn = note.EffectiveOn;
                Desc = note.Desc;
                Images = new ObservableCollection<string>(note.Medias.Select(_=>_.FileName));
            }

        }

        /// <summary>
        /// Sauvegarde des données
        /// </summary>
        public async void OnSave()
        {
            try
            {
                var item = Business.Line.GetLine(_key);
                if (item==null)
                {
                    Business.Note.Create(EffectiveOn, Desc, Images);
                }
                else if (item is Business.Note note)
                {
                    note.Update(EffectiveOn, Desc, Images);
                }

                await Shell.Current.GoToAsync(".."); // Retour à la page précédente
            }
            catch (Exception ex)
            {
                await ServiceHelper.GetService<IAlertService>()!.ShowAlertAsync(ex);
            }
        }

        /// <summary>
        /// Ajout d'une image à la note
        /// </summary>
        public async void OnAdd()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Choisir une image",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    var items = Images == null || !Images.Any() ? new List<string>() : new List<string>(Images);
                    items.Add(result.FullPath);
                    Images = new ObservableCollection<string>(items);
                }

            }
            catch (Exception ex)
            {
                await ServiceHelper.GetService<IAlertService>()!.ShowAlertAsync(ex);
            }
        }

        /// <summary>
        /// Suppression d'une image à la note
        /// </summary>
        public async void OnRemove(string filename)
        {
            try
            {
                if (Images.Any())
                {
                    var item = Images.First(_ => _ == filename);
                    Images.Remove(item);
                    NotifyPropertyChanged(nameof(Images));
                }
            }
            catch (Exception ex)
            {
                await ServiceHelper.GetService<IAlertService>()!.ShowAlertAsync(ex);
            }
        }
    }
}
