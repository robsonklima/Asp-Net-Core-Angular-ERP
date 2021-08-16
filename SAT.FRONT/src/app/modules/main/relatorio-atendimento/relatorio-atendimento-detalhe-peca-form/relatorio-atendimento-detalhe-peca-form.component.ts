import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { PecaService } from 'app/core/services/peca.service';
import { Peca, PecaParameters } from 'app/core/types/peca.types';
import { RelatorioAtendimentoDetalhePeca } from 'app/core/types/relatorio-atendimento-detalhe-peca';
import { UsuarioSessionData } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { RelatorioAtendimentoFormComponent } from '../relatorio-atendimento-form/relatorio-atendimento-form.component';

@Component({
  selector: 'app-relatorio-atendimento-detalhe-peca-form',
  templateUrl: './relatorio-atendimento-detalhe-peca-form.component.html'
})
export class RelatorioAtendimentoDetalhePecaFormComponent implements OnInit {
  pecas: Peca[];
  form: FormGroup;
  sessionData: UsuarioSessionData;
  pecaFilterCtrl: FormControl = new FormControl();
  raDetalhePeca: RelatorioAtendimentoDetalhePeca;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _pecaService: PecaService,
    private _userService: UserService,
    public dialogRef: MatDialogRef<RelatorioAtendimentoFormComponent>
  ) {
    this.sessionData = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.obterPecas();
    this.inicializarForm();
    this.registrarEmitters();
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      codPeca: [undefined, [Validators.required]],
      qtdePecas: [undefined, [Validators.required]]
    });
  }

  private registrarEmitters(): void {
    this.pecaFilterCtrl.valueChanges
      .pipe(
        takeUntil(this._onDestroy),
        debounceTime(700),
        distinctUntilChanged()
      )
      .subscribe(() => {
        this.obterPecas(this.pecaFilterCtrl.value);
      });
  }

  async obterPecas(filter: string='') {
    const params: PecaParameters = {
      sortDirection: 'asc',
      sortActive: 'nomePeca',
      pageSize: 50,
      filter: filter
    }

    const data = await this._pecaService
      .obterPorParametros(params)
      .toPromise();

    this.pecas = data.pecas;
  }

  inserir(): void {
    let form = this.form.getRawValue();
    form.codUsuarioCad = this.sessionData.usuario.codUsuario;
    form.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');
    this.dialogRef.close(form);
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
