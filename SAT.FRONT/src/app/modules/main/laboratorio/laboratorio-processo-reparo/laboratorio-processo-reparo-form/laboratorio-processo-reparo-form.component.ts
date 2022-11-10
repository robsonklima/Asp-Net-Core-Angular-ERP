import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BancadaLaboratorioService } from 'app/core/services/bancada-laboratorio.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ORCheckListService } from 'app/core/services/or-checklist.service';
import { ORItemService } from 'app/core/services/or-item.service';
import { ORTempoReparoService } from 'app/core/services/or-tempo-reparo.service';
import { ORService } from 'app/core/services/or.service';
import { ViewLaboratorioTecnicoBancada } from 'app/core/types/bancada-laboratorio.types';
import { ORCheckList, ORCheckListParameters } from 'app/core/types/or-checklist.types';
import { ORItem } from 'app/core/types/or-item.types';
import { ORTempoReparo } from 'app/core/types/or-tempo-reparo.types';
import { OR } from 'app/core/types/OR.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import _ from 'lodash';
import moment from 'moment';

@Component({
  selector: 'app-laboratorio-processo-reparo-form',
  templateUrl: './laboratorio-processo-reparo-form.component.html'
})
export class LaboratorioProcessoReparoFormComponent implements OnInit {
  item: ORItem;
  or: OR;
  codORItem: number;
  loading: boolean = true;
  tecnicoBancada: ViewLaboratorioTecnicoBancada;
  userSession: UserSession;
  tempoReparo: ORTempoReparo;
  orCheckList: ORCheckList;
  form: FormGroup;

  constructor(
    private _bancadaLaboratorioService: BancadaLaboratorioService,
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _orService: ORService,
    private _snack: CustomSnackbarService,
    private _orItemService: ORItemService,
    private _orTempoReparoService: ORTempoReparoService,
    private _orCheckList: ORCheckListService
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codORItem = +this._route.snapshot.paramMap.get('codORItem');
    this.criarForm();

    this.item = await this._orItemService
      .obterPorCodigo(this.codORItem)
      .toPromise();

    this.tempoReparo = _.last(this.item.temposReparo);

    this.form.patchValue({
      codMagnus: this.item?.peca?.codMagnus,
      descricao: this.item?.peca?.nomePeca,
      numSerie: this.item?.numSerie,
      usuarioTecnico: this.item?.usuarioTecnico?.nomeUsuario,
      status: this.item?.statusOR?.descStatus,

    });

    this.or = await this._orService
      .obterPorCodigo(this.item.codOR)
      .toPromise();

    const tecnicosBancada = await this._bancadaLaboratorioService
      .obterPorView({ codUsuario: this.userSession.usuario.codUsuario })
      .toPromise();

    this.tecnicoBancada = tecnicosBancada.shift();
    this.loading = false;
  }

  public obterEvolucaoReparo(tecnico: ViewLaboratorioTecnicoBancada) {
    if (!tecnico.tempoEmReparo || !tecnico.tempoEmReparo || tecnico.tempoReparoPeca == '00:00')
      return 0;

    const tempoEmReparo = moment.duration(tecnico.tempoEmReparo).asMinutes();
    const tempoReparoPeca = moment.duration(tecnico.tempoReparoPeca).asMinutes();
    const percentualEvolucao = (tempoEmReparo / tempoReparoPeca * 100).toFixed(2)
    return +percentualEvolucao <= 100 ? percentualEvolucao : 100;
  }

  criarForm() {
    this.form = this._formBuilder.group({
      codMagnus: [
        {
          value: '',
          disabled: true
        },
      ],
      descricao: [
        {
          value: '',
          disabled: true
        },
      ],
      numSerie: [
        {
          value: '',
          disabled: false
        },
      ],
      usuarioTecnico: [
        {
          value: '',
          disabled: true
        },
      ],
      status: [
        {
          value: '',
          disabled: true
        },
      ],
      nivel: [undefined]
    });
  }

  toggleStatusReparo() {
    if (!this.tempoReparo?.indAtivo) {
      this._orTempoReparoService
        .criar({
          codORItem: this.item.codORItem,
          codUsuarioCad: this.userSession.usuario.codUsuario,
          dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
          dataHoraInicio: moment().format('YYYY-MM-DD HH:mm:ss'),
          codTecnico: this.userSession.usuario.codUsuario,
          dataHoraFim: null,
          indAtivo: 1
        })
        .subscribe((tr) => {
          this.tempoReparo = tr;
          this._snack.exibirToast('Reparo iniciado', 'success');
        },
        () => {
          this._snack.exibirToast('Erro ao finalizar reparo', 'error');
        });
    } else {
      this._orTempoReparoService
        .atualizar({
          ...this.tempoReparo,
          ...{
            indAtivo: 0,
            dataHoraFim: moment().format('YYYY-MM-DD HH:mm:ss')
          }
        })
        .subscribe((tr) => {
          this.tempoReparo = tr;
          this._snack.exibirToast('Reparo finalizado', 'success');
        },
        () => {
          this._snack.exibirToast('Erro ao finalizar reparo', 'error');
        });
    }
  }

  salvar() {
    
  }
}
