import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { PecaService } from 'app/core/services/peca.service';
import { Peca, PecaParameters } from 'app/core/types/peca.types';
import { RelatorioAtendimentoDetalhePeca } from 'app/core/types/relatorio-atendimento-detalhe-peca.type';
import { UsuarioSessionData } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { debounceTime, delay, distinctUntilChanged, filter, map, takeUntil } from 'rxjs/operators';
import { RelatorioAtendimentoFormComponent } from '../relatorio-atendimento-form/relatorio-atendimento-form.component';

@Component({
  selector: 'app-relatorio-atendimento-detalhe-peca-form',
  templateUrl: './relatorio-atendimento-detalhe-peca-form.component.html'
})
export class RelatorioAtendimentoDetalhePecaFormComponent implements OnInit {
  pecas: Peca[] = [];
  pecasFiltro: FormControl = new FormControl();
  form: FormGroup;
  sessionData: UsuarioSessionData;
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

  async ngOnInit() {
    this.inicializarForm();

    this.pecas = (await this._pecaService.obterPorParametros({
      sortActive: 'nomePeca',
      sortDirection: 'asc',
      pageSize: 100,
    }).toPromise()).items;

    this.pecasFiltro.valueChanges
      .pipe(
        filter(query => !!query),
        takeUntil(this._onDestroy),
        debounceTime(700),
        map(async query => {
          const data = await this._pecaService.obterPorParametros({
            sortActive: 'nomePeca',
            sortDirection: 'asc',
            filter: query,
            pageSize: 100
          }).toPromise();

          return data.items.slice();
        }),
        delay(500),
        takeUntil(this._onDestroy)
      )
      .subscribe(async data => {
        this.pecas = await data;
      });
  }

  private inicializarForm(): void {
    this.form = this._formBuilder.group({
      codPeca: [undefined, [Validators.required]],
      qtdePecas: [undefined, [Validators.required]]
    });
  }

  inserir(): void {
    let form = this.form.getRawValue();
    form.codUsuarioCad = this.sessionData.usuario.codUsuario;
    form.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');
    form.peca = this.pecas.filter(p => p.codPeca == form.codPeca).shift();
    this.dialogRef.close(form);
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
