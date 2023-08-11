using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{

    public class UsersModel
    {
        public int usu_Id { get; set; }
        public string usu_UserName { get; set; }
        public string usu_Password { get; set; }

        [JsonIgnore]
        public bool usu_Status { get; set; }
        [JsonIgnore]
        public string usu_PasswordSalt { get; set; }
        public string usu_Profile_picture { get; set; } 
        public int? rol_Id { get; set; }
        public int per_Id { get; set; }
        public int? usu_IdUserCreate { get; set; }
        public int? usu_IdUserModified { get; set; }

    }

}

