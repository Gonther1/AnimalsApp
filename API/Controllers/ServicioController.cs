using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers;

public class ServicioController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ServicioController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ServicioDto>>> Get()
    {
        var varServ = await _unitOfWork.Servicios.GetAllAsync();
        return _mapper.Map<List<ServicioDto>>(varServ);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Servicio>> Post(ServicioDto servDto)
    {
        var varServ = _mapper.Map<Servicio>(servDto);



        _unitOfWork.Servicios.Add(varServ);

        await _unitOfWork.SaveAsync();
        if (varServ == null)
        {
            return BadRequest();
        }
        servDto.Id = varServ.Id;
        return CreatedAtAction(nameof(Post), new { id = servDto.Id }, servDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ServicioDto>> Get(int id)
    {
        var varServ = await _unitOfWork.Servicios.GetByIdAsync(id);
        if (varServ == null)
        {
            return NotFound();
        }
        return _mapper.Map<ServicioDto>(varServ);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ServicioDto>> Put(int id, [FromBody] ServicioDto servDto)
    {
        var varServ = _mapper.Map<Servicio>(servDto);

        if (varServ.Id == 0)
        {
            varServ.Id = id;
        }
        if (varServ.Id != id)
        {
            return BadRequest();
        }
        if (varServ == null)
        {
            return NotFound();
        }


        servDto.Id = varServ.Id;
        _unitOfWork.Servicios.Update(varServ);
        await _unitOfWork.SaveAsync();
        return servDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var varServ = await _unitOfWork.Servicios.GetByIdAsync(id);
        if (varServ == null)
        {
            return NotFound();
        }
        _unitOfWork.Servicios.Remove(varServ);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}