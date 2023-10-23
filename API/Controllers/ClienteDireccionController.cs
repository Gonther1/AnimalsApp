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

public class ClienteDireccionController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClienteDireccionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ClienteDireccionDto>>> Get()
    {
        var variable = await _unitOfWork.ClientesDirecciones.GetAllAsync();
        return _mapper.Map<List<ClienteDireccionDto>>(variable);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ClienteDireccion>> Post(ClienteDireccionDto clientedireccionDto)
    {
        var variable = _mapper.Map<ClienteDireccion>(clientedireccionDto);



        _unitOfWork.ClientesDirecciones.Add(variable);

        await _unitOfWork.SaveAsync();
        if (variable == null)
        {
            return BadRequest();
        }
        clientedireccionDto.Id = variable.Id;
        return CreatedAtAction(nameof(Post), new { id = clientedireccionDto.Id }, clientedireccionDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ClienteDireccionDto>> Get(int id)
    {
        var variable = await _unitOfWork.ClientesDirecciones.GetByIdAsync(id);
        if (variable == null)
        {
            return NotFound();
        }
        return _mapper.Map<ClienteDireccionDto>(variable);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ClienteDireccionDto>> Put(int id, [FromBody] ClienteDireccionDto clientedireccionDto)
    {
        var variable = _mapper.Map<ClienteDireccion>(clientedireccionDto);

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


        clientedireccionDto.Id = variable.Id;
        _unitOfWork.ClientesDirecciones.Update(variable);
        await _unitOfWork.SaveAsync();
        return clientedireccionDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var variable = await _unitOfWork.ClientesDirecciones.GetByIdAsync(id);
        if (variable == null)
        {
            return NotFound();
        }
        _unitOfWork.ClientesDirecciones.Remove(variable);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}