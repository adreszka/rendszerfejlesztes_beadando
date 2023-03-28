using AutoMapper;
using rendszerfejlesztes_beadando.Models;
using rendszerfejlesztes_beadando.Models.Entities;

namespace rendszerfejlesztes_beadando
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Component, ComponentModel>();
            CreateMap<ComponentModel, Component>();
        }
    }
}
