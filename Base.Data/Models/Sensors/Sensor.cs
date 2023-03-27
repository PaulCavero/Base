using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Data.Models.Measurements;

namespace Base.Data.Models.Sensors;

public partial class Sensor
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid SensorId { get; set; }

    public string? SensorManufacturerName { get; set; }

    public decimal SensorCapacity { get; set; }

    public virtual ICollection<Measurement> Measurements { get; } = new List<Measurement>();
}
