﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Mappers;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class CheckpointRequestService : CrudService<CheckpointRequestDto,CheckpointRequest>, ICheckpointRequestService, IInternalCheckpointRequestService
    {
        private readonly ICheckpointRequestRepository _checkpointRequestRepository;
        private readonly INotificationRepository _notificationRepository;
        private CheckpointRequestMapper _checkpointRequestMapper;
        public CheckpointRequestService(ICrudRepository<CheckpointRequest> repository, IMapper mapper, ICheckpointRequestRepository checkpointRequestRepository, INotificationRepository notificationRepository) : base(repository, mapper) 
        {
            _checkpointRequestRepository = checkpointRequestRepository;
            _checkpointRequestMapper = new CheckpointRequestMapper();
            _notificationRepository = notificationRepository;
        }

        public Result<List<CheckpointRequestDto>> GetAll()
        {
            var objectRequests = CrudRepository.GetPaged(0, 0).Results.ToList();
            return MapToDto(objectRequests);
        }

        public Result<CheckpointRequestDto> RejectRequest(int id, string notificationComment)
        {
            var request = CrudRepository.Get(id);
            _notificationRepository.CreateRequestNotification(notificationComment, request.AuthorId, id);
            var objectRequest = _checkpointRequestRepository.RejectRequest(id);
            return MapToDto(objectRequest);
        }

        public Result<CheckpointRequestDto> AcceptRequest(int id, string notificationComment)
        {
            var request = CrudRepository.Get(id);
            _notificationRepository.CreateRequestNotification(notificationComment, request.AuthorId, id);
            var objectRequest = _checkpointRequestRepository.AcceptRequest(id);
            return MapToDto(objectRequest);
        }

        public Result<CheckpointRequestDto> Create(int checkpointId, int authorId, string status)
        {
            var checpointRequest = _checkpointRequestMapper.createDto(checkpointId, authorId, status);
            return Create(checpointRequest);
        }

        public Result<CheckpointRequestDto> GetRequestByCheckpointId(int checkpointId)
        {
            return MapToDto(_checkpointRequestRepository.GetRequestCheckpointId(checkpointId));
        }
    }
}
