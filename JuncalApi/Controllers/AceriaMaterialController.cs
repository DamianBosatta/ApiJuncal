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
    public class AceriaMaterialController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public AceriaMaterialController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AceriaMaterialRespuesta>>> GetAceriasMaterial()
        {

            var ListaAceriasMaterial = _uow.RepositorioJuncalAceriaMaterial.GetAll().Where(c => c.Isdeleted == false).ToList();

            if (ListaAceriasMaterial.Count() > 0)
            {
                List<AceriaMaterialRespuesta> listaAceriasMatRespuesta = _mapper.Map<List<AceriaMaterialRespuesta>>(ListaAceriasMaterial);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaAceriasMatRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<AceriaMaterialRespuesta>() == null });


        }

        [HttpPost]
        public ActionResult CargarAceriaMaterial([FromBody] AceriaMaterialRequerido aceriaMatRequerido)
        {
            var aceriaMat = _uow.RepositorioJuncalAceriaMaterial.GetAll(c => c.Cod.Equals(aceriaMatRequerido.Cod)).SingleOrDefault();

            if (aceriaMat is null)
            {
                JuncalAceriaMaterial aceriaMatNuevo = _mapper.Map<JuncalAceriaMaterial>(aceriaMatRequerido);

                _uow.RepositorioJuncalAceriaMaterial.Insert(aceriaMatNuevo);
                return Ok(new { success = true, message = "La Aceria Material fue Creada Con Exito", result = aceriaMatNuevo });
            }
            else if (aceriaMat.Isdeleted == true) return Ok(new { success = false, message = " La Aceria Ya Existe , Pero Esta Eliminada ", result = aceriaMat });
            else return Ok(new { success = false, message = " La Aceria Ya Existe ", result = aceriaMat });

        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedAceriaMaterial(int id)
        {

            var aceriaMat = _uow.RepositorioJuncalAceriaMaterial.GetById(id);
            if (aceriaMat != null && aceriaMat.Isdeleted == false)
            {
                aceriaMat.Isdeleted = true;
                _uow.RepositorioJuncalAceriaMaterial.Update(aceriaMat);

                return Ok(new { success = true, message = "La Aceria Material Fue Eliminada ", result = aceriaMat.Isdeleted });


            }


            return Ok(new { success = false, message = "La Aceria Material no fue encontrada", result = new JuncalAceriaMaterial() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAceriaMaterial(int id, AceriaMaterialRequerido aceriaMatEdit)
        {
            var aceriaMat = _uow.RepositorioJuncalAceriaMaterial.GetById(id);

            if (aceriaMat != null && aceriaMat.Isdeleted == false)
            {
                aceriaMat = _mapper.Map(aceriaMatEdit,aceriaMat);
                _uow.RepositorioJuncalAceriaMaterial.Update(aceriaMat);
                return Ok(new { success = true, message = "La Aceria Material fue actualizada", result = aceriaMat });
            }

            return Ok(new { success = false, message = "La Aceria Material no fue encontrada ", result = new JuncalAceriaMaterial() == null });


        }
    }
}