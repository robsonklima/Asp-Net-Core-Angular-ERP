using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ClientePecaMap : IEntityTypeConfiguration<ClientePeca>
    {
        public void Configure(EntityTypeBuilder<ClientePeca> builder)
        {
            builder
                .ToTable("ClientePeca");

            builder
                .HasKey(i => i.CodClientePeca);
        }
    }
}