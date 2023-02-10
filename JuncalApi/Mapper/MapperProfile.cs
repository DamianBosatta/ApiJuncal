using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;

namespace JuncalApi.Mapper
{
    public class MapperProfile:Profile
    {

        public MapperProfile()
        {
            CreateMap<CamionRequerido, JuncalCamion>();
            CreateMap<JuncalCamion,CamionRespuesta >();
            CreateMap<ChoferRequerido,JuncalChofer>();
            CreateMap<JuncalChofer,ChoferRespuesta>();
            CreateMap<TransportistaRequerido, JuncalTransportistum>();
            CreateMap<JuncalTransportistum, TransportistaRespuesta>();


        }



    }
}
