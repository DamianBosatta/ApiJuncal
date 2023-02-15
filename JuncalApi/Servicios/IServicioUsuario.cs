using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;

namespace JuncalApi.Servicios
{
    public interface IServicioUsuario
    {

        public dynamic InicioSesion (LoginRequerido userReq);
        public int RegistroUsuario(LoginRequerido userReq);   

    }
}
