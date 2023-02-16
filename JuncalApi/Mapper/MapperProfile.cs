using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Mapper
{
    public class MapperProfile:Profile
    {

        public MapperProfile()
        {
            #region CAMION
            CreateMap<CamionRequerido, JuncalCamion>();
            CreateMap<JuncalCamion,CamionRespuesta >();
            #endregion
         
            #region CHOFER
            CreateMap<ChoferRequerido,JuncalChofer>();
            CreateMap<JuncalChofer,ChoferRespuesta>();
            #endregion
            
            #region TRANSPORTISTA
            CreateMap<TransportistaRequerido, JuncalTransportistum>();
            CreateMap<JuncalTransportistum, TransportistaRespuesta>();
            #endregion
         
            #region ACERIA
            CreateMap<AceriaRequerido, JuncalAcerium>();
            CreateMap< JuncalAcerium,AceriaRespuesta>();
            #endregion
          
            #region ACERIA MATERIAL
            CreateMap<AceriaMaterialRequerido, JuncalAceriaMaterial>();
            CreateMap<JuncalAceriaMaterial, AceriaMaterialRespuesta>();
            #endregion

            #region MATERIAL
            CreateMap<MaterialRequerido, JuncalMaterial>();
            CreateMap<JuncalMaterial, MaterialRespuesta>();
            #endregion

            #region ROLES
            CreateMap<RolesRequerido, JuncalRole>();
            CreateMap<JuncalRole, RolesRespuesta>();
            #endregion

            #region USUARIO
            CreateMap<UsuarioRequerido, JuncalUsuario>();
            CreateMap<JuncalUsuario, UsuarioRespuesta>();
            #endregion
        }



    }
}
