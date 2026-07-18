using Application.DTOs.Artists;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Track_Management.Controllers;

[ApiController]
[Route("api/artists")]
public class ArtistsController : ControllerBase
{
    private readonly IArtistService _artistService;

    public ArtistsController(IArtistService artistService)
    {
        _artistService = artistService;
    }

    [HttpPost]
    public async Task<ActionResult<ArtistDto>> Create(CreateArtistDto dto)
    {
        var artist = await _artistService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { }, artist);
    }

    [HttpGet]
    public async Task<ActionResult<List<ArtistDto>>> GetAll()
    {
        return Ok(await _artistService.GetAllAsync());
    }
}
