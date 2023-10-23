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

public class PaisController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PaisController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<PaisDto>>> Get()
    {
        var variable = await _unitOfWork.Paises.GetAllAsync();
        return _mapper.Map<List<PaisDto>>(variable);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Pais>> Post(PaisDto paisDto)
    {
        var variable = _mapper.Map<Pais>(paisDto);



        _unitOfWork.Paises.Add(variable);

        await _unitOfWork.SaveAsync();
        if (variable == null)
        {
            return BadRequest();
        }
        paisDto.Id = variable.Id;
        return CreatedAtAction(nameof(Post), new { id = paisDto.Id }, paisDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PaisDto>> Get(int id)
    {
        var variable = await _unitOfWork.Paises.GetByIdAsync(id);
        if (variable == null)
        {
            return NotFound();
        }
        return _mapper.Map<PaisDto>(variable);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<PaisDto>> Put(int id, [FromBody] PaisDto paisDto)
    {
        var variable = _mapper.Map<Pais>(paisDto);

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


        paisDto.Id = variable.Id;
        _unitOfWork.Paises.Update(variable);
        await _unitOfWork.SaveAsync();
        return paisDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var variable = await _unitOfWork.Paises.GetByIdAsync(id);
        if (variable == null)
        {
            return NotFound();
        }
        _unitOfWork.Paises.Remove(variable);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}