using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{
    public class CustomerFilesModel
    {
        public int cfi_Id { get; set; }
        public string cfi_fileRoute { get; set; }
        public string Nombre { get; set; }
        public int cus_Id { get; set; }
        public int cfi_IdUserCreate { get; set; }
        public DateTime cfi_DateCreate { get; set; }
        public int cfi_IdUserModified { get; set; }
        public IFormFile formFile { get; set; }
        public string href { get; set; }
        public List<CustomerFilesModel> listFile { get; set; }
    }
}
