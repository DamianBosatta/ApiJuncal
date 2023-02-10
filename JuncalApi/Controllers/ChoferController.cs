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
    public class ChoferController : Controller
    {


        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public ChoferController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChoferRespuesta>>> GetChoferes()
        {

            var ListaChoferes = _uow.RepositorioJuncalChofer.GetAll().ToList();

            if (ListaChoferes.Count() > 0)
            {
                List<ChoferRespuesta> listaChoferesRespuesta = _mapper.Map<List<ChoferRespuesta>>(ListaChoferes);
                return Ok(listaChoferesRespuesta);

            }
            else return new List<ChoferRespuesta>();

        }

        [HttpPost]
        public ActionResult CargarChofer([FromBody] ChoferRequerido choferReq)
        {
            var chofer = _uow.RepositorioJuncalChofer.GetAll(c => c.Dni.Equals(choferReq.Dni)).SingleOrDefault();

            if (chofer is null)
            {
                JuncalChofer choferNuevo = _mapper.Map<JuncalChofer>(choferReq);

                _uow.RepositorioJuncalChofer.Insert(choferNuevo);

            }


            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult IsDeletedChofer(int id)
        {

            var chofer = _uow.RepositorioJuncalChofer.GetById(id);
            if (chofer != null)
            {
                chofer.Isdeleted = true;
                _uow.RepositorioJuncalChofer.Update(chofer);

            }

            return Ok();


        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditChofer(int id, ChoferRequerido choferEdit)
        {
            var chofer = _uow.RepositorioJuncalChofer.GetById(id);

            if (chofer != null)
            {
                chofer = _mapper.Map<JuncalChofer>(choferEdit);
                _uow.RepositorioJuncalChofer.Update(chofer);

            }

            return Ok();


        }

    }
}
