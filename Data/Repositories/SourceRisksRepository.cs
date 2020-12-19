using System;
using System.Collections.Generic;
using System.Linq;
using Data.Interfaces;
using Data.Entities;
using Data.EFContext;
using System.Data.Entity;

namespace Data.Repositories
{
    class SourceRisksRepository : IRepository<SourceRisk>
    {
        SourceRiskContext context;
        public SourceRisksRepository(SourceRiskContext context)
        {
            this.context = context;
        }
        public void Create(SourceRisk t)
        {
            context.SourceRisks.Add(t);
        }
        public void Delete(int id)
        {
            var sourcerisk = context.SourceRisks.Find(id);
            context.SourceRisks.Remove(sourcerisk);
        }
        public IEnumerable<SourceRisk> Find(Func<SourceRisk, bool> predicate)
        {
            return context
            .SourceRisks
            .Include(g => g.Incidents)
            .Where(predicate)
            .ToList();
        }
        public SourceRisk Get(int id)
        {
            return context.SourceRisks.Find(id);
        }
        public IEnumerable<SourceRisk> GetAll()
        {
            return context.SourceRisks.Include(g => g.Incidents);
        }
        public void Update(SourceRisk t)
        {
            context.Entry<SourceRisk>(t).State = EntityState.Modified;
        }
    }
}
