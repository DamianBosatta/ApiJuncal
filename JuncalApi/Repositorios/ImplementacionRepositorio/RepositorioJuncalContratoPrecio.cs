using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalContratoPrecio:RepositorioGenerico<JuncalContratoPrecio>,IRepositorioJuncalContratoPrecio
    {
        public RepositorioJuncalContratoPrecio(JuncalContext db) : base(db)
        {
        }
    }
}
