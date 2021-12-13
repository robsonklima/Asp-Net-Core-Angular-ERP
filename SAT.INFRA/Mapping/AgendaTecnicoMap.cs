using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class AgendaTecnicoMap : IEntityTypeConfiguration<AgendaTecnico>
    {
        public void Configure(EntityTypeBuilder<AgendaTecnico> builder)
        {
            builder
                .ToTable("AgendaTecnicoV2");

            builder
                .HasKey(prop => prop.CodAgendaTecnico);

            builder
                .HasOne(prop => prop.OrdemServico)
                .WithMany()
                .HasForeignKey(i => i.CodOS)
                .HasPrincipalKey(i => i.CodOS);
        }
    }
}