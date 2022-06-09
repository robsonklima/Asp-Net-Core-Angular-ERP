import { EventEmitter, Injectable } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSidenav } from '@angular/material/sidenav';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { Filtro, FiltroUsuarioData, IFilterBaseCore } from '../types/filtro.types';
import { DialogSaveFilterComponent } from '../../modules/main/dialog/dialog-save-filter/dialog-save-filter.component';
import { FiltroService } from '../services/filtro.service';
import { CustomSnackbarService } from '../services/custom-snackbar.service';
import Enumerable from 'linq';
import { MatSelect } from '@angular/material/select';
import moment from 'moment';

@Injectable({
    providedIn: 'root'
})

export class FilterBase implements IFilterBaseCore {
    public filterName: string;
    public filter: any;
    public userSession: UserSession;
    public form: FormGroup;
    public sidenav: MatSidenav;
    public meusFiltros: FiltroUsuarioData[] = [];
    public selectFiltroElement: MatSelect;

    public onRefreshFilter: EventEmitter<any> = new EventEmitter();
    public onAplicar: EventEmitter<FilterBase> = new EventEmitter();
    public onLimpar: EventEmitter<FilterBase> = new EventEmitter();

    protected _dialog?: MatDialog;
    protected _filtroService?: FiltroService;
    protected _snack?: CustomSnackbarService;

    private _filtroDeUsuario: boolean = false;

    constructor(
        protected _userService: UserService,
        protected _formBuilder: FormBuilder,
        filterName: string,
    ) {
        this.filterName = filterName;
        this.userSession = JSON.parse(this._userService.userSession);
        this.filter = this._userService.obterFiltro(filterName);
        this.meusFiltros = this.getFiltros();

        this._userService.atualizaFiltrosUsuario(this.userSession.usuario).subscribe(us => {
            this.userSession.usuario.filtroUsuario = us.filtroUsuario;
            this.refreshFilter();
        });
    }

    configurarFiltrosUsuario(filterName: string, dialog: MatDialog, filtroService: FiltroService, snack: CustomSnackbarService, selectFiltroElement: MatSelect) {
        this.filterName = filterName;
        this._dialog = dialog;
        this._filtroService = filtroService;
        this._snack = snack;
        this.selectFiltroElement = selectFiltroElement;
        this._filtroDeUsuario = true;
    }

    onSelectFiltroUsuario(codFiltroUsuario: number) {
        if (codFiltroUsuario > 0) {
            this.form.patchValue(this.buscaValoresFiltro(codFiltroUsuario));
        }
    }

    obterUsuarioFiltro(): number {
        return this._userService.obterUsuarioFiltro();
    }

    registerUsuarioFiltro(codFiltroUsuario: number) {
        this._userService.registrarUsuarioFiltro(codFiltroUsuario);
    }

    buscaValoresFiltro(codFiltroUsuario: number): any[] {
        return JSON.parse(Enumerable.from(this.userSession.usuario.filtroUsuario).firstOrDefault
            (w => w.codFiltroUsuario == codFiltroUsuario).dadosJson);
    }

    getFiltros(): FiltroUsuarioData[] {
        return Enumerable.from(this.userSession.usuario.filtroUsuario).where(w => w.componenteFiltro == this.filterName).toArray();
    }

    aplicar(): void {
        if (this._filtroDeUsuario) {
            this.onAplicar.emit(this);
        }
        else {
            this.aplicarForm();
        }
    }

    aplicarForm() {
        const form: any = this.form.getRawValue();

        const filtro: Filtro = {
            nome: this.filterName,
            parametros: form
        };

        this._userService.registrarFiltro(filtro);

        if (this._filtroDeUsuario) {
            this.refreshFilter();
        }

        this.sidenav.close();
    }

    refreshFilter() { this.onRefreshFilter.emit(); }

    remover(codFiltroUsuario: number): void {
        this._filtroService.deletar(codFiltroUsuario).toPromise()
            .then(async () => {
                this._snack.exibirToast("Configuração de filtro removida!", "success");
                this._userService.atualizaFiltrosUsuario(this.userSession.usuario).subscribe(us => {
                    this.userSession.usuario.filtroUsuario = us.filtroUsuario;
                    if (codFiltroUsuario == this.obterUsuarioFiltro()) {
                        this.registerUsuarioFiltro(0);
                    }
                    this.refreshFilter();
                });
            }).catch(() => {
                this._snack.exibirToast("Erro ao remover o filtro.", "error");
            });
    }

    salvar(codFiltroUsuario?: number, nomeFiltroAtualizar?: string): void {
        let params = {
            codUsuario: this.userSession.usuario.codUsuario,
            dadosJson: JSON.stringify(this.form.getRawValue()),
            componenteFiltro: this.filterName
        }

        if (codFiltroUsuario > 0) {
            this._filtroService.atualizar({
                codFiltroUsuario: codFiltroUsuario,
                nomeFiltro: nomeFiltroAtualizar,
                ...params
            }).toPromise()
                .then(async () => {
                    this._snack.exibirToast("Configurações de filtro atualizado com sucesso!", "success");
                    this._userService.atualizaFiltrosUsuario(this.userSession.usuario).subscribe(us => {
                        this.userSession.usuario.filtroUsuario = us.filtroUsuario;
                        this.registerUsuarioFiltro(codFiltroUsuario);
                        this.aplicarForm();
                    });
                }).catch(() => {
                    this._snack.exibirToast("Erro ao atualizar o filtro.", "error");
                });
        } else {
            const dialogRef = this._dialog.open(DialogSaveFilterComponent);
            dialogRef.afterClosed().subscribe(async (data: any) => {
                this._filtroService.criar({
                    nomeFiltro: data.nomeFiltro,
                    ...params
                }).toPromise()
                    .then(async () => {
                        this._snack.exibirToast("Configurações de filtro criado com sucesso!", "success");
                        this._userService.atualizaFiltrosUsuario(this.userSession.usuario).subscribe(us => {
                            this.userSession.usuario.filtroUsuario = us.filtroUsuario;
                            let novoCodigo = Enumerable.from(us.filtroUsuario).orderByDescending(ord => ord.codFiltroUsuario).firstOrDefault().codFiltroUsuario;
                            this.registerUsuarioFiltro(novoCodigo);
                            this.aplicarForm();
                        });
                    }).catch(() => {
                        this._snack.exibirToast("Erro ao criar o filtro.", "error");
                    });
            });
        }
    }

    limpar(): void {
        if (this._filtroDeUsuario) {
            this.onLimpar.emit(this);
        }

        this.form.reset();

        if (this._filtroDeUsuario) {
            this.aplicarForm();
            this.registerUsuarioFiltro(0);
        } else {
            this.aplicarForm();
        }

        this.sidenav.close();

        if (this.userSession?.usuario?.codFilial) {
            this.form.controls['codFiliais'].setValue([this.userSession.usuario.codFilial]);
            this.form.controls['codFiliais'].disable();
        }
    }

    selectAll(select: AbstractControl, values, propertyName) {
        if (select.value[0] == 0 && propertyName != '')
            select.patchValue([...values.map(item => item[`${propertyName}`]), 0]);
        else if (select.value[0] == 0 && propertyName == '')
            select.patchValue([...values.map(item => item), 0]);
        else
            select.patchValue([]);
    }
}
