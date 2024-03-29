﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Mappers;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Encounters.API.Internal;
using Explorer.Encounters.API.Dtos;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourStatisticsService: ITourStatisticsService
    {
        private readonly IInternalTourOwnershipService _internalTourOwnershipService;
        private readonly ITourRepository _tourRepository;
        private readonly ITourExecutionRepository _tourExecutionRepository;
        private readonly IInternalEncounterExecutionService _internalEncounterExecutionService;
        public TourStatisticsService(IInternalTourOwnershipService internalTourOwnershipService, ITourRepository tourRepository, ITourExecutionRepository tourExecutionRepository, IInternalEncounterExecutionService internalEncounterExecutionService)
        {
            _internalTourOwnershipService = internalTourOwnershipService;
            _tourRepository = tourRepository;
            _tourExecutionRepository = tourExecutionRepository;
            _internalEncounterExecutionService = internalEncounterExecutionService;
        }

        public Result<List<long>> GetSoldToursIds()
        {
            return _internalTourOwnershipService.GetSoldToursIds();
        }

        public Result<List<long>> GetStartedToursIds()
        {
            return _tourExecutionRepository.GetStartedToursIds();
        }

        public Result<List<long>> GetFinishedToursIds()
        {
            return _tourExecutionRepository.GetFinishedToursIds();
        }

        public Result<List<Tour>> GetPublishedToursByAuthor(long authorId)
        {
            return _tourRepository.GetPublishedToursByAuthor(authorId);
        }

        public Result<List<long>> GetAuthorsPublishedSoldToursIds(long authorId)
        {
            try
            {
                List<long> soldTourIds = GetSoldToursIds().Value;

                List<Tour> publishedTours = GetPublishedToursByAuthor(authorId).Value;

                List<long> soldTourIdsInPublishedTours = soldTourIds
                    .Where(id => publishedTours.Any(t => t.Id == id))
                    .ToList();

                return (Result<List<long>>)soldTourIdsInPublishedTours;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Result<List<long>> GetAuthorsStartedToursIds(long authorId)
        {
            try
            {
                List<long> soldTourIds = GetStartedToursIds().Value;

                List<Tour> publishedTours = GetPublishedToursByAuthor(authorId).Value;

                List<long> soldTourIdsInPublishedTours = soldTourIds
                    .Where(id => publishedTours.Any(t => t.Id == id))
                    .ToList();

                return (Result<List<long>>)soldTourIdsInPublishedTours;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Result<List<long>> GetAuthorsFinishedToursIds(long authorId)
        {
            try
            {
                List<long> soldTourIds = GetFinishedToursIds().Value;

                List<Tour> publishedTours = GetPublishedToursByAuthor(authorId).Value;

                List<long> soldTourIdsInPublishedTours = soldTourIds
                    .Where(id => publishedTours.Any(t => t.Id == id))
                    .ToList();

                return (Result<List<long>>)soldTourIdsInPublishedTours;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Result<Dictionary<long, int>> GetTourCompletionNumberForEachTour(long authorId)
        {
            try
            {
                List<long> soldTourIds = GetAuthorsFinishedToursIds(authorId).Value;
                Dictionary<long, int> tourCompletitionNumberForEachTour = new Dictionary<long, int>();

                foreach (long tourId in soldTourIds)
                {
                    if (tourCompletitionNumberForEachTour.ContainsKey(tourId))
                    {
                        tourCompletitionNumberForEachTour[tourId]++;
                    }
                    else
                    {
                        tourCompletitionNumberForEachTour[tourId] = 1;
                    }
                }

                return (Result<Dictionary<long, int>>)tourCompletitionNumberForEachTour;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Result<Dictionary<long, int>> GetTourStartedNumberForEachTour(long authorId)
        {
            try
            {
                List<long> soldTourIds = GetAuthorsStartedToursIds(authorId).Value;
                Dictionary<long, int> touStartedNumberForEachTour = new Dictionary<long, int>();

                foreach (long tourId in soldTourIds)
                {
                    if (touStartedNumberForEachTour.ContainsKey(tourId))
                    {
                        touStartedNumberForEachTour[tourId]++;
                    }
                    else
                    {
                        touStartedNumberForEachTour[tourId] = 1;
                    }
                }

                return (Result<Dictionary<long, int>>)touStartedNumberForEachTour;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Result<Dictionary<long, double>> GetTourCompletionPercentageForEachTour(long authorId)
        {
            try
            {
                Result<Dictionary<long, int>> startedNumberResult = GetTourStartedNumberForEachTour(authorId);
                Result<Dictionary<long, int>> completionNumberResult = GetTourCompletionNumberForEachTour(authorId);

                Dictionary<long, int> startedNumberForEachTour = startedNumberResult.Value;
                Dictionary<long, int> completionNumberForEachTour = completionNumberResult.Value;

                Dictionary<long, double> completionPercentageForEachTour = new Dictionary<long, double>();

                foreach (long tourId in startedNumberForEachTour.Keys)
                {
                    int startedNumber = startedNumberForEachTour.ContainsKey(tourId) ? startedNumberForEachTour[tourId] : 0;
                    int completionNumber = completionNumberForEachTour.ContainsKey(tourId) ? completionNumberForEachTour[tourId] : 0;

                    double completionPercentage = (startedNumber == 0) ? 0 : ((double)completionNumber /(startedNumber + completionNumber)) * 100;

                    completionPercentageForEachTour.Add(tourId, completionPercentage);
                }

                return (Result<Dictionary<long, double>>)completionPercentageForEachTour;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Result<int> GetToursInCompletionRangeCount(long authorId, double minPercentage, double maxPercentage)
        {
            try
            {
                Result<Dictionary<long, double>> completionPercentageResult = GetTourCompletionPercentageForEachTour(authorId);
    
                Dictionary<long, double> completionPercentageForEachTour = completionPercentageResult.Value;

                int toursInRangeCount = completionPercentageForEachTour.Values
                    .Count(percentage => percentage >= minPercentage && percentage <= maxPercentage);

                return (Result<int>)toursInRangeCount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Result<int> GetAuthorsSoldToursNumber(long authorId)
        {
            return GetAuthorsPublishedSoldToursIds(authorId).Value.Count();
        }

        public Result<int> GetAuthorsStartedToursNumber(long authorId)
        {
            return GetAuthorsStartedToursIds(authorId).Value.Count();
        }

        public Result<int> GetAuthorsFinishedToursNumber(long authorId)
        {
            return GetAuthorsFinishedToursIds(authorId).Value.Count();
        }

        public Result<double> GetAuthorsTourCompletionPercentage(long authorId)
        {
            try
            {
                int startedToursCount = GetAuthorsStartedToursNumber(authorId).Value;
                int finishedToursCount = GetAuthorsFinishedToursNumber(authorId).Value;

                if (startedToursCount == 0)
                {
                    return (Result<double>)0;
                }

                double completionPercentage = (double)finishedToursCount / (startedToursCount + finishedToursCount) * 100;
                return (Result<double>)completionPercentage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Result<int> GetTourSalesCount(long authorId, long tourId)
        {
            try
            {
                List<long> authorsPublishedSoldToursIds = GetAuthorsPublishedSoldToursIds(authorId).Value;
                int tourSalesCount = authorsPublishedSoldToursIds.Count(id => id == tourId);
                return (Result<int>)tourSalesCount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Result<int> GetTourStartingsCount(long authorId, long tourId)
        {
            try
            {
                List<long> authorsStartedToursIds = GetAuthorsStartedToursIds(authorId).Value;
                int tourStartingsCount = authorsStartedToursIds.Count(id => id == tourId);
                return (Result<int>)tourStartingsCount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Result<int> GetTourFinishingCount(long authorId, long tourId)
        {
            try
            {
                List<long> authorsFinishedToursIds = GetAuthorsFinishedToursIds(authorId).Value;
                int tourFinishingCount = authorsFinishedToursIds.Count(id => id == tourId);
                return (Result<int>)tourFinishingCount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public double CalculatePercentages(int touristsReachedCheckpoint, int totalTourists)
        {
            double percentageReached = 0;
            if (totalTourists > 0 && touristsReachedCheckpoint > 0)
            {
                percentageReached = (double)touristsReachedCheckpoint / totalTourists * 100;
            }
            return percentageReached;
        }
        public Result<List<CheckpointStatisticsDto>> CalculateCheckpointArrivalPercentages(long tourId) //izmeni ime
        {
            Tour tour = _tourRepository.Get(tourId);
            List<TourExecution> tourExecutions = _tourExecutionRepository.GetByTourId(tourId);
            List<CheckpointStatisticsDto> checkpointStatisticsDtos = new List<CheckpointStatisticsDto>();

            HashSet<Tuple<long, long>> visitedCheckpoints = new HashSet<Tuple<long, long>>();

            foreach (var checkpoint in tour.Checkpoints)
            {
                int count = 0;
                foreach (var tourExecution in tourExecutions)
                {
                    foreach(var completedCheckpoint in tourExecution.CompletedCheckpoints)
                    {
                        var checkpointTuple = Tuple.Create(tourExecution.Id, completedCheckpoint.CheckpointId);
                        if (!visitedCheckpoints.Contains(checkpointTuple) && completedCheckpoint.CheckpointId == checkpoint.Id)
                        {
                            visitedCheckpoints.Add(checkpointTuple);
                            count++;
                        }
                    }
                }
                var percentageReached = CalculatePercentages(count, tourExecutions.Count);
                var percetnageFinishedEncounter = GetPercentageOfTouristsFinishedEncounter(checkpoint);
                var checkpointStatisticsDto = new CheckpointStatisticsDto(checkpoint.Id, checkpoint.Name, percentageReached, percetnageFinishedEncounter);
                checkpointStatisticsDtos.Add(checkpointStatisticsDto);
            }
            return (Result<List<CheckpointStatisticsDto>>)checkpointStatisticsDtos;
        }

        public double CalculateEncountersPercentages(int touristsFinishedEncounter, int totalTourists)
        {
            double percentageFinishedEncounter = 0;

            if (totalTourists > 0)
            {
                percentageFinishedEncounter = (double)touristsFinishedEncounter / totalTourists * 100;
            }

            return percentageFinishedEncounter;
        }


        public double GetPercentageOfTouristsFinishedEncounter(Checkpoint checkpoint)
        {
            try
            {

                List<EncounterExecutionDto> encounterExecutionDtos = new List<EncounterExecutionDto>();
                encounterExecutionDtos = _internalEncounterExecutionService.GetByEncounter(checkpoint.EncounterId).Value; 
                var totalTourists = encounterExecutionDtos.Count();
                if (totalTourists  == 0)
                {
                    return 0;
                }
                var touristsFinishedEncounter = encounterExecutionDtos.Count(e => e.Status.Equals("Completed"));
                double percentageFinishedEncounter = CalculateEncountersPercentages(touristsFinishedEncounter, totalTourists);
                return percentageFinishedEncounter;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
