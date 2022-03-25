import { ActivatedRoute } from '@angular/router';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { forkJoin, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, first, takeUntil } from 'rxjs/operators';
import { Location } from '@angular/common';
import { UserService } from 'app/core/user/user.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { CausaService } from 'app/core/services/causa.service';
import { Causa } from 'app/core/types/causa.types';
import Enumerable from 'linq';
import { EquipamentoModulo } from 'app/core/types/equipamento-modulo.types';
import { EquipamentoModuloService } from 'app/core/services/equipamento-modulo.service';
import moment from 'moment';

@Component({
  selector: 'app-equipamento-modulo-form',
  templateUrl: './equipamento-modulo-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class EquipamentoModuloFormComponent implements OnInit, OnDestroy {
  usuarioSessao: UsuarioSessao;
  codEquip: number;
  nomeEquip: string;
  form: FormGroup;
  equipamentoModulo: EquipamentoModulo;
  public isLoading: Boolean = true;
  public causas: Causa[] = [];
  public equipamentoCausas: Causa[] = [];

  causasFiltro: FormControl = new FormControl();

  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _equipamentoModuloService: EquipamentoModuloService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _route: ActivatedRoute,
    private _causaService: CausaService
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codEquip = +this._route.snapshot.paramMap.get('codEquip');

    this.obterCausas().then(async () => {
      this.form = this._formBuilder.group({
        codEquip: [
          {
            value: undefined,
            disabled: true
          },
        ],
        codConfigEquipModulos: [undefined],
        codECausa: [undefined],
        codUsuarioManut: [undefined],
        dataHoraManut: [undefined],
        codUsuarioCad: [undefined],
        dataHoraCad: [undefined],
        indAtivo: [undefined]
      });

      this.causasFiltro.valueChanges
        .pipe(
          takeUntil(this._onDestroy),
          debounceTime(500),
          distinctUntilChanged()
        )
        .subscribe(async (filtro) => {
          this.obterCausas(filtro);
        });

      this._equipamentoModuloService.obterPorParametros({ codEquip: this.codEquip })
        .pipe(first())
        .subscribe(async apiData => {
          this.form.controls["codECausa"].setValue(Enumerable.from(apiData.items).where(s => s.indAtivo == 1).select(s => s.codECausa).toArray());
          this.equipamentoCausas = Enumerable.from(apiData.items).select(s => s.causa).toArray();
          this.nomeEquip = Enumerable.from(apiData.items).firstOrDefault().equipamento?.nomeEquip;
        });

      this.isLoading = false;
    })
  }

  private async obterCausas(filtro: string = '') {
    const data = await this._causaService.obterPorParametros({
      indAtivo: 1,
      sortActive: 'codECausa',
      sortDirection: 'asc',
      filter: filtro,
      apenasModulos: 1
    }).toPromise();
    this.causas = data.items;
  }

  salvar(): void {
    this.isLoading = true;
    const form: any = this.form.getRawValue();
    let eventos: any[] = [];

    let lista = Enumerable.from(this.equipamentoCausas).select(s => s.codECausa).toArray();
    lista.push(...form.codECausa);

    for (let causa of Enumerable.from(lista).distinct().toArray()) {
      let parametros = {
        ...form,
        codEquip: this.codEquip,
        codECausa: causa,
        indAtivo: form.codECausa.includes(causa) ? 1 : 0,
        codUsuarioManut: this.usuarioSessao.usuario?.codUsuario,
        dataHoraManut: moment().format('yyyy-MM-DD HH:mm:ss'),
        codConfigEquipModulos: 0
      };
      eventos.push(this._equipamentoModuloService.atualizar(parametros));
    }

    forkJoin({
      ...eventos
    }).subscribe(resultado => {
      this.isLoading = false;
      this._snack.exibirToast("Registro atualizado com sucesso!", "success");
      this._location.back();
    });
  }

  ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
