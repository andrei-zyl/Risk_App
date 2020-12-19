using Data.EFContext;
using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    
        class ObjectRisksRepository : IRepository<ObjectRisk>
        {
            SourceRiskContext context;
            public ObjectRisksRepository(SourceRiskContext context)
            {
                this.context = context;
            }
            public void Create(ObjectRisk t)
            {
                context.ObjectRisks.Add(t);
            }
            public void Delete(int id)
            {
                var objectrisk = context.ObjectRisks.Find(id);
                context.ObjectRisks.Remove(objectrisk);
            }
            public IEnumerable<ObjectRisk> Find(Func<ObjectRisk, bool> predicate)
            {
                return context
                .ObjectRisks
                .Include(g => g.Incidents)
                .Where(predicate)
                .ToList();
            }
            public ObjectRisk Get(int id)
            {
                return context.ObjectRisks.Find(id);
            }
            public IEnumerable<ObjectRisk> GetAll()
            {
                return context.ObjectRisks.Include(g => g.Incidents);
            }
            public void Update(ObjectRisk t)
            {
                context.Entry<ObjectRisk>(t).State = EntityState.Modified;
            }
        }
}
