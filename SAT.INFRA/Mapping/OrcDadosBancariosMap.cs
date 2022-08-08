using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrcDadosBancariosMap : IEntityTypeConfiguration<OrcDadosBancarios>
    {
        public void Configure(EntityTypeBuilder<OrcDadosBancarios> builder)
        {
            builder.ToTable("OrcDadosBancarios");
            builder.HasKey(i => i.CodOrcDadosBancarios);
        }
    }
}