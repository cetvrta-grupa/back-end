﻿using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Internal
{
    public interface IInternalObjectRequestService
    {
        Result<ObjectRequestDto> Create(int mapObjectId, int authorId, string status);
        Result<ObjectRequestDto> AcceptRequest(int objectRequestId, string notificationComment);
        Result<ObjectRequestDto> Get(int objectRequestId);
        Result<ObjectRequestDto> GetRequestByMapObjectId(int mapObjectId);
        Result Delete(int objectRequestId);
    }
}
