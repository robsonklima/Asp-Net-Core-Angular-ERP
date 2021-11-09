import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Filtro } from 'app/core/types/filtro.types';
import moment from 'moment';

@Component({
  selector: 'app-media-global-atendimento-tecnico',
  templateUrl: './media-global-atendimento-tecnico.component.html',
  styleUrls: ['./media-global-atendimento-tecnico.component.css'
  ]
})

export class MediaGlobalAtendimentoTecnicoComponent implements OnInit {
  @Input() filtro: Filtro;
  public mediaGlobalAtendimentoTecnicosModel: MediaGlobalAtendimentoTecnicosModel = new MediaGlobalAtendimentoTecnicosModel();
  public loading: boolean = true;

  constructor(private _cdr: ChangeDetectorRef,
    private _tecnicoService: TecnicoService,
    private _ordemServicoService: OrdemServicoService) { }

  ngOnInit(): void {
    this.obterDados();
  }

  async obterDados() {
    this.loading = true;


    let listaTecnicos = (await this._tecnicoService
      .obterPorParametros({})
      .toPromise()).items;

    for (let tecnico of listaTecnicos.filter(m => m.indAtivo && m.usuario?.indAtivo)) {

      let listaOSTecnico = await this._ordemServicoService.obterPorParametros({
        codTecnico: tecnico.codTecnico,
        dataAberturaInicio: '2021-08-01', //moment().startOf('month').format('YYYY-MM-DD hh:mm'),
        dataAberturaFim: moment().endOf('month').format('YYYY-MM-DD hh:mm'),
      }).toPromise();

      if (listaOSTecnico.items?.length > 0) {
        this.mediaGlobalAtendimentoTecnicosModel.qntTodasIntervencoes += listaOSTecnico.items.filter(os => os.codStatusServico != 2).length;
        this.mediaGlobalAtendimentoTecnicosModel.qntCorretivos += listaOSTecnico.items.filter(os => os.codTipoIntervencao == 2).length;
        this.mediaGlobalAtendimentoTecnicosModel.qntPreventivos += listaOSTecnico.items.filter(os => os.codTipoIntervencao == 8).length;
        this.mediaGlobalAtendimentoTecnicosModel.qntInstalacoes += listaOSTecnico.items.filter(os => os.codTipoIntervencao == 4).length;
        this.mediaGlobalAtendimentoTecnicosModel.qntAltEngenharia += listaOSTecnico.items.filter(os => os.codTipoIntervencao == 1).length;
      }
    }

    this.loading = false;
    this._cdr.detectChanges();
  }
}

export class MediaGlobalAtendimentoTecnicosModel {
  qntTodasIntervencoes: number = 0;
  qntCorretivos: number = 0;
  qntPreventivos: number = 0;
  qntInstalacoes: number = 0;
  qntAltEngenharia: number = 0;
}