using CoffeemaniaTest.Domain.Entities;

namespace CoffeemaniaTest.Domain.Interfaces;

public interface IDistanceCalculatorService
{
    Task<double> CalculateDistanceAsync(Coordinate? coordinate1, Coordinate? coordinate2,
        CancellationToken cancellationToken);
}