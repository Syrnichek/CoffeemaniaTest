using CoffeemaniaTest.Domain.Entities;

namespace CoffeemaniaTest.Application.DTO.Requests;

public record DistanceRequest(
    Coordinate? Coordinate1,
    Coordinate? Coordinate2
    );