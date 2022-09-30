using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrdemServicoSTNOrigemMap : IEntityTypeConfiguration<OrdemServicoSTNOrigem>
    {
        public void Configure(EntityTypeBuilder<OrdemServicoSTNOrigem> builder)
        {
            builder.ToTable("OrigemChamadoSTN");
            builder.HasKey(i => i.CodOrigemChamadoSTN);
        }
    }
}