using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("OSRelatorioInstalacao")]
public class OrdemServicoRelatorioInstalacao {
    [Key]
    public int CodOSRelatorioInstalacao { get; set; }
    public int CodOS { get; set; }
    public int CodOSRelatorioInstalacaoItem { get; set; }
    [ForeignKey("CodOSRelatorioInstalacaoItem")]
    public OrdemServicoRelatorioInstalacaoItem OrdemServicoRelatorioInstalacaoItem { get; set; }
    [ForeignKey("CodOSRelatorioInstalacao")]
    public OrdemServicoRelatorioInstalacaoNaoConformidade OrdemServicoRelatorioInstalacaoNaoConformidade { get; set; }
    public int IndStatus { get; set; }
    public string Detalhe { get; set; }
}