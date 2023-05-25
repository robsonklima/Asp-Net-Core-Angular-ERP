import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Peca, PecaParameters } from 'app/core/types/peca.types';
import { PecaService } from 'app/core/services/peca.service';
import { Subject } from 'rxjs';
import { takeUntil, debounceTime, distinctUntilChanged, tap, map, delay, filter } from 'rxjs/operators';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import moment from 'moment';
import _ from 'lodash';
import { Router } from '@angular/router';
import { ORItem } from 'app/core/types/or-item.types';
import { ORItemInsumoService } from 'app/core/services/or-item-insumo.service';
import { LaboratorioProcessoReparoInsumoComponent } from '../laboratorio-processo-reparo-insumo.component';
import { ORItemInsumo } from 'app/core/types/or-item-insumo.types';
import { statusConst } from 'app/core/types/status-types';

@Component({
    selector: 'app-laboratorio-insumo-dialog',
    templateUrl: './laboratorio-insumo-dialog.component.html'
})
export class LaboratorioProcessoReparoInsumoDialogComponent implements OnInit {
    orItem: ORItem;
    pecas: Peca[] = [];
    userSession: UserSession;
    public form: FormGroup;
    loading: boolean = true;
    pecaFilterCtrl: FormControl = new FormControl();
    protected _onDestroy = new Subject<void>();

    constructor(
        @Inject(MAT_DIALOG_DATA) data: any,
        private _formBuilder: FormBuilder,
        public _dialogRef: MatDialogRef<LaboratorioProcessoReparoInsumoComponent>,
        private _pecaService: PecaService,
        private _orItemInsumoService: ORItemInsumoService,
        private _userService: UserService,
        private _snack: CustomSnackbarService,
        private _router: Router,
        private dialogRef: MatDialogRef<LaboratorioProcessoReparoInsumoComponent>
    ) {
        this.orItem = data?.orItem;
        this.userSession = JSON.parse(this._userService.userSession);
    }

    async ngOnInit() {
        this.inicializarForm();
        this.obterPecas();
        this.registrarEmitters();
        this.loading = false;
    }

    private async obterPecas(filtro: string = ''): Promise<Peca[]> {
        return new Promise(async (resolve, reject) => {
            let params: PecaParameters = {
                filter: filtro,
                sortActive: 'nomePeca',
                sortDirection: 'asc',
                pageSize: 100
            };

            const data = await this._pecaService
                .obterPorParametros(params)
                .toPromise();

            resolve(data.items);
        });
    }

    private inicializarForm() {
        this.form = this._formBuilder.group({
            codPeca: [undefined],
            qtdUtilizada: [undefined]
        });
    }

    private registrarEmitters() {
        this.pecaFilterCtrl.valueChanges
            .pipe(
                filter(text => text != ''),
                tap(() => { }),
                debounceTime(700),
                distinctUntilChanged(),
                map(async query => {
                    return (await this.obterPecas(query));
                }),
                delay(500),
                takeUntil(this._onDestroy)
            )
            .subscribe(async data => {
                this.pecas = await data;
            });
    }

    cancelar() {
        this._dialogRef.close();
    }

    async salvar() {
        const form = this.form.getRawValue();

        let obj: ORItemInsumo = {
            ...{
                codORItem: this.orItem.codORItem,
                dataHoraOritem: moment().format('YYYY-MM-DD HH:mm:ss'),
                codOR: this.orItem.codOR,
                codPeca: form.codPeca,
                codStatus: this.orItem.codStatus,
                quantidade: +form.qtdUtilizada,
                indConfLog: 0,
                indConfLab: 0,
                indAtivo: statusConst.ATIVO,
                codUsuarioCad: this.userSession.usuario.codUsuario,
                dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
                codStatusPendente: 0
            }
        };

        this._orItemInsumoService.criar(obj).subscribe(insumo => {
        	this._snack.exibirToast("Registro adicionado com sucesso!", "success");
            this.dialogRef.close(true);
        });
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}

