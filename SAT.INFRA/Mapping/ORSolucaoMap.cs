using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ORSolucaoMap : IEntityTypeConfiguration<ORSolucao>
    {
        public void Configure(EntityTypeBuilder<ORSolucao> builder)
        {
            builder.ToTable("ORSolucao");
            builder.HasKey(i => i.CodSolucao);

            builder.Property(i => i.Descricao).HasColumnName("Desricao");
       }
    }
}