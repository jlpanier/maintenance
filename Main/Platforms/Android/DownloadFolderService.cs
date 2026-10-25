/// <summary>
/// Gestion des paramètres Android
/// </summary>
public partial class DownloadFolderService : IDownloadFolderService
{
    /// <summary>
    /// Obtenir le répertoire de téléchargement
    /// </summary>
    public static string GetDownloadFolder() => Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads)!.AbsolutePath;

}
