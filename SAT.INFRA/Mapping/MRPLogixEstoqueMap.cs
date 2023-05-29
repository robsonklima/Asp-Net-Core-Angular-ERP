using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class MRPLogixEstoqueMap : IEntityTypeConfiguration<MRPLogixEstoque>
    {
        public void Configure(EntityTypeBuilder<MRPLogixEstoque> builder)
        {
            builder.ToTable("MRPLogixEstoque");
            builder.HasKey(i => i.CodMRPLogixEstoque);
        }
    }
}