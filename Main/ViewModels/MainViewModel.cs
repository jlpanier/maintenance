using Business;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace Main.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// Ajout d'un compte 
        /// </summary>
        public ICommand ClickSettingsCommand => new Command(OnSettings);

        /// <summary>
        /// Ajout d'un compte 
        /// </summary>
        public ICommand ClickLineCommand => new Command<Note>(OnNote);
        

        public ICommand ClicMenu => new Command(OnMenu);

        public ICommand ClicProduct => new Command(OnProduct);

        public ICommand ClicWork => new Command(OnWork);

        public ICommand ClicNote => new Command(OnNewNote);


        public bool MenuVisible
        {
            get => _menuVisible;
            set
            {
                _menuVisible = value;
                NotifyPropertyChanged(nameof(MenuVisible));
            }
        }
        private bool _menuVisible;

        /// <summary>
        /// Lignes
        /// </summary>
        public ObservableCollection<Business.ILine> Lines
        {
            get => _lines;
            set
            {
                if (_lines != value)
                {
                    _lines = value;
                    NotifyPropertyChanged(nameof(Lines));
                }
            }
        }
        public ObservableCollection<Business.ILine> _lines = new ObservableCollection<Business.ILine>();

        public MainViewModel()
        {
        }

        /// <summary>
        /// Ajout d'un compte    
        /// </summary>
        private async void OnSettings()
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }

        private async void OnMenu()
        {
            MenuVisible = !MenuVisible;
        }

        private async void OnProduct()
        {
            MenuVisible = false;
        }

        private async void OnWork()
        {
            MenuVisible = false;
        }

        private async void OnNewNote()
        {
            MenuVisible = false;
            await Shell.Current.GoToAsync($"{nameof(EditNotePage)}", new Dictionary<string, object>
            {
                ["Id"] = 0,
            });
        }

        private async void OnNote(Note item)
        {
            MenuVisible = false;
            await Shell.Current.GoToAsync($"{nameof(EditNotePage)}", new Dictionary<string, object>
            {
                ["Id"] = item.Id,
            });
        }

        public void Load()
        {
            Lines = new ObservableCollection<Business.ILine>(Business.Line.All);
        }
    }
}
