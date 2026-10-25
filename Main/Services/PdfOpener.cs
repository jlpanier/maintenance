using FFImageLoading.Helpers;

namespace Main.Services;

public static class PdfOpener
{
    public static async void OpenPdf(string pdfPath)
    {
#if ANDROID
        if (string.IsNullOrWhiteSpace(pdfPath))
            return;

        var context = Android.App.Application.Context;

        // Fichier Java
        var file = new Java.IO.File(pdfPath);
        file.SetReadable(true);

        // URI via FileProvider
        var uri = AndroidX.Core.Content.FileProvider.GetUriForFile(
            context,
            $"{context.PackageName}.fileprovider",
            file);

        // Intent Android
        var intent = new Android.Content.Intent(Android.Content.Intent.ActionView);
        intent.SetDataAndType(uri, "application/pdf");
        intent.AddFlags(Android.Content.ActivityFlags.GrantReadUriPermission);
        intent.AddFlags(Android.Content.ActivityFlags.NewTask);

        try
        {
            context.StartActivity(intent);
        }
        catch (Exception ex )
        {
            await ServiceHelper.GetService<IAlertService>()!.ShowAlertAsync(ex);
        }
#endif
    }
}
