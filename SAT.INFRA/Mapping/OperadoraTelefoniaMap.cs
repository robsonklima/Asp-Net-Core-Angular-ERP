using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class OperadoraTelefoniaMap : IEntityTypeConfiguration<OperadoraTelefonia>
    {
        public void Configure(EntityTypeBuilder<OperadoraTelefonia> builder)
        {
            builder.ToTable("CodOperadoraTelefonia");
            builder.HasKey(i => i.CodOperadoraTelefonia);
        }
    }
}