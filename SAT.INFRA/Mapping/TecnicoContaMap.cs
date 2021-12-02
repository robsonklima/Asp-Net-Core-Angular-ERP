using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class TecnicoContaMap : IEntityTypeConfiguration<TecnicoConta>
    {
        public void Configure(EntityTypeBuilder<TecnicoConta> builder)
        {
            builder
            .ToTable("TecnicoConta");

            builder
                .HasKey(prop => prop.CodTecnicoConta);
        }
    }
}