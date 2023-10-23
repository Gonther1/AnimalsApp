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

public class RazaController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RazaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<RazaDto>>> Get()
    {
        var variable = await _unitOfWork.Razas.GetAllAsync();
        return _mapper.Map<List<RazaDto>>(variable);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Raza>> Post(RazaDto razaDto)
    {
        var variable = _mapper.Map<Raza>(razaDto);



        _unitOfWork.Razas.Add(variable);

        await _unitOfWork.SaveAsync();
        if (variable == null)
        {
            return BadRequest();
        }
        razaDto.Id = variable.Id;
        return CreatedAtAction(nameof(Post), new { id = razaDto.Id }, razaDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<RazaDto>> Get(int id)
    {
        var variable = await _unitOfWork.Razas.GetByIdAsync(id);
        if (variable == null)
        {
            return NotFound();
        }
        return _mapper.Map<RazaDto>(variable);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<RazaDto>> Put(int id, [FromBody] RazaDto razaDto)
    {
        var variable = _mapper.Map<Raza>(razaDto);

        if (variable.Id == 0)
        {
            variable.Id = id;
        }
        if (variable.Id != id)
        {
            return BadRequest();
        }
        if (variable == null)
        {
            return NotFound();
        }


        razaDto.Id = variable.Id;
        _unitOfWork.Razas.Update(variable);
        await _unitOfWork.SaveAsync();
        return razaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var variable = await _unitOfWork.Razas.GetByIdAsync(id);
        if (variable == null)
        {
            return NotFound();
        }
        _unitOfWork.Razas.Remove(variable);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}