using Base.Data.UnitOfWork;
using Base.API.Models.Sensors;
using Base.Data.Models.Sensors;
using Base.Broker.Loggings;

namespace Base.API.Services.Sensors
{
    public class SensorService : ISensorService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoggingBroker loggingBroker;

        public SensorService(IUnitOfWork unitOfWork, ILoggingBroker loggingBroker)
        {
            this.unitOfWork = unitOfWork;
            this.loggingBroker = loggingBroker;
        }

        public Task AddSensorAsync(SensorDTO sensorDTO)
        {
            try
            {
                ValidateSensorIsNull(sensorDTO);
                
                unitOfWork.SensorRepository.Insert(ToSensor(sensorDTO));
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using insert a Permission Type.";
                this.loggingBroker.LogError(ex);
                throw new ArgumentException(errorMessage, ex);
            }

            return Task.CompletedTask;
        }

        public IQueryable<SensorDTO> RetrieveAllSensor()
        {
            try
            {
                IEnumerable<Sensor> Permissions = unitOfWork.SensorRepository.Get();
                return Permissions.Select(x => ToSensorDTO(x)).AsQueryable<SensorDTO>();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using RetrieveAllSensor.";
                this.loggingBroker.LogError(ex);
                throw new ArgumentException(errorMessage, ex);
            }
        }

        public ValueTask<SensorDTO> RetrieveSensorByIdAsync(Guid sensorId)
        {
            try
            {
                ValidateSensorId(sensorId);
                Sensor Sensor = unitOfWork.SensorRepository.GetByID(sensorId);

                return new ValueTask<SensorDTO>(ToSensorDTO(Sensor));
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using RetrieveAllSensor.";
                this.loggingBroker.LogError(ex);
                throw new ArgumentException(errorMessage, ex);
            }
        }

        public Task ModifySensorAsync(SensorDTO sensorDTO)
        {
            try
            {
                ValidateSensorIsNull(sensorDTO);
                
                unitOfWork.SensorRepository.Update(ToSensor(sensorDTO));
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using ModifySensorAsync.";
                this.loggingBroker.LogError(ex);
                throw new ArgumentException(errorMessage, ex);
            }
            return Task.CompletedTask;
        }

        public Task RemoveSensorByIdAsync(Guid sensorId)
        {
            try
            {
                ValidateSensorId(sensorId);
                Sensor sensor = unitOfWork.SensorRepository.GetByID(sensorId);

                ValidateSensorExist(ToSensorDTO(sensor));
                unitOfWork.SensorRepository.Delete(sensor);
                unitOfWork.Save();

            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using RemoveSensorByIdAsync.";
                this.loggingBroker.LogError(ex);
                throw new ArgumentException(errorMessage, ex);
            }
            return Task.CompletedTask;
        }

        public static SensorDTO ToSensorDTO(Sensor sensor)
        {
            if (sensor == null) { return new SensorDTO(); }
            return new SensorDTO
            {
                SensorId = sensor.SensorId,
                SensorManufacturerName = sensor.SensorManufacturerName,
                SensorCapacity = sensor.SensorCapacity
            };
        }

        public static Sensor ToSensor(SensorDTO sensorDTO)
        {
            if (sensorDTO == null) { return new Sensor(); }
            return new Sensor
            {
                SensorId = sensorDTO.SensorId,
                SensorManufacturerName = sensorDTO.SensorManufacturerName,
                SensorCapacity = sensorDTO.SensorCapacity
            };
        }

        private static void ValidateSensorIsNull(SensorDTO sensorDTO)
        {
            if (sensorDTO is null)
            {
                throw new ArgumentNullException
                            ("Sensor", "The Instance of Sensor cannot be null.");
            }
        }

        private static void ValidateSensorExist(SensorDTO sensor)
        {
            if (sensor is null)
            {
                throw new ArgumentNullException
                            ("Sensor", "The Instance of Sensor cannot befound on DataBase.");
            }
        }

        private static void ValidateSensorId(Guid sensorId)
        {
            if (sensorId == new Guid())
            {
                throw new ArgumentNullException
                        ("Sensor", "The ID of Sensor cannot be default.");
            }
        }
    }
}
