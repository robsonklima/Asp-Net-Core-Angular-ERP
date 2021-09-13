import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { PecaService } from 'app/core/services/peca.service';
import { Peca, PecaParameters, PecaStatus } from 'app/core/types/peca.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Location } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { Subject } from 'rxjs';
import { debounceTime, delay, filter, map, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-peca-form',
  templateUrl: './peca-form.component.html'
})
export class PecaFormComponent implements OnInit 
{

  public codPeca: number;
  public isAddMode: boolean;
  public pecaStatus: any[] = [];
  public pecasSubstituicao: Peca[] = [];
  public pecasSubstituicaoFiltro: FormControl = new FormControl();
  public _onDestroy = new Subject<void>();
  public form: FormGroup;
  private userSession: UsuarioSessao;
  private peca: Peca;

  constructor(
    private _router: Router,
    private _formBuilder: FormBuilder,
    private _pecaService: PecaService,
    private _route: ActivatedRoute,
    private _userService: UserService,
    private _location: Location,
    private _dialog: MatDialog,
    private _snack: CustomSnackbarService) 
      { this.userSession = JSON.parse(this._userService.userSession) }

  async ngOnInit() 
  {
    this.codPeca = +this._route.snapshot.paramMap.get('codPeca');
    this.isAddMode = !this.codPeca;

    this.inicializarForm();
    this.obterPecaSubstituicao();
    this.obterStatus();
    
    if (!this.isAddMode) 
    {
      const data = await this._pecaService
        .obterPorCodigo(this.codPeca)
        .toPromise();

      this.form.patchValue(data);
      this.peca = data;
    }
  }

  private inicializarForm(): void 
  {
    this.form = this._formBuilder.group({
      codPeca: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      codMagnus: [undefined, Validators.required],
      nomePeca: [undefined, Validators.required],
      valCusto: [undefined, Validators.required],
      valCustoDolar: [undefined, Validators.required],
      valCustoEuro: [undefined],
      valPeca: [undefined, Validators.required],
      valPecaDolar: [undefined, Validators.required],
      valPecaEuro: [undefined],
      valPecaAssistencia: [undefined],
      valIpiassistencia: [undefined],
      valIPI: [undefined, Validators.required],
      qtdMinimaVenda: [undefined, Validators.required],
      ncm: [undefined, Validators.required],
      pecaFamilia: [undefined],
      codPecaSubstituicao: [undefined],
      codPecaStatus: [undefined, Validators.required]
    });
  }

  private async obterStatus(): Promise<void>
  {
    Object.keys(PecaStatus)
    .filter((e) => isNaN(Number(e)))
    .forEach((tr, i) => 
      this.pecaStatus.push({ codPecaStatus: i+1, label: tr }));
  }

  private async obterPecaSubstituicao() 
  {
    this.pecasSubstituicao = (await this._pecaService.obterPorParametros({
      sortActive: 'nomePeca',
      sortDirection: 'asc',
      pageSize: 100,
    }).toPromise()).items;

    this.pecasSubstituicaoFiltro
        .valueChanges
        .pipe(filter(query => !!query),
          takeUntil(this._onDestroy),
          debounceTime(700),
          map(async query => (await this._pecaService.obterPorParametros({
            sortActive: 'nomePeca',
            sortDirection: 'asc',
            filter: query,
            pageSize: 100
          }).toPromise()).items.slice()),
          delay(500),
          takeUntil(this._onDestroy))
        .subscribe(async data => 
          this.pecasSubstituicao = await data);
  }

  public salvar(): void { this.isAddMode ? this.criar() : this.atualizar(); }

  public atualizar(): void 
  {
    var peca = 
    {
      ...this.peca,
      ...this.form.getRawValue(),
      ...
      {
        dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioManut: this.userSession.usuario.codUsuario
      }
    };

    this._pecaService.atualizar(peca).subscribe(() => 
    {
      this._snack.exibirToast(`Peça ${peca.nomePeca} atualizada com sucesso!`, "success");
      this._location.back();
    });
  }

  public criar(): void 
  {
    var peca: Peca = 
    {
      ...this.peca,
      ...this.form.getRawValue(),
      ...{
        dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        codUsuarioCad: this.userSession.usuario.codUsuario,
        indObrigRastreabilidade: 1,
        indValorFixo: 1
      }
    };

    this._pecaService.criar(peca).subscribe(() => 
    {
      this._snack.exibirToast(`Peça ${peca.nomePeca} criada com sucesso!`, "success");
      this._location.back();
    });
  }

  public remover(): void
  {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent,
    {
      data: 
      {
        titulo: 'Confirmação',
        message: `Deseja remover a peça ${this.peca.nomePeca}?`,
        buttonText: 
        {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) => 
    {
      if (!confirmacao) return;
      
      this._pecaService.deletar(this.codPeca).subscribe(() => 
      {
        this._snack.exibirToast(`${this.peca.nomePeca} removida com sucesso!`, 'success');
        this._router.navigate(['/peca']);
      }, 
      e => 
      {
        console.log(e);
        this._snack.exibirToast('Erro ao remover peça', 'error');
      })
    });
  }

  ngOnDestroy() 
  {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}