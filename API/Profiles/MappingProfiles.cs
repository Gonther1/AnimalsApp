using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Pais,PaisDto>()
        .ReverseMap();

        CreateMap<Departamento,DepartamentoDto>()
        .ReverseMap();

        CreateMap<Ciudad,CiudadDto>()
        .ReverseMap();

        CreateMap<Cita,CitaDto>()
        .ReverseMap();

        CreateMap<Cliente,ClienteDto>()
        .ReverseMap();

        CreateMap<ClienteTelefono,ClienteTelefonoDto>()
        .ReverseMap();
        
        CreateMap<ClienteDireccion,ClienteDireccionDto>()
        .ReverseMap();
        
        CreateMap<Raza,RazaDto>()
        .ReverseMap();

        CreateMap<Mascota,MascotaDto>()
        .ReverseMap();

        CreateMap<Servicio, ServicioDto>()
        .ReverseMap();
        
    }
    
}