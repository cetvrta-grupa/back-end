﻿using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface ITourBundleRepository : ICrudRepository<TourBundle>
{
    PagedResult<TourBundle> GetAllPublished(int page, int pageSize);
	List<TourBundle> GetAllByAuthor(long id);
	TourBundle GetBundleWithTours(long id);
}
