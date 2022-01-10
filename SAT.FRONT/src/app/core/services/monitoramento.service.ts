import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'
import { 
  Monitoramento, MonitoramentoData, monitoramentoItemConst as item, MonitoramentoParameters, monitoramentoStatusConst as status, monitoramentoTipoConst as tipo
} from '../types/monitoramento.types';
import moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class MonitoramentoService
{
  constructor(private http: HttpClient) { }

  obterPorParametros(parameters: MonitoramentoParameters): Observable<MonitoramentoData>
  {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key =>
    {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Monitoramento`, { params: params }).pipe(
      map((data: MonitoramentoData) => data)
    )
  }

  obterStatus(m: Monitoramento): string
  {
    const ociosidade = moment().diff(moment(m.dataHoraProcessamento), 'hours');

    switch (m.tipo)
    {
      case tipo.CHAMADO:
        if (ociosidade >= 3)
          return status.DANGER;

        if (ociosidade > 1 && ociosidade < 3)
          return status.WARNING;

        return status.OK;

      case tipo.SERVICO:
        if (ociosidade >= 3)
          return status.DANGER;

        if (ociosidade > 1 && ociosidade < 3)
          return status.WARNING;

        return status.OK;

      case tipo.INTEGRACAO:
        let max = 3;
        let med = 1;

        if (m.item == 'Analisa Inconsistencia Ponto' || m.item == 'Analisa Bloqueio Ponto') {
          max = 25;
          med = 24;
        }

        if (ociosidade >= max)
          return status.DANGER;

        if (ociosidade > med && ociosidade < max)
          return status.WARNING;

        return status.OK;

      case tipo.STORAGE:
        if (((m.total - m.emUso) / m.total) * 100 <= 30)
          return status.DANGER;

        return status.OK;

      case tipo.MEMORY:
        if (((m.total - m.emUso) / m.total) * 100 <= 30)
          return status.DANGER;

        return status.OK;
      default:
        return status.OK;
    }
  }

  obterDescricao(m: Monitoramento): string {
    switch (m.item) {
      case item.SERVICOSATINTEGRACAOBRBV2:
        return 'Serviço de integração dos chamados do portal do BRB DSR';
      case item.SERVICOSATINTEGRACAOMETROSP:
        return 'Serviço de integração dos chamados do portal do Metrô SP DSR';
      case item.SERVICOSATDADOIMPORTACAO:
        return 'Insere as planilhas importadas via SAT no banco de dados';
      case item.BANRISULENVIAEMAILSERVICE:
        return 'Envia ao cliente e-mails com PDF das ocorrências de chamados SAT';
      case item.ANALISABLOQUEIOPONTO:
        return 'Rotina de bloqueio de pontos automática';
      case item.ANALISAINCONSISTENCIAPONTO:
        return 'Rotina checagem de inconsistências dos pontos registrados';
      case item.TICKETLOG:
        return 'Rotina que integra os combustíveis aprovados com o terceiro';
    
      default:
        return null;
    }
  }
}