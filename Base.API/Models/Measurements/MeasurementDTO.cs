using System;
using System.Collections.Generic;
using Base.Data.Models.Sensors;

namespace Base.API.Models.Measurements;

public partial class MeasurementDTO
{
    public long MeasurementId { get; set; }

    public decimal? MeasurementTemperature { get; set; }

    public decimal? MeasurementPressure { get; set; }

    public Guid SensorId { get; set; }

    public virtual Sensor Sensor { get; set; } = null!;
}
