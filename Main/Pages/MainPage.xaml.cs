using Android.Widget;
using CommunityToolkit.Maui.Core;
using Main.ViewModels;

namespace Main.Pages
{
    /// <summary>
    /// Gestion de la page principale, avec le plateau de jeu et les boutons de contrôle
    /// </summary>
    public partial class MainPage : ContentPage
    {
        // Liste des URLs interdites
        readonly List<string> _blockedUrls = new()
        {
            "facebook.com",
            "twitter.com",
            "instagram.com",
            "tiktok.com"
        };

        // Liste des URLs autorisées
        readonly List<string> _authorizeUrls = new()
        {
            "google.com",
        };

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
                MainWebView.Source = "https://www.google.com";
                vm.Url = "https://www.google.com";
            }
        }

        private async void MainWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            // Vérifie si l’URL contient un domaine interdit
            if (!_authorizeUrls.Any(b => e.Url.Contains(b, StringComparison.OrdinalIgnoreCase)))
            {
                e.Cancel = true; // bloque la navigation
            }
        }
    }
}