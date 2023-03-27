using Base.Data.Models.Measurements;
using Base.Data.Models.Sensors;
using Base.Data.Repository;

namespace Base.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Repository<Measurement> MeasurementRepository { get; }
        Repository<Sensor> SensorRepository { get; }

        void Dispose();
        void Save();
    }
}