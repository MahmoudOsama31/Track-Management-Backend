using Application.Common.Exceptions;
using Application.DTOs.Tracks;
using Application.Interfaces;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class TrackService : ITrackService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TrackService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TrackDto> CreateAsync(CreateTrackDto dto)
    {
        var artist = await _unitOfWork.Artists.GetByIdAsync(dto.ArtistId)
            ?? throw new NotFoundException($"Artist with id {dto.ArtistId} was not found.");

        if (await _unitOfWork.Tracks.IsrcExistsAsync(dto.ISRC))
            throw new ConflictException($"A track with ISRC '{dto.ISRC}' already exists.");

        var track = _mapper.Map<Track>(dto);
        track.Status = TrackStatus.Draft;

        await _unitOfWork.Tracks.AddAsync(track);
        await _unitOfWork.SaveChangesAsync();

        track.Artist = artist;
        return _mapper.Map<TrackDto>(track);
    }

    public async Task<List<TrackDto>> GetAllAsync(int? artistId, string? genre, TrackStatus? status)
    {
        var tracks = await _unitOfWork.Tracks.GetFilteredAsync(artistId, genre, status);
        return _mapper.Map<List<TrackDto>>(tracks);
    }

    public async Task<TrackDetailsDto> GetByIdAsync(int id)
    {
        var track = await _unitOfWork.Tracks.GetWithDetailsAsync(id)
            ?? throw new NotFoundException($"Track with id {id} was not found.");

        return _mapper.Map<TrackDetailsDto>(track);
    }

    public async Task<TrackDetailsDto> DistributeAsync(int id, DistributeTrackDto dto)
    {
        var track = await _unitOfWork.Tracks.GetWithDetailsAsync(id)
            ?? throw new NotFoundException($"Track with id {id} was not found.");

        var dsps = await _unitOfWork.DSPs.GetByIdsAsync(dto.DspIds);
        var missingDspId = dto.DspIds.FirstOrDefault(dspId => dsps.All(d => d.Id != dspId));
        if (missingDspId != 0)
            throw new NotFoundException($"DSP with id {missingDspId} was not found.");

        foreach (var dspId in dto.DspIds)
        {
            if (await _unitOfWork.TrackDistributions.ExistsAsync(id, dspId))
                throw new ConflictException($"Track {id} has already been distributed to DSP {dspId}.");
        }

        foreach (var dspId in dto.DspIds)
        {
            await _unitOfWork.TrackDistributions.AddAsync(new TrackDistribution
            {
                TrackId = id,
                DSPId = dspId,
                SubmittedAt = DateTime.UtcNow,
                Status = DistributionStatus.Pending
            });
        }

        track.Status = TrackStatus.Submitted;
        _unitOfWork.Tracks.Update(track);

        await _unitOfWork.SaveChangesAsync();

        var updatedTrack = await _unitOfWork.Tracks.GetWithDetailsAsync(id);
        return _mapper.Map<TrackDetailsDto>(updatedTrack);
    }

    public async Task<TrackDto> UpdateStatusAsync(int id, UpdateTrackStatusDto dto)
    {
        var track = await _unitOfWork.Tracks.GetByIdAsync(id)
            ?? throw new NotFoundException($"Track with id {id} was not found.");

        track.Status = dto.Status;
        _unitOfWork.Tracks.Update(track);
        await _unitOfWork.SaveChangesAsync();

        var updatedTrack = await _unitOfWork.Tracks.GetWithDetailsAsync(id);
        return _mapper.Map<TrackDto>(updatedTrack);
    }
}
