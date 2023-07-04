using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class AgendamentoMap : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.ToTable("AgendamentoOS");
            builder.HasKey(prop => prop.CodAgendamento);
            
            builder
                .HasOne(prop => prop.MotivoAgendamento)
                .WithMany()
                .HasForeignKey(prop => prop.CodMotivo)
                .HasPrincipalKey(prop => prop.CodMotivo);
        }
    }
}