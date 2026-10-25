using Main.ViewModels;

namespace Main.Pages
{
    /// <summary>
    /// Gestion de la page principale
    /// </summary>
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel) 
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        /// <summary>
        /// customize behavior immediately prior to the page becoming visible.
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is MainViewModel vm)
            {
                vm.Load();
            }
        }
    }
}