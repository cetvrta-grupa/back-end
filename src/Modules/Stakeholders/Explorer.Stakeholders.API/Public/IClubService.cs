﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IClubService
    {
        Result<PagedResult<ClubDto>> GetPaged(int page, int pageSize);
        Result<ClubDto> Create(ClubDto club);
        Result<ClubDto> Update(ClubDto club);
        Result Delete(int id); 
        Result<ClubDto> GetClubWithUsers(int id);
        Result<ClubDto> RemoveMember(int memberId, int clubId);
        Result<ClubDto> AddMember(int memberId, int clubId);
        Result<List<ClubDto>> GetClubsByUser(int userId);
    }
}