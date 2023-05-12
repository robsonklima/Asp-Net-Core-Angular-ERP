import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { MotivoComunicacaoService } from 'app/core/services/motivo-comunicacao.service';
import { OperadoraTelefoniaService } from 'app/core/services/operadora-telefonia.service';
import { RedeBanrisulService } from 'app/core/services/rede-banrisul.service';
import { RelatorioAtendimentoPOSService } from 'app/core/services/relatorio-atendimento-pos.service';
import { StatusServicoService } from 'app/core/services/status-servico.service';
import { TipoComunicacaoService } from 'app/core/services/tipo-comunicacao.service';
import { DefeitoPOS, OperadoraTelefonia } from 'app/core/types/chamado.types';
import { Equipamento } from 'app/core/types/equipamento.types';
import { MotivoComunicacao } from 'app/core/types/motivo-comunicacao.types';
import { RedeBanrisul } from 'app/core/types/rede-banrisul.types';
import { RelatorioAtendimentoPOS } from 'app/core/types/relatorio-atendimento-pos.types';
import { StatusServico } from 'app/core/types/status-servico.types';
import { TipoComunicacao } from 'app/core/types/tipo-comunicacao.types';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-relatorio-atendimento-pos-form',
  templateUrl: './relatorio-atendimento-pos-form.component.html'
})
export class RelatorioAtendimentoPosFormComponent implements OnInit {
  @Input() codRAT: number;
  rat: RelatorioAtendimentoPOS;
  redes: RedeBanrisul[] = [];
  tiposComunicacao: TipoComunicacao[] = [];
  motivosComunicacao: MotivoComunicacao[] = [];
  operadoras: OperadoraTelefonia[] = [];
  defeitos: DefeitoPOS[] = [];
  loading: boolean;
  form: FormGroup;
  statusServicos: StatusServico[] = [];
  equipamentos: Equipamento[] = [];
  protected _onDestroy = new Subject<void>();

  constructor(
    private _statusServicoService: StatusServicoService,
    private _equipamentoService: EquipamentoService,
    private _redeBanrisulService: RedeBanrisulService,
    private _tipoComunicacaoService: TipoComunicacaoService,
    private _operadoraTelefoniaService: OperadoraTelefoniaService,
    private _motivoComunicacaoService: MotivoComunicacaoService,
    //private _defeitoPOSService: DefeitoPOSSer
    private _relatorioAtendimentoPOSService: RelatorioAtendimentoPOSService,
    private _formBuilder: FormBuilder,
  ) { }

  async ngOnInit() {
    this.inicializarForm();
    this.registrarEmitters();

    await this.obterRATPOS();
    this.obterStatusServicos();
    this.obterEquipamentos();
    this.obterRedes();
    this.obterTiposDeComunicacao();
    this.obterOperadoras();
    this.obterMotivosComunicacao();
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
      codStatusServico: [undefined],
      numSerieRet: [undefined],
      codEquipRet: [undefined],
      numSerieInst: [undefined],
      codEquipInst: [undefined],
      codRedeBanrisul: [undefined],
      codTipoComunicacao: [undefined],
      codMotivoComunicacao: [undefined],
      numeroChipInstalado: [undefined],
      numeroChipRetirado: [undefined],
      codOperadoraTelefoniaChipRetirado: [undefined],
      codOperadoraTelefoniaChipInstalado: [undefined],
      indSmartphone: [0],
      ObsMotivoCancelamento: [undefined],
      ObsMotivoComunicacao: [undefined]
    })
  }

  private registrarEmitters() {
    
  }

  private async obterRATPOS() {
    this.rat = (await this._relatorioAtendimentoPOSService
        .obterPorParametros({ 
          sortActive: 'CodRAT',
          sortDirection: 'desc'
        })
        .toPromise()).items?.shift();

    console.log(this.rat, this.form.controls);
    

    this.form.patchValue(this.rat);
  }

  private async obterStatusServicos() {
    this.statusServicos = (await this._statusServicoService
        .obterPorParametros({ 
          sortActive: 'NomeStatusServico',
          sortDirection: 'asc'
        })
        .toPromise()).items;
  }

  private async obterEquipamentos() {
    this.equipamentos = (await this._equipamentoService
        .obterPorParametros({ sortActive: 'NomeEquip',sortDirection: 'asc' })
        .toPromise()).items.filter(e => e.nomeEquip.includes('POS') || e.nomeEquip.includes('PIN'));
  }

  private async obterRedes() {
    this.redes = (await this._redeBanrisulService
        .obterPorParametros({ sortActive: 'Rede',sortDirection: 'asc' })
        .toPromise()).items;
  }

  private async obterTiposDeComunicacao() {
    this.tiposComunicacao = (await this._tipoComunicacaoService
        .obterPorParametros({ sortActive: 'Tipo',sortDirection: 'asc' })
        .toPromise()).items;
  }

  private async obterOperadoras() {
    this.operadoras = (await this._operadoraTelefoniaService
        .obterPorParametros({ sortActive: 'NomeOperadoraTelefonia',sortDirection: 'asc' })
        .toPromise()).items;
  }

  private async obterMotivosComunicacao() {
    this.motivosComunicacao = (await this._motivoComunicacaoService
      .obterPorParametros({ sortActive: 'Motivo',sortDirection: 'asc' })
      .toPromise()).items;
  }

  private async obterDefeitos() {
    // this.motivosComunicacao = (await this._motivoComunicacaoService
    //   .obterPorParametros({ sortActive: 'Motivo',sortDirection: 'asc' })
    //   .toPromise()).items;
  }

  salvar() {
    this.rat != null ? this.atualizar() : this.inserir();
  }

  private async inserir() {
    console.log('inserir');
  } 

  private async atualizar() {
    console.log('atualizar');
  }

  ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
