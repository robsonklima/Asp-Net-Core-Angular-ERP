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
        }
    }
}