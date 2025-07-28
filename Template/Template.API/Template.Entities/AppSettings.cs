using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Entities
{
   public class AppSettings
    {
        public string JwtSecretKey { get; set; }
        public string WebApiUrl { get; set; }
        public string[] AllowedOrigins { get; set; }
    }
}
