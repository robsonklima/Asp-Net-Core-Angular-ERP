using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class PecaRE5114Map : IEntityTypeConfiguration<PecaRE5114>
    {
        public void Configure(EntityTypeBuilder<PecaRE5114> builder)
        {
            builder
                .ToTable("PecaRE5114");

            builder
                .HasKey(i => i.CodPecaRe5114);

            builder
                .HasOne(prop => prop.Peca)
                .WithMany()
                .HasForeignKey(prop => prop.CodPeca)
                .HasPrincipalKey(prop => prop.CodPeca);

        }
    }
}