using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces;

public interface IUnitOfWork
{
    IPaisRepository Paises { get; }
    ICiudadRepository Ciudades { get; }
    IDepartamentoRepository Departamentos { get; }
    ICitaRepository Citas { get; }
    IClienteRepository Clientes { get; }
    IClienteDireccionRepository ClientesDirecciones { get; }
    IClienteTelefonoRepository ClientesTelefonos { get; }
    IMascotaRepository Mascotas { get; }
    IRazaRepository Razas { get; }
    IServicioRepository Servicios { get; }
    Task<int> SaveAsync();
}
