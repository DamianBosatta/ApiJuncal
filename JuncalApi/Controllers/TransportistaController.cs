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
    public class TransportistaController : Controller
    {


        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public TransportistaController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportistaRespuesta>>> GetTransportistas()
        {

            var ListaTransportistas = _uow.RepositorioJuncalTransportistum.GetAll().Where(c=>c.Isdeleted==false).ToList();

            if (ListaTransportistas.Count() > 0)
            {
                List<TransportistaRespuesta> listaTransportistasRespuesta = _mapper.Map<List<TransportistaRespuesta>> (ListaTransportistas);
                return Ok(new { success = true, message = " La Lista Esta Lista Para Ser Utilizada", result = ListaTransportistas });

            }
            return Ok(new { success = false, message = " La Lista Esta Vacia", result = new List<TransportistaRespuesta>()== null }); ;

        }


        [HttpPost]
        public ActionResult CargarTransportista([FromBody] TransportistaRequerido transportistaReq)
        {
            var transportista = _uow.RepositorioJuncalTransportistum.GetAll(c => c.Cuit.Equals(transportistaReq.Cuit)).SingleOrDefault();

            if (transportista is null)
            {
                JuncalTransportistum TransportistaNuevo = _mapper.Map<JuncalTransportistum>(transportistaReq);

                _uow.RepositorioJuncalTransportistum.Insert(TransportistaNuevo);
                return Ok(new { success = true, message = " El Transportista Fue Creado Con Exito ", result = TransportistaNuevo });
            }
           else if (transportista.Isdeleted == true) return Ok(new { success = false, message = " El Transportista Ya Existe, Pero Esta Eliminado ", result = transportista });
           else return Ok(new { success = false, message = " El Transportista Ya Existe ", result = transportista });

        }

        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedTransportista(int id)
        {

            var transportista = _uow.RepositorioJuncalTransportistum.GetById(id);
            if (transportista != null && transportista.Isdeleted==false)
            {
                transportista.Isdeleted = true;
                _uow.RepositorioJuncalTransportistum.Update(transportista);
                return Ok(new { success = true, message = " El Transportista Fue Eliminado Con Exito ", result = transportista.Isdeleted});
            }

            return Ok(new { success = false, message = " El Transportista No Se Encontro ", result = new JuncalTransportistum()== null });


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTransportista(int id, TransportistaRequerido transportistaEdit)
        {
            var transportista = _uow.RepositorioJuncalTransportistum.GetById(id);

            if ( transportista!= null && transportista.Isdeleted == false)
            {
                transportista = _mapper.Map(transportistaEdit,transportista);
                _uow.RepositorioJuncalTransportistum.Update(transportista);
                return Ok(new { success = true, message = " El Transportista Fue Actualizado Con Exito ", result = transportista });
            }

            return Ok(new { success = false, message = " El Transportista No Se Encontro ", result = new JuncalTransportistum() == null });


        }



















    }
}
