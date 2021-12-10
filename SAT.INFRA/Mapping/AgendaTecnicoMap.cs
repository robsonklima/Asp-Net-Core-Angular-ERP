using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class AgendaTecnicoMap : IEntityTypeConfiguration<AgendaTecnico>
    {
        public void Configure(EntityTypeBuilder<AgendaTecnico> builder)
        {
            builder
                .ToTable("AgendaTecnicoV2");

            builder
                .HasKey(prop => prop.CodAgendaTecnico);

            // builder
            //     .Ignore(prop => prop.Cor);

            // builder
            //     .Ignore(prop => prop.Titulo);
        }
    }
}