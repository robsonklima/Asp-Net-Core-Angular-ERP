using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Mapping
{
    public class DespesaCartaoCombustivelTecnicoMap : IEntityTypeConfiguration<DespesaCartaoCombustivelTecnico>
    {
        public void Configure(EntityTypeBuilder<DespesaCartaoCombustivelTecnico> builder)
        {
            builder
                .ToTable("DespesaCartaoCombustivelTecnico");

            builder
                .HasKey(prop => prop.CodDespesaCartaoCombustivelTecnico);

            builder
                .Property(e => e.CodTecnico)
                .HasConversion<string>();

            builder
              .HasOne(i => i.DespesaCartaoCombustivel)
              .WithOne()
              .HasForeignKey<DespesaCartaoCombustivel>(i => i.CodDespesaCartaoCombustivel)
              .HasPrincipalKey<DespesaCartaoCombustivelTecnico>(i => i.CodDespesaCartaoCombustivel);
        }
    }
}