﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.Core.Domain.Encounters;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterRepository:ICrudRepository<Encounter>
    {
        Encounter MakeEncounterPublished(long id);
        List<Encounter> GetByIds(List<long> ids);
    }
}
