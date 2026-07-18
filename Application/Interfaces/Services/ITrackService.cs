using Application.DTOs.Tracks;
using Domain.Enums;

namespace Application.Interfaces.Services;

public interface ITrackService
{
    Task<TrackDto> CreateAsync(CreateTrackDto dto);
    Task<List<TrackDto>> GetAllAsync(int? artistId, string? genre, TrackStatus? status);
    Task<TrackDetailsDto> GetByIdAsync(int id);
    Task<TrackDetailsDto> DistributeAsync(int id, DistributeTrackDto dto);
    Task<TrackDto> UpdateStatusAsync(int id, UpdateTrackStatusDto dto);
}
