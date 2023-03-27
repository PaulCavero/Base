using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Data.Models.Sensors;

namespace Base.Data.Models.Measurements;

public partial class Measurement
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long MeasurementId { get; set; }

    public decimal? MeasurementTemperature { get; set; }

    public decimal? MeasurementPressure { get; set; }

    public Guid SensorId { get; set; }

    public virtual Sensor Sensor { get; set; } = null!;
}
