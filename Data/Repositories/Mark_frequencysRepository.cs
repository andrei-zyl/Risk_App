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
    
    class Mark_frequencysRepository : IRepository<Mark_frequency>
    {
        SourceRiskContext context;
        public Mark_frequencysRepository(SourceRiskContext context)
        {
            this.context = context;
        }
        public void Create(Mark_frequency t)
        {
            context.Mark_frequencys.Add(t);
        }
        public void Delete(int id)
        {
            var Mark_frequency = context.Mark_frequencys.Find(id);
            context.Mark_frequencys.Remove(Mark_frequency);
        }
        public IEnumerable<Mark_frequency> Find(Func<Mark_frequency, bool> predicate)
        {
            return context
            .Mark_frequencys
            .Include(g => g.Incidents)
            .Where(predicate)
            .ToList();
        }
        public Mark_frequency Get(int id)
        {
            return context.Mark_frequencys.Find(id);
        }
        public IEnumerable<Mark_frequency> GetAll()
        {
            return context.Mark_frequencys.Include(g => g.Incidents);
        }
        public void Update(Mark_frequency t)
        {
            context.Entry<Mark_frequency>(t).State = EntityState.Modified;
        }
    }
}
