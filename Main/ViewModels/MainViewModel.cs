using System.Windows.Input;


namespace Main.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// Ajout d'un compte 
        /// </summary>
        public ICommand ClickSettingsCommand => new Command(OnSettings);


        public string Url
        {
            get => _url;
            set
            {
                if (_url == value) return;
                _url = value;
                NotifyPropertyChanged(nameof(Url));
            }
        }
        private string _url = "https://www.microsoft.com";


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

    }
}
