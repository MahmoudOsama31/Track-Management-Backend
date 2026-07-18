using Application.DTOs.Tracks;
using Application.Interfaces.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Track_Management.Controllers;

[ApiController]
[Route("api/tracks")]
public class TracksController : ControllerBase
{
    private readonly ITrackService _trackService;

    public TracksController(ITrackService trackService)
    {
        _trackService = trackService;
    }

    [HttpPost]
    public async Task<ActionResult<TrackDto>> Create(CreateTrackDto dto)
    {
        var track = await _trackService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = track.Id }, track);
    }

    [HttpGet]
    public async Task<ActionResult<List<TrackDto>>> GetAll([FromQuery] int? artistId, [FromQuery] string? genre, [FromQuery] TrackStatus? status)
    {
        return Ok(await _trackService.GetAllAsync(artistId, genre, status));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TrackDetailsDto>> GetById(int id)
    {
        return Ok(await _trackService.GetByIdAsync(id));
    }

    [Authorize]
    [HttpPost("{id:int}/distribute")]
    public async Task<ActionResult<TrackDetailsDto>> Distribute(int id, DistributeTrackDto dto)
    {
        return Ok(await _trackService.DistributeAsync(id, dto));
    }

    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult<TrackDto>> UpdateStatus(int id, UpdateTrackStatusDto dto)
    {
        return Ok(await _trackService.UpdateStatusAsync(id, dto));
    }
}
