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

public class MascotaController : BaseControllerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MascotaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<MascotaDto>>> Get()
    {
        var variable = await _unitOfWork.Mascotas.GetAllAsync();
        return _mapper.Map<List<MascotaDto>>(variable);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Mascota>> Post(MascotaDto mascotaDto)
    {
        var variable = _mapper.Map<Mascota>(mascotaDto);



        _unitOfWork.Mascotas.Add(variable);

        await _unitOfWork.SaveAsync();
        if (variable == null)
        {
            return BadRequest();
        }
        mascotaDto.Id = variable.Id;
        return CreatedAtAction(nameof(Post), new { id = mascotaDto.Id }, mascotaDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MascotaDto>> Get(int id)
    {
        var variable = await _unitOfWork.Mascotas.GetByIdAsync(id);
        if (variable == null)
        {
            return NotFound();
        }
        return _mapper.Map<MascotaDto>(variable);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<MascotaDto>> Put(int id, [FromBody] MascotaDto mascotaDto)
    {
        var variable = _mapper.Map<Mascota>(mascotaDto);

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


        mascotaDto.Id = variable.Id;
        _unitOfWork.Mascotas.Update(variable);
        await _unitOfWork.SaveAsync();
        return mascotaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var variable = await _unitOfWork.Mascotas.GetByIdAsync(id);
        if (variable == null)
        {
            return NotFound();
        }
        _unitOfWork.Mascotas.Remove(variable);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}