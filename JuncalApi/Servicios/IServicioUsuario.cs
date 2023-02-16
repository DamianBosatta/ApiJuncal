using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;

namespace JuncalApi.Servicios
{
    public interface IServicioUsuario
    {

        public dynamic InicioSesion (LoginRequerido userReq);
        public JuncalUsuario RegistroUsuario(UsuarioRequerido userReq);   

    }
}
