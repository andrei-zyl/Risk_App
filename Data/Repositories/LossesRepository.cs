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
    class LossesRepository : IRepository<Loss>
    {
        SourceRiskContext context;
        public LossesRepository(SourceRiskContext context)
        {
            this.context = context;
        }
        public void Create(Loss t)
        {
            context.Losses.Add(t);
        }
        public void Delete(int id)
        {
            var Loss = context.Losses.Find(id);
            context.Losses.Remove(Loss);
        }
        public IEnumerable<Loss> Find(Func<Loss, bool> predicate)
        {
            return context
            .Losses
            .Include(g => g.Incidents)
            .Where(predicate)
            .ToList();
        }
        public Loss Get(int id)
        {
            return context.Losses.Find(id);
        }
        public IEnumerable<Loss> GetAll()
        {
            return context.Losses.Include(g => g.Incidents);
        }
        public void Update(Loss t)
        {
            context.Entry<Loss>(t).State = EntityState.Modified;
        }
    }
}
