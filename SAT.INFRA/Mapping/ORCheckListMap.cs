using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class ORCheckListMap : IEntityTypeConfiguration<ORCheckList>
    {
        public void Configure(EntityTypeBuilder<ORCheckList> builder)
        {
            builder.ToTable("ORCheckList");
            builder.HasKey(i => i.CodORCheckList);

            builder
                .HasOne(prop => prop.UsuarioCadastro)
                .WithMany()
                .HasForeignKey(prop => prop.CodUsuarioCad)
                .HasPrincipalKey(prop => prop.CodUsuario);
        }
    }
}