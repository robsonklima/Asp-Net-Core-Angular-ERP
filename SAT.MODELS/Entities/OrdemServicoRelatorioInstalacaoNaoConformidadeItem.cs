using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("OSRelatorioInstalacaoNaoConformidadeItem")]
public class OrdemServicoRelatorioInstalacaoNaoConformidadeItem {
    [Key]
    public int CodOSRelatorioNaoConformidadeItem { get; set; }
    public string Item { get; set; }
    public byte IndAtivo { get; set; }
}