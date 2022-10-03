using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OrdemServicoSTNMap : IEntityTypeConfiguration<OrdemServicoSTN>
    {
        public void Configure(EntityTypeBuilder<OrdemServicoSTN> builder)
        {
            builder.ToTable("ChamadosSTN");
            builder.HasKey(i => i.CodAtendimento);
        }
    }
}