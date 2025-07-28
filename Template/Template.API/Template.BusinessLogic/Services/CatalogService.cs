using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Template.API.Models;
using Template.DataAccess.Repositories;
using Template.Entities.Entities;

namespace Template.BusinessLogic.Services
{
    public class CatalogService
    {
        private readonly IMapper _mapper;
        private readonly AreasRepository _areasRepository;
        public CatalogService(
                             
                              AreasRepository areasRepository,
                             
                              IMapper mapper)
        {
           
            _areasRepository = areasRepository;
            _mapper = mapper;
        }
        #region tbAreas
        public ServiceResult ListAreas()
        {
            var result = new ServiceResult();
            try
            {
                var listado = _areasRepository.List();
                if (listado.Any())
                    return result.Ok(listado);
                else
                    return result.Error();
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult FindAreas(Expression<Func<View_Areas_List, bool>> expression = null)
        {
            var result = new ServiceResult();
            var resultado = new View_Areas_List();
            var errorMessage = "";
            try
            {
                resultado = _areasRepository.Search(expression);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return result.Ok(resultado);
        }

        public ServiceResult RegisterAreas(tbAreas item)
        {
            var result = new ServiceResult();
            var entidad = new AreasModel();
            //var entidad = new tbAreas();
            try
            {
                var query = _areasRepository.Insert(item);
                if (query > 0)
                {
                    entidad.are_Id = query;
                    return result.Ok(entidad);
                }

                else
                    return result.Error();
            }
            catch (Exception ex)
            {

                return result.Error(ex.Message);
            }
        }


        public ServiceResult UpdateAreas(int Id, tbAreas items)
        {
            var result = new ServiceResult();
            var entidad = new AreasModel();
            //var entidad = new tbAreas();
            try
            {
                var IdUser = Id;
                var usuario = _areasRepository.Find(x => x.are_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _areasRepository.Update(IdUser, items);
                entidad.are_Id = query;
                return result.Ok(entidad);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }

        public ServiceResult DeleteAreas(int Id, int Mod)
        {
            var result = new ServiceResult();
            try
            {
                var IdUser = Id;
                var ModUser = Mod;
                var usuario = _areasRepository.Find(x => x.are_Id == Id);
                if (usuario == null)
                    return result.Error($"No existe el registro");
                var query = _areasRepository.Delete(IdUser, ModUser);
                return result.Ok(query);
            }
            catch (Exception ex)
            {
                return result.Error(ex.Message);
            }
        }
        #endregion


    }
}
