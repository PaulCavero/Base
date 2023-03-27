using Base.API.Models.Measurements;

namespace Base.API.Services.Measurements
{
    public interface IMeasurementService
    {
        Task AddMeasurementAsync(MeasurementDTO MeasurementDTO);
        Task ModifyMeasurementAsync(MeasurementDTO MeasurementDTO);
        Task RemoveMeasurementByIdAsync(long MeasurementId);
        IQueryable<MeasurementDTO> RetrieveAllMeasurement();
        ValueTask<MeasurementDTO> RetrieveMeasurementByIdAsync(long MeasurementId);
    }
}