using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ClienteTelefonoRepository : GenericRepository<ClienteTelefono>, IClienteTelefonoRepository
{
    private readonly AnimalsAppContext _context;
    public ClienteTelefonoRepository(AnimalsAppContext context) : base(context)
    {
        _context = context;
    }
}
