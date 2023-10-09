using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class ClienteDireccion : BaseEntity
{
    [Required]
    public int IdCliente { get; set; }

    public string TipoDeVia { get; set; }
    public int NumeroPri { get; set; }
    public string Letra { get; set; }
    public string Bis { get; set; }
    public string LetraSec { get; set; }
    public string Caridnal { get; set; }
    public int NumeroSec { get; set; }
    public string LetraTer { get; set; }
    public int NumeroTer { get; set; }
    public string CardinalSec { get; set; }
    public string Complemento { get; set; }
    public string CodigoPostal { get; set; }

    [Required]
    public int IdCiudad { get; set; }

}
