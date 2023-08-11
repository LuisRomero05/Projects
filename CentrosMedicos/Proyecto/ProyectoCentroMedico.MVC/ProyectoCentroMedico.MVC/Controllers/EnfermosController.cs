using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCentroMedico.MVC.Controllers
{
    public class EnfermosController : Controller
    {
        // GET: EnfermosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EnfermosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EnfermosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EnfermosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EnfermosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EnfermosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EnfermosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EnfermosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
