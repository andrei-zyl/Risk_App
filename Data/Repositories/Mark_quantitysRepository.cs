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
    
    class Mark_quantitysRepository : IRepository<Mark_quantity>
    {
        SourceRiskContext context;
        public Mark_quantitysRepository(SourceRiskContext context)
        {
            this.context = context;
        }
        public void Create(Mark_quantity t)
        {
            context.Mark_quantitys.Add(t);
        }
        public void Delete(int id)
        {
            var Mark_quantity = context.Mark_quantitys.Find(id);
            context.Mark_quantitys.Remove(Mark_quantity);
        }
        public IEnumerable<Mark_quantity> Find(Func<Mark_quantity, bool> predicate)
        {
            return context
            .Mark_quantitys
            .Include(g => g.Incidents)
            .Where(predicate)
            .ToList();
        }
        public Mark_quantity Get(int id)
        {
            return context.Mark_quantitys.Find(id);
        }
        public IEnumerable<Mark_quantity> GetAll()
        {
            return context.Mark_quantitys.Include(g => g.Incidents);
        }
        public void Update(Mark_quantity t)
        {
            context.Entry<Mark_quantity>(t).State = EntityState.Modified;
        }
    }
}
