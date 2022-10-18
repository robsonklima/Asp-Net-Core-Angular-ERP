using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class IntegracaoMap : IEntityTypeConfiguration<Integracao>
    {
        public void Configure(EntityTypeBuilder<Integracao> builder)
        {
            builder.ToTable("OSIntegracao");
            builder.HasKey(prop => prop.CodOSIntegracao);
        }
    }
}