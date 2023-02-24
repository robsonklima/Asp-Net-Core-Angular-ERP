import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { statusConst } from 'app/core/types/status-types';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, first, takeUntil } from 'rxjs/operators';
import { Location } from '@angular/common';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { OSBancada } from 'app/core/types/os-bancada.types';
import { OSBancadaService } from 'app/core/services/os-bancada.service';
import { ClienteBancada, ClienteBancadaParameters } from 'app/core/types/cliente-bancada.types';
import { ClienteBancadaService } from 'app/core/services/cliente-bancada.service';
import { MatDialog } from '@angular/material/dialog';
import { LaboratorioOSBancadaPecasDialogComponent } from '../laboratorio-os-bancada-pecas-dialog/laboratorio-os-bancada-pecas-dialog.component';

@Component({
    selector: 'app-laboratorio-os-bancada-form',
    templateUrl: './laboratorio-os-bancada-form.component.html'
})
export class LaboratorioOSBancadaFormComponent implements OnInit, OnDestroy {

    protected _onDestroy = new Subject<void>();
    public osBancada: OSBancada;
    public clienteBancada: ClienteBancada;
    public clientes: ClienteBancada[] = [];
    public loading: boolean = true;
    public codOsbancada: number;
    public isAddMode: boolean;
    public form: FormGroup;
    userSession: UsuarioSessao;
    clienteBancadaFilterCtrl: FormControl = new FormControl();

    constructor(
        private _formBuilder: FormBuilder,
        private _snack: CustomSnackbarService,
        private _route: ActivatedRoute,
        private _osBancadaService: OSBancadaService,
        private _clienteBancadaService: ClienteBancadaService,
        public _location: Location,
        private _dialog: MatDialog,
        private _userService: UserService
    ) {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    async ngOnInit() {

        this.codOsbancada = +this._route.snapshot.paramMap.get('codOsbancada');
        this.inicializarForm();
        this.isAddMode = !this.codOsbancada;
        this.obterCliente();

        if (!this.isAddMode) {
            this._osBancadaService.obterPorCodigo(this.codOsbancada)
                .pipe(first())
                .subscribe(data => {
                    this.form.patchValue(data);
                    this.osBancada = data;
                    this._clienteBancadaService.obterPorCodigo(data.codClienteBancada)
                        .pipe(first())
                        .subscribe(data => {
                            this.form.patchValue(data);
                            this.clienteBancada = data;
                        });
                });
        }
        this.registrarEmitters();
        this.loading = false;
    }

    private inicializarForm() {
        this.form = this._formBuilder.group({
            codClienteBancada: [undefined, Validators.required],
            nfentrada: [undefined, Validators.required],
            dataNf: [undefined, Validators.required],
            dataChegada: [undefined, Validators.required],
            prazoEntrega: [undefined, Validators.required],
            valorNf: [undefined, Validators.required],
            observacao: [undefined]
        });
    }

    private registrarEmitters() {
        this.clienteBancadaFilterCtrl.valueChanges
            .pipe(
                takeUntil(this._onDestroy),
                debounceTime(700),
                distinctUntilChanged()
            )
            .subscribe(() => {
                this.obterCliente(this.clienteBancadaFilterCtrl.value);
            });
    }

    private async obterCliente(filtro: string = '') {
        const params: ClienteBancadaParameters = {
            filter: filtro,
            sortActive: 'nomeCliente',
            sortDirection: 'asc',
            indAtivo: statusConst.ATIVO,
            pageSize: 100
        }
        const data = await this._clienteBancadaService.obterPorParametros(params).toPromise();
        this.clientes = data.items;
    }

    public salvar(): void {
        this.isAddMode ? this.criar() : this.atualizar();
    }

    atualizar(): void {
        const form: any = this.form.getRawValue();


        let obj = {
            ...this.osBancada,
            ...form,
            ...{
                dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
                codUsuarioManut: this.userSession.usuario?.codUsuario
            }
        };
        console.log(obj);

        this._osBancadaService.atualizar(obj).subscribe(() => {
            this._snack.exibirToast(`OS de Bancada atualizado com sucesso!`, "success");
            this._location.back();
        });
    }

    criar(): void {
        const form = this.form.getRawValue();

        let obj = {
            ...this.osBancada,
            ...form,
            ...{
                codUsuarioCadastro: this.userSession.usuario.codUsuario,
                dataCadastro: moment().format('YYYY-MM-DD HH:mm:ss')
            }
        };
        console.log(obj);

        this._osBancadaService.criar(obj).subscribe(() => {
            this._snack.exibirToast(`OS de Bancada criada com sucesso!`, "success");
            this._location.back();
        });
    }

    criarPeca() {
        this._dialog.open(LaboratorioOSBancadaPecasDialogComponent, {
          data: { osBancada: this.osBancada }
        });
      }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }

}
