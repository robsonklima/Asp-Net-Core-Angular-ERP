using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class TipoChamadoSTNMap : IEntityTypeConfiguration<TipoChamadoSTN>
    {
        public void Configure(EntityTypeBuilder<TipoChamadoSTN> builder)
        {
            builder
                .ToTable("TipoChamadoSTN");

            builder
                .HasKey(prop => prop.CodTipoChamadoSTN);
        }
    }
}