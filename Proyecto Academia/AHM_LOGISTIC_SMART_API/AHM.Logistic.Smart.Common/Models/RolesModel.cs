using AHM.Logistic.Smart.Entities.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Common.Models
{

    public class RolesModel
    {
        public RolesModel()
        {
            ComponentesTree = new List<ComponentsModel>();
            ModulosTree = new List<ModuleModel>();
            ModulosPantallasTree = new List<ModuleItemsModel>();
            ModuleIdList = new List<int>();
        }

        public int? rol_Id { get; set; }
        public string rol_Description { get; set; }
        public bool rol_Status { get; set; }
        public int? rol_IdUserCreate { get; set; }
        public int? rol_IdUserModified { get; set; }

        public IEnumerable<ComponentsModel> ComponentesTree { get; set; }
        public IEnumerable<ModuleModel> ModulosTree { get; set; }
        public IEnumerable<ModuleItemsModel> ModulosPantallasTree { get; set; }

        public string ModuleItemsInput { get; set; }
        public IEnumerable<int> ModuleIdList { get; set; }
        public void LoadList(IEnumerable<int> modulos)
        {
            ModuleIdList = modulos;
        }

        public void LoadTreeViewData(IEnumerable<ComponentsModel> componentes,
                                     IEnumerable<ModuleModel> modulos,
                                     IEnumerable<ModuleItemsModel> moduloPantallas)
        {

            if (componentes == null || !componentes.Any())
            {
                return;
            }
            if (modulos == null || !modulos.Any())
            {
                return;
            }
            if (moduloPantallas == null || !moduloPantallas.Any())
            {

                return;
            }

            ComponentesTree = componentes;
            ModulosTree = modulos;
            ModulosPantallasTree = moduloPantallas;
            ModuleItemsInput = "";
            if (ModuleIdList.Any())
            {
                foreach (var item in ModuleIdList)
                {
                    ModuleItemsInput += $"{item},";
                }
                ModuleItemsInput = ModuleItemsInput.TrimEnd(',');
            }
        }

        public void ParseTreeViewData()
        {
            if (!string.IsNullOrWhiteSpace(ModuleItemsInput))
            {
                ModuleIdList = ModuleItemsInput.Split(',').Where(x => int.TryParse(x, out int mos)).Select(x => int.Parse(x)).ToList();
            }
        }
    }
}
