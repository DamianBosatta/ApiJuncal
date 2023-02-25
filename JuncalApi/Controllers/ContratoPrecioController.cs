using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoPrecioController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public ContratoPrecioController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContratoPrecioRespuesta>>> GetAContratosPrecio()
        {

            var ListaContratosPrecio = _uow.RepositorioJuncalContratoPrecio.GetAllByCondition(c => c.Isdeleted == false);

            if (ListaContratosPrecio.Count() > 0)
            {
                List<ContratoPrecioRespuesta> listaContratoPrecioRespuesta = _mapper.Map<List<ContratoPrecioRespuesta>>(ListaContratosPrecio);
                return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaContratoPrecioRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<ContratoPrecioRespuesta>() == null });


        }


        [HttpGet("{idContratoItem}")]
        public async Task<ActionResult<IEnumerable<ContratoPrecioRespuesta>>> GetAContratosPrecioForItem(int idContratoItem)
        {

            var ListaContratosPrecio = _uow.RepositorioJuncalContratoPrecio.GetAllByCondition(c => c.IdItem==idContratoItem && c.Isdeleted == false);

            if (ListaContratosPrecio.Count() > 0)
            {
                List<ContratoPrecioRespuesta> listaContratoPrecioRespuesta = _mapper.Map<List<ContratoPrecioRespuesta>>(ListaContratosPrecio);
                return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaContratoPrecioRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<ContratoPrecioRespuesta>() == null });


        }


        [HttpPost]
        public ActionResult CargarContratoPrecio([FromBody] List<ContratoPrecioRequerido> listContratoPrecioRequerido)
        {
            JuncalContratoPrecio contratoPrecioNuevo = new();

            foreach(var item in listContratoPrecioRequerido)
            {
                contratoPrecioNuevo = _mapper.Map<JuncalContratoPrecio>(item);
                _uow.RepositorioJuncalContratoPrecio.Insert(contratoPrecioNuevo);

            }
            if(listContratoPrecioRequerido.Count() > 0)
            {
                return Ok(new { success = true, message = "La Lista De Contrato Precio Fue Creado Con Exito ", result = Ok() });

            }


            return Ok(new { success = false, message = " Ocurrio Un Error En La Carga ", result = new List <ContratoPrecioRespuesta>()==null });

        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedContratoPrecio(int id)
        {

            var contratoPrecio = _uow.RepositorioJuncalContratoPrecio.GetById(id);
            if (contratoPrecio != null && contratoPrecio.Isdeleted == false)
            {
                contratoPrecio.Isdeleted = true;
                _uow.RepositorioJuncalContratoPrecio.Update(contratoPrecio);

                return Ok(new { success = true, message = "El Contrato Precio Fue Eliminado ", result = contratoPrecio.Isdeleted });

            }

            return Ok(new { success = false, message = "El Contrato No Se Encontro ", result = new JuncalContratoPrecio() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditContratoPrecio(int id, ContratoPrecioRequerido contratoPrecioEdit)
        {
            var contratoPrecio = _uow.RepositorioJuncalContratoPrecio.GetById(id);

            if (contratoPrecio != null && contratoPrecio.Isdeleted == false)
            {
                _mapper.Map(contratoPrecioEdit, contratoPrecio);
                _uow.RepositorioJuncalContratoPrecio.Update(contratoPrecio);
                return Ok(new { success = true, message = "El Contrato Precio  fue Actualizado ", result = contratoPrecio });
            }

            return Ok(new { success = false, message = "El Contrato Precio No Fue Encontrado ", result = new JuncalContratoPrecio() == null });


        }







    }
}
