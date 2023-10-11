using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

    public class CitaDto
    {
        public int Id { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public int IdCliente { get; set; }
        public int IdMascota { get; set; }
        public int ServicioId { get; set; }
        public string Numero { get; set; }
    }
