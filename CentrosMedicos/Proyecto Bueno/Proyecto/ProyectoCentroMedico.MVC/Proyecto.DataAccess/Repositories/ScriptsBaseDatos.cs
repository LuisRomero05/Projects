using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.DataAccess.Repositories
{
    public static class ScriptsBaseDatos
    {

        #region tbUsuarios
        //Listado
        public static string UDP_Select_tbUsuarios = "UDP_Select_tbUsuarios";
        //Insert
        public static string UDP_insertar_usuarios = "UDP_insertar_usuarios";
        #endregion

        #region tbRoles
        //listado
        public static string UDP_Select_tbRoles = "UDP_Select_tbRoles";
        #endregion

        #region tbSala
        //listado
        public static string UDP_Select_tbSala = "UDP_Select_tbSala";
        //insertar
        public static string UDP_insertar_Sala = "UDP_insertar_Sala";
        //reporte
        public static string UDP_tbSala_SelectId  = "UDP_tbSala_SelectId";

        #endregion

        #region tbHospiltales
        public static string UDP_Select_tbHospiltales = "UDP_Select_tbHospiltales";
        public static string UDP_insertar_hospitales = "UDP_insertar_hospitales";
        public static string UDP_tbHospital_SelectId = "UDP_tbHospital_SelectId";
        #endregion
       
        #region tbPlantilla
        public static string UDP_insertar_Plantilla = "UDP_insertar_Plantilla";
        public static string UDP_tbPlantilla_SelectId = "UDP_tbPlantilla_SelectId";
        public static string UDP_Select_tbPlantilla = "UDP_Select_tbPlantilla";
        #endregion

        #region tbEnfermo
        public static string UDP_insertar_enfermo = "UDP_insertar_enfermo";
        public static string UDP_Select_tbEnfermo = "UDP_Select_tbEnfermo";
        public static string UDP_tbEnfermo_SelectId = "UDP_tbEnfermo_SelectId";
        #endregion

        #region ReporteGeneral
        public static string UDP_Registro_Paciente = "UDP_Registro_Paciente";
        #endregion

        #region EmpleadoSala
        public static string UDP_Empleados_Cada_Sala = "UDP_Empleados_Cada_Sala";
        #endregion

        #region EmpleadoHospital
        public static string UDP_Empleados_Cada_Hospital = "UDP_Empleados_Cada_Hospital";
        #endregion
    }
}
