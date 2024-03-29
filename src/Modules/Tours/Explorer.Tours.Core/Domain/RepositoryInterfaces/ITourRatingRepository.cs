﻿using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
	public interface ITourRatingRepository : ICrudRepository<TourRating>
	{
		TourRating GetTourRating(int ratingId);
	}
}
