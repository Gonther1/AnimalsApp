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

public class CiudadController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CiudadController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<CiudadDto>>> Get()
    {
        var varCiudad = await _unitOfWork.Ciudades.GetAllAsync();
        return _mapper.Map<List<CiudadDto>>(varCiudad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Ciudad>> Post(CiudadDto ciudadDto)
    {
        var varCiudad = _mapper.Map<Ciudad>(ciudadDto);



        _unitOfWork.Ciudades.Add(varCiudad);

        await _unitOfWork.SaveAsync();
        if (varCiudad == null)
        {
            return BadRequest();
        }
        ciudadDto.Id = varCiudad.Id;
        return CreatedAtAction(nameof(Post), new { id = ciudadDto.Id }, ciudadDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<CiudadDto>> Get(int id)
    {
        var varCiudad = await _unitOfWork.Ciudades.GetByIdAsync(id);
        if (varCiudad == null)
        {
            return NotFound();
        }
        return _mapper.Map<CiudadDto>(varCiudad);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<CiudadDto>> Put(int id, [FromBody] CiudadDto ciudadDto)
    {
        var varCiudad = _mapper.Map<Ciudad>(ciudadDto);

        if (varCiudad.Id == 0)
        {
            varCiudad.Id = id;
        }
        if (varCiudad.Id != id)
        {
            return BadRequest();
        }
        if (varCiudad == null)
        {
            return NotFound();
        }


        ciudadDto.Id = varCiudad.Id;
        _unitOfWork.Ciudades.Update(varCiudad);
        await _unitOfWork.SaveAsync();
        return ciudadDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var varCiudad = await _unitOfWork.Ciudades.GetByIdAsync(id);
        if (varCiudad == null)
        {
            return NotFound();
        }
        _unitOfWork.Ciudades.Remove(varCiudad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}