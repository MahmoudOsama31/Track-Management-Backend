using Application.DTOs.Artists;
using Application.DTOs.Tracks;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Artist, ArtistDto>();
        CreateMap<CreateArtistDto, Artist>();

        CreateMap<Track, TrackDto>()
            .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name));
        CreateMap<CreateTrackDto, Track>();

        CreateMap<Track, TrackDetailsDto>()
            .ForMember(dest => dest.Distributions, opt => opt.MapFrom(src => src.TrackDistributions));

        CreateMap<TrackDistribution, TrackDistributionDto>()
            .ForMember(dest => dest.DSPName, opt => opt.MapFrom(src => src.DSP.Name));
    }
}
