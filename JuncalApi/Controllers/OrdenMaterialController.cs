﻿using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenMaterialController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public OrdenMaterialController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenMaterialRespuesta>>> GetOrdenMateriales(int idOrden)
        {

            var ListaOrdenMateriales = _uow.RepositorioJuncalOrdenMarterial.GetAllByCondition(c => c.IdOrden == idOrden && c.Isdeleted == false).ToList();

            if (ListaOrdenMateriales.Count() > 0)
            {
                List<OrdenMaterialRespuesta> listaOrdenMaterialRespuesta = _mapper.Map<List<OrdenMaterialRespuesta>>(ListaOrdenMateriales);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaOrdenMaterialRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<OrdenMaterialRespuesta>() == null });


        }


        [HttpPost]
        public ActionResult CargarOrdenMaterial([FromBody] OrdenMaterialRequerido ordenMaterialReq)
        {
            var ordenMaterial = _uow.RepositorioJuncalOrdenMarterial.GetByCondition(c => c.IdOrden == ordenMaterialReq.IdOrden
            && c.IdMaterial == ordenMaterialReq.IdMaterial
            && c.Isdeleted == false);

            if (ordenMaterial is null)
            {
                JuncalOrdenMarterial ordenMaterialNuevo = _mapper.Map<JuncalOrdenMarterial>(ordenMaterialReq);
                _uow.RepositorioJuncalOrdenMarterial.Insert(ordenMaterialNuevo);
                return Ok(new { success = true, message = "La Orden Material Fue Creado Con Exito", result = ordenMaterialNuevo });
            }

            return Ok(new { success = false, message = " La Orden Material Ya Esta Cargada ", result = ordenMaterial });

        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedOrdenMaterial(int id)
        {

            var ordenMaterial = _uow.RepositorioJuncalOrdenMarterial.GetById(id);
            if (ordenMaterial != null && ordenMaterial.Isdeleted == false)
            {
                ordenMaterial.Isdeleted = true;
                _uow.RepositorioJuncalOrdenMarterial.Update(ordenMaterial);

                return Ok(new { success = true, message = "La Orden Material Fue Eliminada ", result = ordenMaterial.Isdeleted });

            }
            return Ok(new { success = false, message = "La Orden Material  No Fue Encontrada", result = new JuncalOrdenMarterial() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrdenMaterial(int id, OrdenMaterialRequerido ordenMaterialEdits)
        {
            var ordenMaterial = _uow.RepositorioJuncalOrdenMarterial.GetById(id);

            if (ordenMaterial != null && ordenMaterial.Isdeleted == false)
            {
                _mapper.Map(ordenMaterialEdits, ordenMaterial);
                _uow.RepositorioJuncalOrdenMarterial.Update(ordenMaterial);
                return Ok(new { success = true, message = "La Orden Material Fue Actualizada", result = ordenMaterial });
            }

            return Ok(new { success = false, message = "La Orden Material No Fue Encontrada ", result = new JuncalOrdenMarterial() == null });


        }

    }
}
