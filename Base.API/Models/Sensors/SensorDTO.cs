using System;
using System.Collections.Generic;
using Base.Data.Models.Measurements;

namespace Base.API.Models.Sensors;

public partial class SensorDTO
{
    public Guid SensorId { get; set; }

    public string? SensorManufacturerName { get; set; }

    public decimal SensorCapacity { get; set; }

    public virtual ICollection<Measurement> Measurements { get; } = new List<Measurement>();
}
