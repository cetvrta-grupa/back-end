﻿using Explorer.Tours.API.Dtos;
using Explorer.API.Controllers.Author.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.API.Controllers.Tourist.Tour;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Tours.Tests.Integration.Tourism;

[Collection("Sequential")]

public class TourExecutionQueryTests : BaseToursIntegrationTest
{
    public TourExecutionQueryTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_by_tour_and_user()
    {
        var userId = 4;
        var tourId = -1;
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope, userId.ToString());

        // Act
        var result = ((ObjectResult)controller.Get(userId, tourId).Result)?.Value as TourExecutionDto;

        // Assert
        result.ShouldNotBeNull();
    }

    private static TourExecutionController CreateController(IServiceScope scope, string personId)
    {
        return new TourExecutionController(scope.ServiceProvider.GetRequiredService<ITourExecutionService>())
        {
            ControllerContext = BuildContext(personId)
        };
    }
}