using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace API.Dtos
{
    public class DepartamentoDto 
    {
        public int Id { get; set; }
        public string NombreDepartamento { get; set; }
        public int IdPais { get; set; }
    }
}