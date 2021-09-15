using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("OSRelatorioInstalacaoItem")]
public class OrdemServicoRelatorioInstalacaoItem {
    [Key]
    public int CodOSRelatorioInstalacaoItem { get; set; }
    public string Item { get; set; }
    public int IndAtivo { get; set; }
}