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

public class ClienteTelefonoController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClienteTelefonoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ClienteTelefonoDto>>> Get()
    {
        var variable = await _unitOfWork.ClientesTelefonos.GetAllAsync();
        return _mapper.Map<List<ClienteTelefonoDto>>(variable);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ClienteTelefono>> Post(ClienteTelefonoDto clientetelefonoDto)
    {
        var variable = _mapper.Map<ClienteTelefono>(clientetelefonoDto);



        _unitOfWork.ClientesTelefonos.Add(variable);

        await _unitOfWork.SaveAsync();
        if (variable == null)
        {
            return BadRequest();
        }
        clientetelefonoDto.Id = variable.Id;
        return CreatedAtAction(nameof(Post), new { id = clientetelefonoDto.Id }, clientetelefonoDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ClienteTelefonoDto>> Get(int id)
    {
        var variable = await _unitOfWork.ClientesTelefonos.GetByIdAsync(id);
        if (variable == null)
        {
            return NotFound();
        }
        return _mapper.Map<ClienteTelefonoDto>(variable);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ClienteTelefonoDto>> Put(int id, [FromBody] ClienteTelefonoDto clientetelefonoDto)
    {
        var variable = _mapper.Map<ClienteTelefono>(clientetelefonoDto);

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


        clientetelefonoDto.Id = variable.Id;
        _unitOfWork.ClientesTelefonos.Update(variable);
        await _unitOfWork.SaveAsync();
        return clientetelefonoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var variable = await _unitOfWork.ClientesTelefonos.GetByIdAsync(id);
        if (variable == null)
        {
            return NotFound();
        }
        _unitOfWork.ClientesTelefonos.Remove(variable);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}