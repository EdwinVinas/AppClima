using modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class ClimaContext : DbContext
    {
        public virtual DbSet<PronosticoClima> Pronostico { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        public ClimaContext() : base("name=Clima_Entities")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }

}
