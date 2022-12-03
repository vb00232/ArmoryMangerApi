﻿using ArmoryManagerApi.DataTransferObjects.ReloadDtos;
using ArmoryManagerApi.Helper;
using ArmoryManagerApi.Interfaces;
using ArmoryManagerApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ArmoryManagerApi.Controllers;

[EnableCors("CorsPolicy")]
[Route("api/reload")]
[ApiController]
[Authorize]
public class ReloadController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReloadController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReload(CreateReloadDto newReload)
    {
        if (!long.TryParse(HttpContext.Request.Headers["UserId"].ToString(), out long userId))
        {
            return BadRequest("Invalid User");
        }

        var reload = _mapper.Map<Reload>(newReload);
        reload.UserId = userId;
        reload.CreatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);
        reload.UpdatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);

        _unitOfWork.ReloadRepository.AddReload(reload);

        await _unitOfWork.SaveAsync();

        return StatusCode(201);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReload(long id)
    {
        await _unitOfWork.ReloadRepository.DeleteReloadAsync(id);

        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReloads()
    {     
        var reloads = await _unitOfWork.ReloadRepository.GetAllReloadsAsync();
        var reloadsDto = _mapper.Map<IEnumerable<ReloadDto>>(reloads);

        return Ok(reloadsDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReload(long id)
    {
        var reload = await _unitOfWork.ReloadRepository.GetReloadAsync(id);
        var reloadDto = _mapper.Map<ReloadDto>(reload);
        
        return Ok(reloadDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReload(long id, ReloadDto updatedReloadDto)
    {
        if (id != updatedReloadDto.Id)
        {
            return BadRequest("Update not allowed");
        }
        var reload = await _unitOfWork.ReloadRepository.GetReloadAsync(id);
        _mapper.Map(updatedReloadDto, reload);
        reload.UpdatedAt = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT);

        await _unitOfWork.SaveAsync();

        return Ok();
    }    
}
