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

public class DepartamentoController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DepartamentoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<DepartamentoDto>>> Get()
    {
        var variable = await _unitOfWork.Departamentos.GetAllAsync();
        return _mapper.Map<List<DepartamentoDto>>(variable);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Departamento>> Post(DepartamentoDto depDto)
    {
        var variable = _mapper.Map<Departamento>(depDto);



        _unitOfWork.Departamentos.Add(variable);

        await _unitOfWork.SaveAsync();
        if (variable == null)
        {
            return BadRequest();
        }
        depDto.Id = variable.Id;
        return CreatedAtAction(nameof(Post), new { id = depDto.Id }, depDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DepartamentoDto>> Get(int id)
    {
        var variable = await _unitOfWork.Departamentos.GetByIdAsync(id);
        if (variable == null)
        {
            return NotFound();
        }
        return _mapper.Map<DepartamentoDto>(variable);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<DepartamentoDto>> Put(int id, [FromBody] DepartamentoDto depDto)
    {
        var variable = _mapper.Map<Departamento>(depDto);

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


        depDto.Id = variable.Id;
        _unitOfWork.Departamentos.Update(variable);
        await _unitOfWork.SaveAsync();
        return depDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var variable = await _unitOfWork.Departamentos.GetByIdAsync(id);
        if (variable == null)
        {
            return NotFound();
        }
        _unitOfWork.Departamentos.Remove(variable);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}