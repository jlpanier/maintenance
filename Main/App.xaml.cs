using Business;
using FFImageLoading.Helpers;
using Repository.Dbo;

namespace Main
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());
            try
            {
                _ = Appli.Instance.InitialiseAsync();
            }
            catch (FileNotFoundException)
            {
                var asset = ServiceHelper.GetService<IAssetService>();
                _ = asset.CopyAssetAsync(BaseDbo.DatabaseName, Appli.Instance.DbPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur d'initialisation de la base de données : " + ex.Message);
                if (!DatabaseAccess.Instance.IsReady())
                {
                    var asset = ServiceHelper.GetService<IAssetService>();
                    _ = asset.CopyAssetAsync(BaseDbo.DatabaseName, Appli.Instance.DbPath);
                }
            }

            return window;
        }
    }
}
