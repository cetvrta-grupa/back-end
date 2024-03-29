﻿using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Tours;

public class Equipment : Entity
{
    public string Name { get; init; }
    public string? Description { get; init; }

    public Equipment(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
        Name = name;
        Description = description;
    }

    public Equipment(long id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}