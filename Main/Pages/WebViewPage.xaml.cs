namespace Main.Pages;

public partial class WebViewPage : ContentPage, IQueryAttributable
{
    /// <summary>
    /// Applique les attributs de requête
    /// </summary>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Path", out var objId) && objId is string path)
        {
            PdfViewer.Source = path;
        }
    }

    public WebViewPage()
	{
		InitializeComponent();
	}
}