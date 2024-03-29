﻿using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Internal
{
    public interface IInternalTouristService
    {
        Result<TouristDto> UpdateTouristXpAndLevel(long touristId, int encounterXp);
        Result<TouristDto> Get(long touristId);
    }
}
