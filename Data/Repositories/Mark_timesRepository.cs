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
    
    class Mark_timesRepository : IRepository<Mark_time>
    {
        SourceRiskContext context;
        public Mark_timesRepository(SourceRiskContext context)
        {
            this.context = context;
        }
        public void Create(Mark_time t)
        {
            context.Mark_times.Add(t);
        }
        public void Delete(int id)
        {
            var Mark_time = context.Mark_times.Find(id);
            context.Mark_times.Remove(Mark_time);
        }
        public IEnumerable<Mark_time> Find(Func<Mark_time, bool> predicate)
        {
            return context
            .Mark_times
            .Include(g => g.Incidents)
            .Where(predicate)
            .ToList();
        }
        public Mark_time Get(int id)
        {
            return context.Mark_times.Find(id);
        }
        public IEnumerable<Mark_time> GetAll()
        {
            return context.Mark_times.Include(g => g.Incidents);
        }
        public void Update(Mark_time t)
        {
            context.Entry<Mark_time>(t).State = EntityState.Modified;
        }
    }
}
