import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { OperadoraTelefoniaService } from 'app/core/services/operadora-telefonia.service';
import { RedeBanrisulService } from 'app/core/services/rede-banrisul.service';
import { StatusServicoService } from 'app/core/services/status-servico.service';
import { TipoComunicacaoService } from 'app/core/services/tipo-comunicacao.service';
import { OperadoraTelefonia } from 'app/core/types/chamado.types';
import { Equipamento } from 'app/core/types/equipamento.types';
import { RedeBanrisul } from 'app/core/types/rede-banrisul.types';
import { StatusServico } from 'app/core/types/status-servico.types';
import { TipoComunicacao } from 'app/core/types/tipo-comunicacao.types';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-relatorio-atendimento-pos-form',
  templateUrl: './relatorio-atendimento-pos-form.component.html'
})
export class RelatorioAtendimentoPosFormComponent implements OnInit {
  @Input() codRAT: number;
  redes: RedeBanrisul[] = [];
  tiposComunicacao: TipoComunicacao[] = [];
  operadoras: OperadoraTelefonia[] = [];
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
    private _formBuilder: FormBuilder,
  ) { }

  ngOnInit(): void {
    this.inicializarForm();
    this.registrarEmitters();
    this.obterStatusServicos();
    this.obterEquipamentos();
    this.obterRedes();
    this.obterTiposDeComunicacao();
    this.obterOperadoras();
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
      codOperadoraTelefoniaChipInstalado: [undefined]
    })
  }

  private registrarEmitters() {
    
  }

  private async obterStatusServicos(filter: string='') {
    this.statusServicos = (await this._statusServicoService
        .obterPorParametros({ 
          sortActive: 'NomeStatusServico',
          sortDirection: 'asc',
          filter: filter
        })
        .toPromise()).items;
  }

  private async obterEquipamentos() {
    this.equipamentos = (await this._equipamentoService
        .obterPorParametros({ sortActive: 'NomeEquip',sortDirection: 'asc' })
        .toPromise()).items.filter(e => e.nomeEquip.includes('POS'));
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

  salvar() {

  }

  ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
