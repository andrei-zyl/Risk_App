using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Data.Entities;

namespace Data.EFContext
{
    public class SourceRiskContext : DbContext
    {      
        public SourceRiskContext(string name) : base(name)
        {
            Database.SetInitializer(new SourceRiskInitializer());
        }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<SourceRisk> SourceRisks { get; set; }
        public DbSet<ObjectRisk> ObjectRisks { get; set; }
        public DbSet<Loss> Losses { get; set; }
        public DbSet<Mark_frequency> Mark_frequencys { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Mark_loss> Mark_losss { get; set; }
        public DbSet<Mark_time> Mark_times { get; set; }
        public DbSet<Mark_quantity> Mark_quantitys { get; set; }
    }
}
