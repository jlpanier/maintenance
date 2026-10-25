using Microsoft.Maui.Controls.PlatformConfiguration;
using System.IO;

namespace Main.Services;

public static class ImageRotationService
{
    /// <summary>
    /// Rotation native Android + correction EXIF + sauvegarde.
    /// </summary>
    public static string RotateImage(string imagePath, int angleDegrees)
    {
#if ANDROID
        if (!File.Exists(imagePath))
            throw new FileNotFoundException("Image introuvable", imagePath);

        // Charger l'image (Android gère HEIC nativement)
        using (var bitmap = Android.Graphics.BitmapFactory.DecodeFile(imagePath))
        {
            if (bitmap !=null)
            {
                int exifRotation = GetExifRotation(imagePath);
                var rotatedBitmap = exifRotation != 0
                    ? ApplyRotation(ApplyRotation(bitmap, exifRotation), angleDegrees)
                    : ApplyRotation(bitmap, angleDegrees);


                // Nouveau nom
                var dir = Path.GetDirectoryName(imagePath);
                var name = Path.GetFileNameWithoutExtension(imagePath);
                var ext = Path.GetExtension(imagePath).ToLower();
                var outdir = DownloadFolderService.GetDownloadFolder();

                // HEIC → JPEG (Android ne peut pas ré‑encoder en HEIC)
                var outputExt = ext == ".heic" ? ".jpg" : ext;
                var outputPath = Path.Combine(outdir, $"{name}_rotated_tmp{outputExt}");

                // Sauvegarde
                using (var stream = File.OpenWrite(outputPath))
                {
                    rotatedBitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 100, stream);
                }

                rotatedBitmap.Dispose();

                var finalPath = outputPath.Replace("_rotated_tmp", "");
                File.Delete(finalPath);
                File.Move(outputPath, finalPath);
                return finalPath;

            }
            throw new PlatformNotSupportedException("Rotation native uniquement disponible sur Android.");
        }

#else
        throw new PlatformNotSupportedException("Rotation native uniquement disponible sur Android.");
#endif
    }

    /// <summary>
    /// Lit l’orientation EXIF (photos Android / iPhone).
    /// </summary>
    private static int GetExifRotation(string imagePath)
    {
#if ANDROID
        var exif = new Android.Media.ExifInterface(imagePath);
        var orientation = exif.GetAttributeInt(Android.Media.ExifInterface.TagOrientation, (int)Android.Media.Orientation.Normal);

        return orientation switch
        {
            (int)Android.Media.Orientation.Rotate90 => 90,
            (int)Android.Media.Orientation.Rotate180 => 180,
            (int)Android.Media.Orientation.Rotate270 => 270,
            _ => 0
        };
#else
        throw new PlatformNotSupportedException("Rotation native uniquement disponible sur Android.");
#endif
    }

#if ANDROID

    /// <summary>
    /// Applique une rotation native Android via Matrix.
    /// </summary>
    private static Android.Graphics.Bitmap ApplyRotation(Android.Graphics.Bitmap bitmap, int angle)
    {
        var matrix = new Android.Graphics.Matrix();
        matrix.PostRotate(angle);

        Android.Graphics.Bitmap rotated = Android.Graphics.Bitmap.CreateBitmap(
            bitmap,
            0, 0,
            bitmap.Width,
            bitmap.Height,
            matrix,
            true);

        bitmap.Dispose();
        return rotated;
    }
#endif
}