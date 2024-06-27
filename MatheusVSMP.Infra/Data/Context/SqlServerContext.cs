using MatheusVSMP.Business.Models.Fornecedores;
using MatheusVSMP.Business.Models.Produtos;
using MatheusVSMP.Infra.Data.Mappings;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MatheusVSMP.Infra.Data.Context
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext() : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar").HasMaxLength(100));

            modelBuilder.Configurations.Add(new FornecedorMapping());
            modelBuilder.Configurations.Add(new EnderecoMapping());
            modelBuilder.Configurations.Add(new ProdutoMapping());

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach(var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Property(nameof(Produto.DataCadastro)).CurrentValue = DateTime.Now;
                }
                if(entry.State == EntityState.Modified)
                {
                    entry.Property(nameof(Produto.DataCadastro)).IsModified = false;

                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
