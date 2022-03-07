import { Component, OnInit } from '@angular/core';
import { IISLogService } from 'app/core/services/iislog.service';
import { IISLog } from 'app/core/types/iislog.types';
import moment from 'moment';

@Component({
  selector: 'app-ociosidade',
  templateUrl: './ociosidade.component.html'
})
export class OciosidadeComponent implements OnInit
{
  dataAtual = moment().format('yyyy-MM-DD HH:mm:ss');
  eventosOciosos: IISLog[] = [];
  opcoesDatas: any[] = [];

  constructor(
    private _iisLogService: IISLogService
  ) { }

  async ngOnInit()
  {
    this.obterOpcoesDatas();
    await this.obterDados(this.dataAtual);
  }

  private async obterDados(data: string = null): Promise<any>
  {
    return new Promise((resolve, reject) =>
    {
      this._iisLogService.obterPorParametros({
        data: data || moment().format('yyyy-MM-DD HH:mm:ss')
      }).subscribe((data: IISLog[]) =>
      {
        this.eventosOciosos = data;
        resolve(data);
      }, () =>
      {
        reject();
      });
    })
  }

  pesquisarDadosPorData(data: string)
  {
    this.obterDados(data);
  }

  obterNomeRecurso(nome: string): string
  {
    if (nome.toUpperCase().includes('SAT.V2.API'))
      return 'SAT V2 API';

    if (nome.toUpperCase().includes('PRJSATWEB'))
      return 'SAT Antigo';

    if (nome.toUpperCase().includes('PRJSATWEBAPI'))
      return 'API Smartphone';

    if (nome.toUpperCase().includes('PRJSATWEBTECNICO'))
      return 'SAT Web Técnico';

    if (nome.toUpperCase().includes('PRJSATWEBOLD'))
      return 'SAT ASP';

    if (nome.toUpperCase().includes('POS/'))
      return 'SAT POS';
  }

  obterCorRecurso(nome: string): string
  {
    if (nome.toUpperCase().includes('SAT.V2.API'))
      return 'text-amber-500';

    if (nome.toUpperCase().includes('PRJSATWEB'))
      return 'text-blue-500';

    if (nome.toUpperCase().includes('PRJSATWEBAPI'))
      return 'text-orange-500';

    if (nome.toUpperCase().includes('PRJSATWEBTECNICO'))
      return 'text-pink-500';

    if (nome.toUpperCase().includes('PRJSATWEBOLD'))
      return 'text-orange-900';

    if (nome.toUpperCase().includes('POS/'))
      return 'text-green-500';
  }

  private obterOpcoesDatas()
    {
        for (let i = 4; i >= 0; i--)
        {
            this.opcoesDatas.push({
                data: moment().add(-i, 'days').format('yyyy-MM-DD HH:mm:ss'),
                prompt: moment().add(-i, 'days').locale('pt').format('dddd').replace('-feira', '')
            });
        }
    }
}
