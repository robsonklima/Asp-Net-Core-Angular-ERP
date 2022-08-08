using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ContratoConfMap : IEntityTypeConfiguration<ContratoConf>
    {
        public void Configure(EntityTypeBuilder<ContratoConf> builder)
        {
            builder.ToTable("ContratoConf");
            builder.HasKey(i => i.CodContratoConf);
        }
    }
}