using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AnimalsAppContext _context;
    public UnitOfWork(AnimalsAppContext context)
    {
        _context = context;
    }
}
