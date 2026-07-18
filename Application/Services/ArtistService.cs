using Application.Common.Exceptions;
using Application.DTOs.Artists;
using Application.Interfaces;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class ArtistService : IArtistService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ArtistService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ArtistDto> CreateAsync(CreateArtistDto dto)
    {
        if (await _unitOfWork.Artists.EmailExistsAsync(dto.Email))
            throw new ConflictException($"An artist with email '{dto.Email}' already exists.");

        var artist = _mapper.Map<Artist>(dto);

        await _unitOfWork.Artists.AddAsync(artist);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ArtistDto>(artist);
    }

    public async Task<List<ArtistDto>> GetAllAsync()
    {
        var artists = await _unitOfWork.Artists.GetAllAsync();
        return _mapper.Map<List<ArtistDto>>(artists);
    }
}
