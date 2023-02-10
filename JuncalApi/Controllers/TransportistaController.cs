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

            var ListaTransportistas = _uow.RepositorioJuncalTransportistum.GetAll().ToList();

            if (ListaTransportistas.Count() > 0)
            {
                List<TransportistaRespuesta> listaTransportistasRespuesta = _mapper.Map<List<TransportistaRespuesta>> (ListaTransportistas);
                return Ok(listaTransportistasRespuesta);

            }
            else return new List<TransportistaRespuesta>();

        }


        [HttpPost]
        public ActionResult CargarTransportista([FromBody] TransportistaRequerido transportistaReq)
        {
            var transportista = _uow.RepositorioJuncalTransportistum.GetAll(c => c.Cuit.Equals(transportistaReq.Cuit)).SingleOrDefault();

            if (transportista is null)
            {
                JuncalTransportistum TransportistaNuevo = _mapper.Map<JuncalTransportistum>(transportistaReq);

                _uow.RepositorioJuncalTransportistum.Insert(TransportistaNuevo);

            }


            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult IsDeletedTransportista(int id)
        {

            var transportista = _uow.RepositorioJuncalTransportistum.GetById(id);
            if (transportista != null)
            {
                transportista.Isdeleted = true;
                _uow.RepositorioJuncalTransportistum.Update(transportista);

            }

            return Ok();


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTransportista(int id, TransportistaRequerido transportistaEdit)
        {
            var transportista = _uow.RepositorioJuncalTransportistum.GetById(id);

            if ( transportista!= null)
            {
                transportista = _mapper.Map<JuncalTransportistum>(transportistaEdit);
                _uow.RepositorioJuncalTransportistum.Update(transportista);

            }

            return Ok();


        }



















    }
}
