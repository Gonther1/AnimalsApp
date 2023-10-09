using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Cliente : BaseEntity
{
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string Apellidos { get; set; }
    [Required]
    public string Email { get; set; }
}
