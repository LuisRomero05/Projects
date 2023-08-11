using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Services.Utilities
{
    public static class HelpersUtils
    {
        public const string ImageDefault = "/images/users/usuario_2.png";

        #region Dashboard
        public const string Inicio = "Dashboard";
        #endregion

        #region Clientes
        public const string Listado_Cliente = "Listado de clientes";
        public const string Registro_Cliente = "Registro de clientes";
        public const string Actualizar_Cliente = "Actualizar clientes";
        public const string Eliminar_Cliente = "Eliminar clientes";
        public const string Listado_Llamadas_Clientes = "Listado de llamadas de clientes";
        public const string Registro_Llamadas_Clientes = "Registro de llamados de clientes";
        public const string Actualizar_Llamadas_Clientes = "Actualizar llamadas de clientes";
        public const string Eliminar_Llamadas_Clientes = "Eliminar llamadas de clientes";
        public const string Listado_Notas_Clientes = "Listado de notas de clientes";
        public const string Registro_Notas_Clientes = "Registro de notas de clientes";
        public const string Actualizar_Notas_Clientes = "Actualizar de notas de clientes";
        public const string Eliminar_Notas_Clientes = "Eliminar de notas de clientes";
        public const string Listado_Reuniones_Clientes = "Listado de reuniones de clientes";
        public const string Registro_Reuniones_Clientes = "Registro de reuniones de clientes";
        public const string Actualizar_Reuniones_Clientes = "Actualizar reuniones de clientes";
        public const string Eliminar_Reuniones_Clientes = "Eliminar reuniones de clientes";
        public const string Listado_Archivos_Clientes = "Listado de archivos de clientes";
        public const string Registro_Archivos_Clientes = "Registro de archivos de clientes";
        public const string Actualizar_Archivos_Clientes = "Actualizar archivos de clientes";
        public const string Eliminar_Archivos_Clientes = "Eliminar archivos de clientes";
        public const string Descargar_archivos_de_clientes = "Descargar archivos de clientes";
        #endregion

        #region Contactos
        public const string Listado_Contacto = "Listado de contactos";
        public const string Registro_Contacto = "Registro de contactos";
        public const string Actualizar_Contacto = "Actualizar contactos";
        public const string Eliminar_Contacto = "Eliminar contactos";
        #endregion

        #region Personas                        
        public const string Listado_Personas = "Listado de personas";
        public const string Registro_Personas = "Registro de personas";
        public const string Actualizar_Personas = "Actualizar personas";
        public const string Eliminar_Personas = "Eliminar personas";
        #endregion

        #region Empleados                        
        public const string Listado_Empleados = "Listado de empleados";
        public const string Registro_Empleados = "Registro de empleados";
        public const string Actualizar_Empleados = "Actualizar empleados";
        public const string Eliminar_Empleados = "Eliminar empleados";
        #endregion

        #region Puestos                        
        public const string Listado_Puestos = "Listado de puestos";
        public const string Registro_Puestos = "Registro de puestos";
        public const string Actualizar_Puestos = "Actualizar puestos";
        public const string Eliminar_Puestos = "Eliminar puestos";
        #endregion

        #region Areas                       
        public const string Listado_Areas = "Listado de áreas";
        public const string Registro_Areas = "Registro de áreas";
        public const string Actualizar_Areas = "Actualizar áreas";
        public const string Eliminar_Areas = "Eliminar áreas";
        #endregion

        #region Paises                   
        public const string Listado_Paises = "Listado de países";
        public const string Registro_Paises = "Registro de países";
        public const string Actualizar_Paises = "Actualizar países";
        public const string Eliminar_Paises = "Eliminar países";
        #endregion

        #region Departamento                    
        public const string Listado_Departamento = "Listado de departamentos";
        public const string Registro_Departamento = "Registro de departamentos";
        public const string Actualizar_Departamento = "Actualizar departamentos";
        public const string Eliminar_Departamento = "Eliminar departamentos";
        #endregion

        #region Municipios                   
        public const string Listado_Municipios = "Listado de municipios";
        public const string Registro_Municipios = "Registro de municipios";
        public const string Actualizar_Municipios = "Actualizar municipios";
        public const string Eliminar_Municipios = "Eliminar municipios";
        #endregion

        #region Cotizaciones               
        public const string Listado_Cotizaciones = "Listado de cotizaciones";
        public const string Registro_Cotizaciones = "Registro de cotizaciones";
        public const string Actualizar_Cotizaciones = "Actualizar cotizaciones";
        public const string Eliminar_Cotizaciones = "Eliminar cotizaciones";
        #endregion

        #region Ordenes               
        public const string Listado_Ordenes = "Listado de cotizaciones";
        public const string Registro_Ordenes = "Registro de órdenes";
        public const string Actualizar_Ordenes = "Actualizar órdenes";
        public const string Eliminar_Ordenes = "Eliminar órdenes";
        #endregion

        #region Productos               
        public const string Listado_Productos = "Listado de productos";
        public const string Registro_Productos = "Registro de productos";
        public const string Actualizar_Productos = "Actualizar productos";
        public const string Eliminar_Productos = "Eliminar productos";
        #endregion

        #region Categorias               
        public const string Listado_Categorias = "Listado de categorías";
        public const string Registro_Categorias = "Registro de categorías";
        public const string Actualizar_Categorias = "Actualizar categorías";
        public const string Eliminar_Categorias = "Eliminar categorías";
        #endregion

        #region SubCategorias               
        public const string Listado_SubCategorias = "Listado de subcategorías";
        public const string Registro_SubCategorias = "Registro de subcategorías";
        public const string Actualizar_SubCategorias = "Actualizar subcategorías";
        public const string Eliminar_SubCategorias = "Eliminar subcategorías";
        #endregion

        #region Campañas               
        public const string Listado_Campañas = "Listado de campañas";
        public const string Registro_Campañas = "Registro de campañas";
        public const string Eliminar_Campañas = "Eliminar campañas";
        public const string Detalles_Campañas = "Detalles campañas";
        #endregion

        #region Usuarios               
        public const string Listado_Usuarios = "Listado de usuarios";
        public const string Registro_Usuarios = "Registro de usuarios";
        public const string Actualizar_Usuarios = "Actualizar usuarios";
        public const string Eliminar_Usuarios = "Eliminar usuarios";
        public const string Actualizar_Usuarios_Individual = "Perfil Usuario";
        #endregion

        #region Roles               
        public const string Listado_Roles = "Listado de roles";
        public const string Registro_Roles = "Registro de roles";
        public const string Actualizar_Roles = "Actualizar roles";
        public const string Eliminar_Roles = "Eliminar roles";
        #endregion

        #region Reportes
        public const string Reportes_Clientes = "Reportes de clientes";
        public const string Reportes_Cotizaciones = "Reportes de cotizaciones";
        public const string Reportes_Ordenes = "Reportes de órdenes de ventas";
        public const string Reportes_Campañas = "Reportes de campañas";
        #endregion
    }

    public class HelpersGeneral
    {
        public string GetEstadoCivil(string estado)
        {
            string stateorsex;
            switch (estado)
            {
                case "C":
                    stateorsex = "Casado";
                    break;
                case "S":
                    stateorsex = "Soltero";
                    break;
                case "D":
                    stateorsex = "Divorciado";
                    break;
                case "V":
                    stateorsex = "Viudo";
                    break;
                case "U":
                    stateorsex = "Unión libre";
                    break;
                case "F":
                    stateorsex = "Femenino";
                    break;
                case "M":
                    stateorsex = "Masculino";
                    break;
                default:
                    stateorsex = "Prefiere no decirlo";
                    break;
            }
            return stateorsex;
        }
    }

}
