using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Configuration
{
    /// <summary>
    /// Type safe appsettings.json class.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// JSON Web Tokens security settings.
        /// </summary>
        public JWTSecuritySettings JWTSecurity { get; set; }

        public SECEHApiSettings ApiSettings { get; set; }

        public DefaultSettings Defaults { get; set; }

        public class JWTSecuritySettings
        {
            /// <summary>
            /// The server key used to sign the JWT token.
            /// </summary>
            public string SigningKey { get; set; }

            /// <summary>
            /// Token expiration time, in minutes.
            /// </summary>
            public int ExpiresMinutes { get; set; }
        }

        public class SECEHApiSettings
        {
            public string BaseUrl { get; set; }
            public string AccessToken { get; set; }
            public int TimeoutSeconds { get; set; }
        }

        public class DefaultSettings
        {
            public string ProfileImageUrl { get; set; }
        }
    }
}
