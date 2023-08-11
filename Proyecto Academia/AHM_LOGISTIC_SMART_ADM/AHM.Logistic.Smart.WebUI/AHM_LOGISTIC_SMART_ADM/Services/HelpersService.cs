using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Services
{
    public class HelpersService : HelpersInterface
    {
        public string UpdateImagenPerfil(IFormFile file, int usu_Id, string nombre, string imagenPerfil, string path)
        {
            string oldFile = "";
            string newExtension = "";
            try
            {
                if (file.ContentType != "image/jpeg" &&
                    file.ContentType != "image/jpg" &&
                    file.ContentType != "image/png" &&
                    file.ContentType != "image/gif")
                {
                    return ("Seleccione un archivo valido (.jpg, .png, .gif).");
                }

                string carpeta = $"{path}\\{nombre}";

                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }
                string imagePath = path;
                string imageName = $"{nombre.ToLower().Trim().Replace(" ", "-")}";

                if (!string.IsNullOrEmpty(imagenPerfil))
                {
                    oldFile = $"{carpeta}{imagenPerfil}";
                }

                if (File.Exists(oldFile))
                {
                    File.Delete(oldFile);
                }
                using (var imagenStream = file.OpenReadStream())
                {
                    using (var img = Image.Load(imagenStream))
                    {
                        img.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Mode = ResizeMode.Min,
                            Size = new SixLabors.ImageSharp.Size(width: 500, height: 500)
                        }));
                        newExtension = Path.GetExtension(file.FileName);
                        if (newExtension == ".png")
                        {
                            img.Save($"{carpeta }\\{imageName}{newExtension}", new PngEncoder());
                        }
                        else
                        {
                            img.Save($"{carpeta }\\{imageName}{newExtension}", new JpegEncoder { Quality = 70 });
                            img.Save($"{carpeta }\\{imageName}{newExtension}", new GifEncoder());
                        }

                        imagenPerfil = $"{carpeta}\\{imageName}{newExtension}";
                    }
                }
                return (imagenPerfil);
            }
            catch (Exception)
            {
                return ("Error");
            }
        }
    }
}
