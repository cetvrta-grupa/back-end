﻿using Explorer.API.Controllers.Tourist.Tourism;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Tourism
{
    [Collection("Sequential")]
    public class TouristPositionQueryTests : BaseToursIntegrationTest
    {

        public TouristPositionQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TouristPositionDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(4);
            result.TotalCount.ShouldBe(4);
        }

        private static TouristPositionController CreateController(IServiceScope scope)
        {
            return new TouristPositionController(scope.ServiceProvider.GetRequiredService<ITouristPositionService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}