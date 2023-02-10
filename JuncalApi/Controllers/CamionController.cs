using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace JuncalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CamionController : Controller
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public CamionController(IUnidadDeTrabajo uow,IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CamionRespuesta>>> GetCamiones()
        {
           
           var ListaCamiones = _uow.RepositorioJuncalCamion.GetAll().ToList();

            if (ListaCamiones.Count() > 0)
            {
                List<CamionRespuesta> listaCamionesRespuesta = _mapper.Map<List<CamionRespuesta>>(ListaCamiones);
              return  Ok(listaCamionesRespuesta);

            }
            else return new List<CamionRespuesta>();

        }

        [HttpPost]
        public ActionResult CargarCamion([FromBody] CamionRequerido camionReq) 
        {
            var camion = _uow.RepositorioJuncalCamion.GetAll(c => c.Patente.Equals(camionReq.Patente)).SingleOrDefault();         

            if (camion is null)
            {
                JuncalCamion camionNuevo = _mapper.Map<JuncalCamion>(camionReq);

                _uow.RepositorioJuncalCamion.Insert(camionNuevo);

            } 


            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult IsDeletedCamion(int id)
        {

            var camion = _uow.RepositorioJuncalCamion.GetById(id);
            if (camion != null)
            {
                camion.Isdeleted = true;
                _uow.RepositorioJuncalCamion.Update(camion);
                               
            }
            
            return Ok();


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCamion(int id, CamionRequerido camionEdit)
        { 
            var camion = _uow.RepositorioJuncalCamion.GetById(id);

            if (camion != null)
            {
                camion = _mapper.Map<JuncalCamion>(camionEdit);
                _uow.RepositorioJuncalCamion.Update(camion);
                       
            }

            return Ok();
        

        }
    }
}
