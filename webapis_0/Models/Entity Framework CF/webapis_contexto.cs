namespace webapis_0.Models.Entity_Framework_CF
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class webapis_contexto : DbContext
    {
        public webapis_contexto()
            : base("name=webapis_modelo") { }

        public virtual DbSet<Proveedor> Proveedores { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<Linea_Factura> Items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.
                Add(new webapis_0.Models.Configuraciones_Fluent_API.ProveedorConfig())
                .Add(new webapis_0.Models.Configuraciones_Fluent_API.FacturaConfig())
                .Add(new webapis_0.Models.Configuraciones_Fluent_API.Linea_FacturaConfig());
        }
    }
}