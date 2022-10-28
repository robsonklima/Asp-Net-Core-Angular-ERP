import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { ORCheckListItemService } from 'app/core/services/or-checklist-item.service';
import { ORCheckListService } from 'app/core/services/or-checklist.service';
import { PecaService } from 'app/core/services/peca.service';
import { ORCheckListItem } from 'app/core/types/or-checklist-item.types';
import { ORCheckList } from 'app/core/types/or-checklist.types';
import { Peca, PecaData, PecaParameters } from 'app/core/types/peca.types';
import { statusConst } from 'app/core/types/status-types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { Subject } from 'rxjs';
import { debounceTime, delay, distinctUntilChanged, first, map, takeUntil, tap } from 'rxjs/operators';
import { LaboratorioChecklistItemFormDialogComponent } from '../laboratorio-checklist-item-form-dialog/laboratorio-checklist-item-form-dialog.component';

@Component({
  selector: 'app-laboratorio-checklist-form',
  templateUrl: './laboratorio-checklist-form.component.html',
  styles: [`
    .list-grid-checklist-itens-form {
        grid-template-columns: 66px 150px 150px 150px auto 88px 88px;

        @screen sm {
          grid-template-columns: 66px 150px 150px 150px auto 88px 88px;
        }
      
        @screen md {
          grid-template-columns: 66px 150px 150px 150px auto 88px 88px;
        }
      
        @screen lg {
          grid-template-columns: 66px 150px 150px 150px auto 88px 88px;
        }
    }
  `],
  encapsulation: ViewEncapsulation.None
})
export class LaboratorioChecklistFormComponent implements OnInit, OnDestroy {
  usuarioSessao: UsuarioSessao;
  codORCheckList: number;
  isAddMode: boolean;
  formCheckList: FormGroup;
  checkList: ORCheckList;
  pecas: Peca[] = [];
  pecaFilterCtrl: FormControl = new FormControl();
  isLoading: boolean;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _orChecklistService: ORCheckListService,
    private _orChecklistItemService: ORCheckListItemService,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _pecaService: PecaService,
    private _route: ActivatedRoute,
    private _dialog: MatDialog
  ) {
    this.usuarioSessao = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.codORCheckList = +this._route.snapshot.paramMap.get('codORCheckList');
    this.isAddMode = !this.codORCheckList;
    this.inicializarForms();
    this.registrarEmitters();

    if (!this.isAddMode) {
      this._orChecklistService.obterPorCodigo(this.codORCheckList)
        .pipe(first())
        .subscribe(async data => {
          this.checkList = data;
          this.formCheckList.patchValue(this.checkList);
          this.pecas = (await this.obterPecas(this.checkList?.peca?.codPeca))?.items;
        }, () => {
          this._snack.exibirToast('Erro ao carregar o checklist', 'error');
        });
    }
  }

  private inicializarForms() {
    this.formCheckList = this._formBuilder.group({
      codPeca: [null, [Validators.required]],
      tempoReparo: [null, [Validators.required]],
      descricao: [null, [Validators.required]],
      indAtivo: [statusConst.ATIVO, [Validators.required]],
    });
  }

  async obterPecas(filtro): Promise<PecaData> {
    if (!filtro) return;

		let params: PecaParameters = {
			sortActive: 'nomePeca',
			sortDirection: 'asc',
      pageSize: 120,
      filter: filtro
		};

		return await this._pecaService
			.obterPorParametros(params)
			.toPromise();
	}

  private registrarEmitters() {
    this.pecaFilterCtrl.valueChanges
			.pipe(
				tap(() => this.isLoading = true),
				debounceTime(700),
        distinctUntilChanged(),
        delay(500),
				map(async query => {
          return (await this.obterPecas(query))?.items?.slice();
				}),
				takeUntil(this._onDestroy)
			)
			.subscribe(async data => {
        const registros = await data;
        if (registros) this.pecas = registros;
        this.isLoading = false;
			});
  }

  salvarCheckList(): void {
    const form: any = this.formCheckList.getRawValue();

    let obj = {
      ...this.checkList,
      ...form,
      ...{
        indAtivo: +form.indAtivo,
      }
    };

    if (this.isAddMode) {
      this._orChecklistService.criar(obj).subscribe(() => {
        this._snack.exibirToast("Registro criado com sucesso!", "success");
        this._location.back();
      })
    } else {
      this._orChecklistService.atualizar(obj).subscribe(() => {
        this._snack.exibirToast("Registro atualizado com sucesso!", "success");
        this._location.back();
      })
    }
  }

  abrirCheckListItemForm(item: ORCheckListItem=null) {
    const dialogRef = this._dialog.open(LaboratorioChecklistItemFormDialogComponent, {
      data: {
        orCheckListItem: item,
        codORCheckList: this.checkList.codORCheckList
      },
      width: '680px'
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
      if (confirmacao) {
        this.ngOnInit();
      }
    });
  }

  excluirCheckListItem(codigo: number) {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: 'Deseja excluir esta atividade?',
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
			if (confirmacao) {
				this._orChecklistItemService.deletar(codigo).subscribe(() => {
          this._snack.exibirToast("Atividade excluída com sucesso!", "success");
          this.ngOnInit();
        });
			}
		});
  }

  ngOnDestroy(): void {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}

