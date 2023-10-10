using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Servicio : BaseEntity
{
    [Required]
    public string Nombre { get; set; }
    [Required]
    public double Precio { get; set; }
    public ICollection<Cita> Citas { get; set; }
}
