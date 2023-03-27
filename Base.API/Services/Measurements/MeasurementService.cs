using Base.API.Models.Measurements;
using Base.Broker.Loggings;
using Base.Data.Models.Measurements;
using Base.Data.UnitOfWork;

namespace Base.API.Services.Measurements
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoggingBroker loggingBroker;

        public MeasurementService(IUnitOfWork unitOfWork, ILoggingBroker loggingBroker)
        {
            this.unitOfWork = unitOfWork;
            this.loggingBroker = loggingBroker;
        }
        public Task AddMeasurementAsync(MeasurementDTO measurementDTO)
        {
            try
            {
                ValidateMeasurementIsNull(measurementDTO);

                unitOfWork.MeasurementRepository.
                    Insert(ToMeasurement(measurementDTO));
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using insert a Measurement Type.";
                this.loggingBroker.LogError(ex);
                throw new ArgumentException(errorMessage, ex);
            }

            return Task.CompletedTask;
        }

        public IQueryable<MeasurementDTO> RetrieveAllMeasurement()
        {
            try
            {
                IEnumerable<Measurement> measurements = unitOfWork.MeasurementRepository.Get();
                return measurements.Select(x => ToMeasurementDTO(x)).AsQueryable<MeasurementDTO>();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using RetrieveAllMeasurementType.";
                this.loggingBroker.LogError(ex);
                throw new ArgumentException(errorMessage, ex);
            }
        }

        public ValueTask<MeasurementDTO> RetrieveMeasurementByIdAsync(long measurementId)
        {
            try
            {
                ValidateMeasurementId(measurementId);
                Measurement measurement = unitOfWork.MeasurementRepository.GetByID(measurementId);

                return new ValueTask<MeasurementDTO>(ToMeasurementDTO(measurement));
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using RetrieveAllMeasurementType.";
                this.loggingBroker.LogError(ex);
                throw new ArgumentException(errorMessage, ex);
            }
        }

        public Task ModifyMeasurementAsync(MeasurementDTO measurementDTO)
        {
            try
            {
                ValidateMeasurementIsNull(measurementDTO);
                Measurement measurement = ToMeasurement(measurementDTO);
                measurement.MeasurementId = measurementDTO.MeasurementId;
                unitOfWork.MeasurementRepository.Update(measurement);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using ModifyMeasurementAsync.";
                this.loggingBroker.LogError(ex);
                throw new ArgumentException(errorMessage, ex);
            }
            return Task.CompletedTask;
        }

        public Task RemoveMeasurementByIdAsync(long measurementId)
        {
            try
            {
                ValidateMeasurementId(measurementId);
                Measurement measurement = unitOfWork.MeasurementRepository.GetByID(measurementId);

                ValidateMeasurementExist(measurement);
                unitOfWork.MeasurementRepository.Delete(measurement);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using RemoveMeasurementTypeByIdAsync.";
                this.loggingBroker.LogError(ex);
                throw new ArgumentException(errorMessage, ex);
            }
            return Task.CompletedTask;
        }

        public static MeasurementDTO ToMeasurementDTO(Measurement measurement)
        {
            if (measurement == null) { return new MeasurementDTO(); }
            return new MeasurementDTO
            {
                MeasurementId = measurement.MeasurementId,
                MeasurementTemperature = measurement.MeasurementTemperature,
                MeasurementPressure = measurement.MeasurementPressure,
                SensorId = measurement.SensorId
            };
        }

        public static Measurement ToMeasurement(MeasurementDTO measurementDTO)
        {
            if (measurementDTO == null) { return new Measurement(); }
            return new Measurement
            {
                MeasurementId = measurementDTO.MeasurementId,
                MeasurementTemperature = measurementDTO.MeasurementTemperature,
                MeasurementPressure = measurementDTO.MeasurementPressure,
                SensorId = measurementDTO.SensorId
            };
        }

        private static void ValidateMeasurementIsNull(MeasurementDTO measurementDTO)
        {
            if (measurementDTO is null)
            {
                throw new ArgumentNullException
                            ("Measurement", "The Instance of Measurement cannot be null.");
            }
        }

        private static void ValidateMeasurementExist(Measurement measurement)
        {
            if (measurement is null)
            {
                throw new ArgumentNullException
                            ("Measurement", "The Instance of Measurement cannot befound on DataBase.");
            }
        }

        private static void ValidateMeasurementId(long measurementId)
        {
            if (measurementId == 0)
            {
                throw new ArgumentNullException
                        ("Measurement", "The ID of Measurement cannot be default.");
            }
        }
    }


}
