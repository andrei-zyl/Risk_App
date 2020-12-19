using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<SourceRisk> SourceRisks { get; }
        IRepository<Incident> Incidents { get; }
        IRepository<ObjectRisk> ObjectRisks { get; }
        IRepository<Loss> Losses { get; }
        IRepository<Unit> Units { get; }
        IRepository<Mark_frequency> Mark_frequencys { get; }
        IRepository<Mark_loss> Mark_losss { get; }
        IRepository<Mark_time> Mark_times { get; }
        IRepository<Mark_quantity> Mark_quantitys { get; }
        void Save();
    }
}
