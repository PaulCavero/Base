using System;
using System.Collections.Generic;
using Base.Data.Models.Measurements;
using Base.Data.Models.Sensors;
using Base.Util.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Base.Data;

public partial class BaseProjectContext : DbContext
{
    private readonly IOptions<DataBaseConfiguration> options;
    
    public BaseProjectContext(IOptions<DataBaseConfiguration> options)
    {
        this.options = options;
    }

    public virtual DbSet<Measurement> Measurements { get; set; }

    public virtual DbSet<Sensor> Sensors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        string connectionString = this.options.Value.DefaultConnection;
        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Measurement>(entity =>
        {
            entity.HasKey(e => e.MeasurementId).HasName("PK__Measurem__85599FB81E96613C");

            
            entity.Property(e => e.MeasurementPressure).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MeasurementTemperature).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Sensor).WithMany(p => p.Measurements)
                .HasForeignKey(d => d.SensorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Measureme__Senso__29572725");
        });

        modelBuilder.Entity<Sensor>(entity =>
        {
            entity.HasKey(e => e.SensorId).HasName("PK__Sensors__D8099BFAFC26E013");

            
            entity.Property(e => e.SensorCapacity).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SensorManufacturerName)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
