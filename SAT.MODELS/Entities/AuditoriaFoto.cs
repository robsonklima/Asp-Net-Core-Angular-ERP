namespace SAT.MODELS.Entities
{

    public class AuditoriaFoto
        {
            public int CodAuditoriaFoto { get; set; }
            public int CodAuditoria { get; set; }
            public Auditoria Auditoria { get; set; }
            public string Foto { get; set; }

        }
}