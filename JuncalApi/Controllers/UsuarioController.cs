using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Servicios;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JuncalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly IServicioUsuario _Servicio;

        public UsuarioController(IUnidadDeTrabajo uow, IMapper mapper,IServicioUsuario servicio)
        {

            _mapper = mapper;
            _uow = uow;
            _Servicio = servicio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolesRespuesta>>> GetUsuarios()
        {

            var ListaUsuarios = _uow.RepositorioJuncalUsuario.GetAllByCondition(u=>u.Isdeleted==false).ToList();

            if (ListaUsuarios.Count() > 0)
            {
                List<UsuarioRespuesta> listaUsuarioRespuesta = _mapper.Map<List<UsuarioRespuesta>>(ListaUsuarios);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaUsuarioRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<UsuarioRespuesta>() == null });


        }


        [HttpPost]
        public ActionResult RegistrarUsuario([FromBody] UsuarioRequerido usuarioReq)
        {
            var usuario = _uow.RepositorioJuncalUsuario.GetByCondition(c => c.Dni.Equals(usuarioReq.Dni));

            if (usuario is null)
            {
                JuncalUsuario usuarioNuevo = _mapper.Map<JuncalUsuario>(usuarioReq);

                _uow.RepositorioJuncalUsuario.Insert(usuarioNuevo);
                return Ok(new { success = true, message = "El Usuario fue Creado Con Exito", result = usuarioNuevo });
            }
           
             return Ok(new { success = false, message = " El Usuario Ya Existe ", result = new JuncalUsuario()==null });

        }

        [HttpPost]
        public ActionResult Login([FromBody] LoginRequerido userReq)
        {
          var Sesion = _Servicio.InicioSesion(userReq);

            if (Sesion is "") 
            { 

             return Ok(new { success = false, message = " El Usuario o Contraseña Fue Invalido ", result = Sesion });

             }

            return Ok(new { success = true, message = "Inicio de Sesion Aceptado", result = Sesion});
          
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditUsuario(int id, UsuarioRequerido usuarioEdit)
        {
            var usuario = _uow.RepositorioJuncalUsuario.GetById(id);

            if (usuario != null)
            { 
            usuario = _mapper.Map<JuncalUsuario>(usuarioEdit);
                _uow.RepositorioJuncalUsuario.Update(usuario);
                
            return Ok(new { success = true, message = "El Usuario fue actualizado", result = usuario });
            }

            return Ok(new { success = false, message = "El Usuario no se Encontro", result = new JuncalUsuario()==null });


        }

        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedUsuario(int id)
        {

            var usuario = _uow.RepositorioJuncalUsuario.GetById(id);
            if (usuario != null && usuario.Isdeleted == false)
            {
                usuario.Isdeleted = true;
                _uow.RepositorioJuncalUsuario.Update(usuario);

                return Ok(new { success = true, message = "El usuario Fue Eliminado ", result = usuario.Isdeleted });


            }


            return Ok(new { success = false, message = "El Usuario no fue encontrado", result = new JuncalUsuario() == null });

        }







    }
}

