using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
{
    private readonly AnimalsAppContext _context;
    public ClienteRepository(AnimalsAppContext context) : base(context)
    {
        _context = context;
    }
}
