﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class PrivateTourService : CrudService<PrivateTourDto, PrivateTour>, IPrivateTourService
    {
        private readonly IPrivateTourRepository _repository;
        public PrivateTourService(IPrivateTourRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
        }
        public Result<PrivateTourDto> Add(PrivateTourDto dto)
        {
            return MapToDto(_repository.Create(MapToDomain(dto)));
        }

        public Result<PrivateTourDto> CreateBlog(PrivateTourDto privateTourDto)
        {
            return MapToDto(_repository.CreateBlog(MapToDomain(privateTourDto)));
        }

        public Result<PrivateTourDto> Finish(PrivateTourDto dto)
        {
            return MapToDto(_repository.Finish(MapToDomain(dto)));
        }

        public Result<List<PrivateTourDto>> GetAllByTourist(long id)
        {
            var tours = _repository.GetAllByTourist(id);
            List<PrivateTourDto> result= new List<PrivateTourDto>();
            foreach(var tour in tours.Value)
            {
                result.Add(MapToDto(tour));
            }
            return result;
        }

        public Result<PrivateTourDto> GetOne(long id)
        {
            return MapToDto(_repository.GetOne(id));
        }

        public Result<List<PrivateTourDto>> GetAll()
        {
            return MapToDto(_repository.GetAll());
        }

        public Result<PrivateTourDto> Next(PrivateTourDto dto)
        {
            return MapToDto(_repository.Next(MapToDomain(dto)));
        }

        public Result<PrivateTourDto> Start(PrivateTourDto dto)
        {
            return MapToDto(_repository.Start(MapToDomain(dto)));
        }
    }
}
