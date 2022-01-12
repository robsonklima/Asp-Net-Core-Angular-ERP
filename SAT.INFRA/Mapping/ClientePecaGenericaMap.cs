using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ClientePecaGenericaMap : IEntityTypeConfiguration<ClientePecaGenerica>
    {
        public void Configure(EntityTypeBuilder<ClientePecaGenerica> builder)
        {
            builder
                .ToTable("ClientePecaGenerica");

            builder
                .HasKey(i => i.CodClientePecaGenerica);
        }
    }
}