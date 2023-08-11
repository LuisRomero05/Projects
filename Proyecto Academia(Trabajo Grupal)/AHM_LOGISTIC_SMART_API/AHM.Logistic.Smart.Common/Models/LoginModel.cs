using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{

    public class LoginModel
    {
        public string usu_UserName { get; set; }
        public string usu_Password { get; set; }
    }
}
