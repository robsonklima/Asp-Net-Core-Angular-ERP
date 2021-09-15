using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("OSRelatorioInstalacaoNaoConformidade")]
public class OrdemServicoRelatorioInstalacaoNaoConformidade {
    [Key]
    public int CodOSRelatorioInstalacao { get; set; }
    public int CodOS { get; set; }
    public int CodOSRelatorioNaoConformidadeItem { get; set; }
    [ForeignKey("CodOSRelatorioNaoConformidadeItem")]
    public OrdemServicoRelatorioInstalacaoNaoConformidadeItem OrdemServicoRelatorioInstalacaoNaoConformidadeItem { get; set; }
    public int IndStatus { get; set; }
    public string Detalhe { get; set; }
}