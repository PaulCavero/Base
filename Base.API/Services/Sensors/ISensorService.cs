using Base.API.Models.Sensors;

namespace Base.API.Services.Sensors
{
    public interface ISensorService
    {
        Task AddSensorAsync(SensorDTO SensorDTO);
        Task ModifySensorAsync(SensorDTO sensorDTO);
        Task RemoveSensorByIdAsync(Guid sensorId);
        IQueryable<SensorDTO> RetrieveAllSensor();
        ValueTask<SensorDTO> RetrieveSensorByIdAsync(Guid sensorId);
    }
}