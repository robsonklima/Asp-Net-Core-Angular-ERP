import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { statusConst } from 'app/core/types/status-types';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { Location } from '@angular/common';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { OSBancada } from 'app/core/types/os-bancada.types';
import { OSBancadaService } from 'app/core/services/os-bancada.service';
import { ClienteBancada, ClienteBancadaParameters } from 'app/core/types/cliente-bancada.types';
import { ClienteBancadaService } from 'app/core/services/cliente-bancada.service';

@Component({
    selector: 'app-laboratorio-os-bancada-form',
    templateUrl: './laboratorio-os-bancada-form.component.html'
})
export class LaboratorioOSBancadaFormComponent implements OnInit, OnDestroy {

    protected _onDestroy = new Subject<void>();
    public osBancada: OSBancada;
    public clienteBancada: ClienteBancada;
    public loading: boolean = true;
    public codOsbancada: number;
    public isAddMode: boolean;
    public form: FormGroup;
    userSession: UsuarioSessao;

    constructor(
        private _formBuilder: FormBuilder,
        private _snack: CustomSnackbarService,
        private _route: ActivatedRoute,
        private _osBancadaService: OSBancadaService,
        private _clienteBancadaService: ClienteBancadaService,
        public _location: Location,
        private _userService: UserService
    ) {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    async ngOnInit() {

        this.codOsbancada = +this._route.snapshot.paramMap.get('codOsbancada');
        this.isAddMode = !this.codOsbancada;
        this.inicializarForm();

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
        this.aoSelecionarCNPJ();
        this.loading = false;
    }

    private registrarEmitters() {
        // this.form.controls['cnpJ_CGC'].valueChanges
        //     .pipe(
        //         debounceTime(700),
        //         distinctUntilChanged(),
        //         takeUntil(this._onDestroy)
        //     )
        //     .subscribe((cnpj) => {
        //         this.aoSelecionarCNPJ();
        //     });
    }

    aoSelecionarCNPJ() {
        if (
            this.form.controls['cnpJ_CGC'].value &&
            this.form.controls['cnpJ_CGC'].value != ''
        ) {
            this.obterCliente();
            this.form.controls['apelido'].enable();
        }
        else {
            this.form.controls['apelido'].disable();
        }
    }


    private inicializarForm() {
        this.form = this._formBuilder.group({
            cnpJ_CGC: [undefined, Validators.required],
            apelido: [undefined, Validators.required],
            nfentrada: [undefined, Validators.required],
            dataNf: [undefined, Validators.required],
            dataChegada: [undefined, Validators.required],
            prazoEntrega: [undefined, Validators.required],
            valorNf: [undefined, Validators.required],
            observacao: [undefined]
        });
    }

    private async obterCliente() {
        const params: ClienteBancadaParameters = {
            sortActive: 'nomeCliente',
            sortDirection: 'asc',
            cnpJ_CGC: this.form.controls['cnpJ_CGC'].value,
            indAtivo: statusConst.ATIVO,
            pageSize: 100
        }
        const data = await this._clienteBancadaService.obterPorParametros(params).toPromise();
        this.clienteBancada = data.items.shift();
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

        this._clienteBancadaService.atualizar(obj).subscribe(() => {
            this._snack.exibirToast(`Preço atualizado com sucesso!`, "success");
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

        this._clienteBancadaService.criar(obj).subscribe(() => {
            this._snack.exibirToast(`OS de Bancada criada com sucesso!`, "success");
            this._location.back();
        });
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }

}
