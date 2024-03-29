using System.Linq;
using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile             
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
        CreateMap<MapObjectDto, MapObject>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => MapObjectTypeFromString(src.Category)))
            .ReverseMap();
        CreateMap<TourPreferenceDto, TourPreference>().ReverseMap();
        CreateMap<TouristPositionDto, TouristPosition>().ReverseMap();
        CreateMap<CheckpointDto, Checkpoint>().ReverseMap();
        CreateMap<TourDto, Tour>().ReverseMap();
        CreateMap<ReportedIssueDto, ReportedIssue>()
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ReverseMap();
        CreateMap<ReportedIssueCommentDto, ReportedIssueComment>().ReverseMap();
        CreateMap<TourRatingDto, TourRating>().ReverseMap();
        CreateMap<PublicCheckpointDto, PublicCheckpoint>().ReverseMap();
        CreateMap<PublicMapObjectDto, PublicMapObject>()
                  .ForMember(dest => dest.Category, opt => opt.MapFrom(src => MapObjectTypeFromString(src.Category)))
                  .ReverseMap();
        CreateMap<TourDto, Tour>().ReverseMap();
        CreateMap<PublishedTourDto, PublishedTour>().ReverseMap();
        CreateMap<ArchivedTourDto, ArchivedTour>().ReverseMap();
        CreateMap<TourTimeDto, TourTime>().ReverseMap();
        CreateMap<TourExecutionDto, TourExecution>().ReverseMap();
        CreateMap<CheckpointCompletitionDto, CheckpointCompletition>().ReverseMap();
        CreateMap<CheckpointSecretDto, CheckpointSecret>().ReverseMap();
        CreateMap<CompositeTourCreationDto, CompositeTour>().ReverseMap();
        CreateMap<PrivateTourExecutionDto, PrivateTourExecution>().ReverseMap();
        CreateMap<PrivateTourDto, PrivateTour>()
                .ForMember(dest => dest.Checkpoints, opt => opt.MapFrom(src => src.Checkpoints))
                .ForMember(dest => dest.Execution, opt => opt.MapFrom(src => src.Execution))
                .ReverseMap();
        CreateMap<PrivateTourBlogDto, PrivateTourBlog>().ReverseMap();
        CreateMap<TourBundleDto, TourBundle>().ReverseMap();
    }

    private MapObjectType MapObjectTypeFromString(string category)
    {
        if (Enum.TryParse<MapObjectType>(category, true, out var mapObjectType))
        {
            return mapObjectType;
        }

        return MapObjectType.Other;
    }
}