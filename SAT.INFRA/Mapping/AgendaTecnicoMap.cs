using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class AgendaTecnicoMap : IEntityTypeConfiguration<AgendaTecnico>
    {
        public void Configure(EntityTypeBuilder<AgendaTecnico> builder)
        {
            builder.ToTable("AgendaTecnico");
            builder.HasKey(prop => prop.CodAgendaTecnico);
            builder.Ignore(prop => prop.Data);

            builder
                .HasOne(prop => prop.OrdemServico)
                .WithMany()
                .HasForeignKey(prop => prop.CodOS)
                .HasPrincipalKey(prop => prop.CodOS);
        }
    }
}