﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Mappers;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class PublicMapObjectService : CrudService<PublicMapObjectDto, PublicMapObject>, IPublicObjectService
    {
        private readonly IInternalObjectRequestService _internalObjectRequestService;
        private readonly IMapObjectService _mapObjectService;
        private PublicMapObjectMapper _publicMapObjectMapper;
        public PublicMapObjectService(ICrudRepository<PublicMapObject> repository, IMapper mapper, IInternalObjectRequestService internalObjectRequestService, IMapObjectService mapObjectService) : base(repository, mapper)
        {
            _internalObjectRequestService = internalObjectRequestService;
            _mapObjectService = mapObjectService;
            _publicMapObjectMapper = new PublicMapObjectMapper();
        }

        public Result<PublicMapObjectDto> Create(int objectRequestId, string notificationComment)
        {
            _internalObjectRequestService.AcceptRequest(objectRequestId, notificationComment);
            var request = _internalObjectRequestService.Get(objectRequestId);
            var mapObject = _mapObjectService.Get(request.Value.MapObjectId);           
            return Create(_publicMapObjectMapper.createDto(mapObject.Value));
        }
    }
}
