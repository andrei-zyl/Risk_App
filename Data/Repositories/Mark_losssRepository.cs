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
    
    class Mark_losssRepository : IRepository<Mark_loss>
    {
        SourceRiskContext context;
        public Mark_losssRepository(SourceRiskContext context)
        {
            this.context = context;
        }
        public void Create(Mark_loss t)
        {
            context.Mark_losss.Add(t);
        }
        public void Delete(int id)
        {
            var Mark_loss = context.Mark_losss.Find(id);
            context.Mark_losss.Remove(Mark_loss);
        }
        public IEnumerable<Mark_loss> Find(Func<Mark_loss, bool> predicate)
        {
            return context
            .Mark_losss
            .Include(g => g.Incidents)
            .Where(predicate)
            .ToList();
        }
        public Mark_loss Get(int id)
        {
            return context.Mark_losss.Find(id);
        }
        public IEnumerable<Mark_loss> GetAll()
        {
            return context.Mark_losss.Include(g => g.Incidents);
        }
        public void Update(Mark_loss t)
        {
            context.Entry<Mark_loss>(t).State = EntityState.Modified;
        }
    }
}
