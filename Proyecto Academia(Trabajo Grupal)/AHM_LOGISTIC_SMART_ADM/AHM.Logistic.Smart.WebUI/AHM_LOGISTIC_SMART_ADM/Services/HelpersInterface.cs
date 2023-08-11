using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Services
{
    public interface HelpersInterface
    {
        public string UpdateImagenPerfil(IFormFile file, int usu_Id, string nombre, string imagenPerfil, string path);
    }
}
