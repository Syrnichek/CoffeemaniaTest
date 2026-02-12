using CoffeemaniaTest.Domain.Entities;
using CoffeemaniaTest.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace CoffeemaniaTest.Application.Implementations;

public partial class DistanceCalculatorService (ILogger<DistanceCalculatorService> logger): IDistanceCalculatorService
{
    private const double EarthRadius = 6371; 
    
    public async Task<double> CalculateDistanceAsync(Coordinate? coordinate1, 
        Coordinate? coordinate2, CancellationToken cancellationToken)
    {
        if (coordinate1 == null || coordinate2 == null)
        {
            ArgumentIsNull();
            throw new ArgumentNullException(nameof(coordinate2), "Координаты не могут быть пустыми.");
        }

        try
        {
            return await Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                double sinLatitude1 = Math.Sin(Radians(coordinate1.Latitude));
                double sinLatitude2 = Math.Sin(Radians(coordinate2.Latitude));
                double cosLatitude1 = Math.Cos(Radians(coordinate1.Latitude));
                double cosLatitude2 = Math.Cos(Radians(coordinate2.Latitude));
                double cosLongitude = Math.Cos(Radians(coordinate1.Longitude) - Radians(coordinate2.Longitude));

                cancellationToken.ThrowIfCancellationRequested();

                double cosD = sinLatitude1 * sinLatitude2 + cosLatitude1 * cosLatitude2 * cosLongitude;

                cancellationToken.ThrowIfCancellationRequested();

                double d = Math.Acos(cosD);

                return EarthRadius * d;
            }, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            OperationCanceled();
            throw;
        }
    }

    private static double Radians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
    
    [LoggerMessage(LogLevel.Warning, "Операция была отменена")]
    public partial void OperationCanceled();

    [LoggerMessage(LogLevel.Error, "Координаты пустые")]
    public partial void ArgumentIsNull();
}