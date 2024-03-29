﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class PrivateTourDatabaseRepository : CrudDatabaseRepository<PrivateTour, ToursContext>, IPrivateTourRepository
    {
        private readonly ToursContext _dbContext;
        public PrivateTourDatabaseRepository(ToursContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public PrivateTour CreateBlog(PrivateTour privateTour)
        {
            var entity = Get(privateTour.Id);
            entity.CreateBlog(privateTour.Blog);
            _dbContext.PrivateTours.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public PrivateTour Finish(PrivateTour privateTour)
        {
            var entity = Get(privateTour.Id);
            entity.Finish();
            _dbContext.PrivateTours.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Result<List<PrivateTour>> GetAll()
        {
            return _dbContext.PrivateTours.ToList();
        }

        public Result<List<PrivateTour>> GetAllByTourist(long touristId)
        {
            var checkpoints = DbContext.PrivateTours.Where(n => n.TouristId == touristId).ToList().ToResult();
            return checkpoints;
        }

        public PrivateTour GetOne(long id)
        {
            return _dbContext.PrivateTours.SingleOrDefault(n => n.Id == id);
        }

        public PrivateTour Next(PrivateTour privateTour)
        {
            var entity = Get(privateTour.Id);
            entity.Next();
            _dbContext.PrivateTours.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public PrivateTour Start(PrivateTour privateTour)
        {
            var entity = Get(privateTour.Id);
            entity.Start();
            _dbContext.PrivateTours.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }
    }
}
