using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Data.Interfaces;
using Data.EFContext;

namespace Data.Repositories
{
    public class EntityUnitOfWork : IUnitOfWork
    {
        SourceRiskContext context;
        SourceRisksRepository SourceRisksRepository;
        IncidentRepository IncidentsRepository;
        ObjectRisksRepository ObjectRisksRepository;
        LossesRepository LossesRepository;
        UnitsRepository UnitsRepository;
        Mark_quantitysRepository Mark_quantitysRepository;
        Mark_frequencysRepository Mark_frequencysRepository;
        Mark_timesRepository Mark_timesRepository;
        Mark_losssRepository Mark_losssRepository;

        public EntityUnitOfWork(string name)
        {
            context = new SourceRiskContext(name);
        }
        public IRepository<SourceRisk> SourceRisks
        {
            get
            {
                if (SourceRisksRepository == null)
                    SourceRisksRepository = new SourceRisksRepository(context);
                return SourceRisksRepository;
            }
        }
        public IRepository<Incident> Incidents
        {
            get
            {
                if (IncidentsRepository == null)
                    IncidentsRepository =
                    new IncidentRepository(context);
                return IncidentsRepository;
            }
        }
        public IRepository<ObjectRisk> ObjectRisks
        {
            get
            {
                if (ObjectRisksRepository == null)
                    ObjectRisksRepository = new ObjectRisksRepository(context);
                return ObjectRisksRepository;
            }
        }
        public IRepository<Loss> Losses
        {
            get
            {
                if (LossesRepository == null)
                    LossesRepository = new LossesRepository(context);
                return LossesRepository;
            }
        }
        public IRepository<Unit> Units
        {
            get
            {
                if (UnitsRepository == null)
                    UnitsRepository = new UnitsRepository(context);
                return UnitsRepository;
            }
        }
        public IRepository<Mark_frequency> Mark_frequencys
        {
            get
            {
                if (Mark_frequencysRepository == null)
                    Mark_frequencysRepository = new Mark_frequencysRepository(context);
                return Mark_frequencysRepository;
            }
        }
        public IRepository<Mark_quantity> Mark_quantitys
        {
            get
            {
                if (Mark_quantitysRepository == null)
                    Mark_quantitysRepository = new Mark_quantitysRepository(context);
                return Mark_quantitysRepository;
            }
        }
        public IRepository<Mark_time> Mark_times
        {
            get
            {
                if (Mark_timesRepository == null)
                    Mark_timesRepository = new Mark_timesRepository(context);
                return Mark_timesRepository;
            }
        }
        public IRepository<Mark_loss> Mark_losss
        {
            get
            {
                if (Mark_losssRepository == null)
                    Mark_losssRepository = new Mark_losssRepository(context);
                return Mark_losssRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
