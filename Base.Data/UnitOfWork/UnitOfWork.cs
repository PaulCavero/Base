using Base.Data.Models.Measurements;
using Base.Data.Models.Sensors;
using Base.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly BaseProjectContext context;
        private Repository<Sensor> sensorRepository;
        private Repository<Measurement> measurementRepository;
        public UnitOfWork(BaseProjectContext context)
        {
            this.context = context;
        }

        public Repository<Sensor> SensorRepository
        {
            get
            {
                if (this.sensorRepository == null)
                {
                    this.sensorRepository = new Repository<Sensor>(context);
                }
                return sensorRepository;
            }
        }

        public Repository<Measurement> MeasurementRepository
        {
            get
            {
                if (this.measurementRepository == null)
                {
                    this.measurementRepository = new Repository<Measurement>(context);
                }
                return measurementRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
