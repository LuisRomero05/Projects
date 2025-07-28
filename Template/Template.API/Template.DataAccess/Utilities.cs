using System;
using System.Collections.Generic;
using System.Text;

namespace Template.DataAccess
{
   public class Utilities
    {
        public static string GetMessage(Exception Ex)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Ex.Message);
            while (Ex.InnerException != null)
            {
                stringBuilder.Append(Ex.InnerException);
                Ex = Ex.InnerException;
            }

            return stringBuilder.ToString();
        }

        public enum Status
        {
            Entregado = 1,
            EnCurso = 2,
            Ejecutandose = 3,
            Finalizado = 4
        }

    }
}
