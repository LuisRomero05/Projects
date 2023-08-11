using AHM.Logistic.Smart.Common.Models;
using AHM_Libreria.WebUI.Attribute;
using AHM_LOGISTIC_SMART_ADM.Services;
using AHM_LOGISTIC_SMART_ADM.Services.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AHM_LOGISTIC_SMART_ADM.Controllers
{
    public class CustomersFileController : Controller
    {
        private readonly CustomersService _customersService;
        public CustomersFileController(CustomersService customersService)
        {
            _customersService = customersService;
        }

        [SessionManager(HelpersUtils.Listado_Archivos_Clientes)]
        public async Task<IActionResult> Index()
        {
            var list = await _customersService.CustomerFileList();
            return View(list.Data);
        }

        [HttpGet]
        [SessionManager(HelpersUtils.Registro_Archivos_Clientes)]
        public IActionResult Create()
        {
            var model = new CustomerFilesModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerFilesModel model)
        {
            model.cfi_IdUserCreate = HttpContext.Session.GetInt32("usu_Id") ?? 1;
            int id = model.cus_Id;
            try
            {
                string directorio = "wwwroot/Attachments";
                if (model.formFile != null)
                {
                    string newFileName = $"{id}-{model.formFile.FileName}";

                    if (!Directory.Exists(directorio))
                    {
                        Directory.CreateDirectory(directorio);
                    }

                    var filePath = $"{directorio}/{newFileName}";
                    if (!System.IO.File.Exists(filePath))
                    {
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            model.formFile.CopyTo(stream);
                        }
                        model.formFile = null;
                        model.cfi_fileRoute = filePath.ToString();

                        var resultado = await _customersService.InsertCustomerFile(model);
                        if (resultado.Success) TempData["CustomerFile"] = "Se agrego un nuevo archivo";
                        else TempData["CustomerFile"] = $"{resultado.Message}";
                    }
                    else
                    {
                        string directorioTemp = "wwwroot/AttachmentsTemp";
                        string newFileNameTemp = $"{id}-{model.formFile.FileName}";
                        if (!Directory.Exists(directorioTemp))
                        {
                            Directory.CreateDirectory(directorioTemp);
                        }
                        var filePathTemp = $"{directorioTemp}/{newFileNameTemp}";
                        using (var streamTemp = System.IO.File.Create(filePathTemp))
                        {
                            model.formFile.CopyTo(streamTemp);
                        }
                        TempData["CustomerFile"] = "Replace";
                        TempData["IdCusReplace"] = model.cus_Id;
                        TempData["filePath"] = filePath.ToString();
                        TempData["filePathTemp"] = filePathTemp.ToString();
                    }
                }
                else TempData["CustomerFile"] = "Se produjo un error, seleccione un archivo valido";

                return RedirectToAction("Edit", "Customers", new { id });
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public IActionResult DeleteRepleace(CustomerFilesModel model)
        {
            string filePath = model.cfi_fileRoute;
            string filePathTemp = model.href;
            ServiceResult result = new ServiceResult();
            System.IO.File.Move(filePathTemp, filePath, true);
            TempData["CustomerFile"] = "0";
            return Json(result);
        }

        [HttpDelete]
        public IActionResult DeleteTemp(string filePathTemp)
        {
            try
            {
                ServiceResult result = new ServiceResult();
                if (filePathTemp != "")
                {
                    FileInfo fInfo = new(filePathTemp);
                    if (fInfo.Exists)
                    {
                        fInfo.Delete();
                    }
                }
                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [SessionManager(HelpersUtils.Eliminar_Archivos_Clientes)]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var documentFind = await _customersService.CustomerFileDetails(Id);

                var data = (CustomerFilesModel)documentFind.Data;
                ServiceResult result = new ServiceResult();
                result = result.Ok();
                if (documentFind.Data != null)
                {

                    if (data.cus_Id == Convert.ToInt32(TempData.Peek("Cus_Id")))
                    {
                        string filePath = data.cfi_fileRoute;
                        if (filePath != null)
                        {
                            FileInfo fInfo = new(filePath);
                            if (fInfo.Exists)
                            {
                                fInfo.Delete();
                            }
                        }
                        int Mod = HttpContext.Session.GetInt32("usu_Id") ?? 1;
                        result = await _customersService.DeleteCustomerFile(Id, Mod);
                        TempData["CustomerFile"] = "Eliminado";
                        return Json(result);
                    }
                }
                TempData["CustomerFile"] = "1";
                result.Message = "El archivo que desea eliminar no existe";
                return Json(result);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public IActionResult Download(IFormCollection collection, string Id)
        {
            try
            {
                string filePath = collection[Id];
                filePath = $"wwwroot/Attachments/{filePath}";
                FileInfo fInfo = new(filePath);
                string[] extencion = fInfo.Extension.Split(".");
                string ext = $"application/{extencion[1]}";
                return File(fInfo.FullName, ext, fInfo.Name);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}