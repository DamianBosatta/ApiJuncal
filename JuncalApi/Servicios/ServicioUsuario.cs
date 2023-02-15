using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Seguridad;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace JuncalApi.Servicios
{
    public class ServicioUsuario : IServicioUsuario
    {
        private readonly IUnidadDeTrabajo _uow;
        public IConfiguration _configuration;

        public ServicioUsuario(IConfiguration configuration,IUnidadDeTrabajo uow)
        {
            _configuration = configuration;
            _uow = uow;
        }

        public dynamic InicioSesion(LoginRequerido userReq)
        {
       
           var data = JsonConvert.DeserializeObject<dynamic>(userReq.ToString());

            string usuario= data.Usuario.ToString();
            string contraseña = data.Contraseña.ToString();

            JuncalUsuario usuarioLoger = _uow.RepositorioJuncalUsuario.GetAll().Where(u => u.Usuario == usuario && u.Contraseña == contraseña).FirstOrDefault();

            if (usuarioLoger == null)
            {
                return new
                {
                    success = false,
                    message = "Credenciales Incorrectas",
                    result = ""

                };
              
            }

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                 new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                  new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                  new Claim("id",usuarioLoger.Id.ToString()),
                  new Claim("nombre", usuarioLoger.Nombre)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var InicioSesion = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires:DateTime.Now.AddMinutes(60),
                signingCredentials:InicioSesion
                );
          
            return new 
            {
                success = true,
                message = "Inicio Sesion Exitoso",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public int RegistroUsuario(LoginRequerido userReq)
        {
            throw new NotImplementedException();
        }
    }
}
