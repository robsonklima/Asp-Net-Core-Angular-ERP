import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { OSBancada } from 'app/core/types/os-bancada.types';
import { LaboratorioOSBancadaFormComponent } from '../laboratorio-os-bancada-form/laboratorio-os-bancada-form.component';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { OSBancadaPecas } from 'app/core/types/os-bancada-pecas.types';
import { PecaRE5114, PecaRE5114Parameters } from 'app/core/types/pecaRE5114.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { FilialService } from 'app/core/services/filial.service';
import { Peca, PecaParameters } from 'app/core/types/peca.types';
import { PecaService } from 'app/core/services/peca.service';
import { Subject } from 'rxjs';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { PecaRE5114Service } from 'app/core/services/pecaRE5114.service';
import { OSBancadaPecasService } from 'app/core/services/os-bancada-pecas.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import moment from 'moment';
import _ from 'lodash';

@Component({
    selector: 'app-laboratorio-os-bancada-pecas-dialog',
    templateUrl: './laboratorio-os-bancada-pecas-dialog.component.html'
})
export class LaboratorioOSBancadaPecasDialogComponent implements OnInit {
    osBancada: OSBancada;
    osBancadaPeca: OSBancadaPecas;
    pecaRe5114: PecaRE5114;
    filiais: Filial[] = [];
    pecas: Peca[] = [];
    userSession: UserSession;
    public form: FormGroup;
    loading: boolean = true;
    pecaFilterCtrl: FormControl = new FormControl();
    protected _onDestroy = new Subject<void>();

    constructor(
        @Inject(MAT_DIALOG_DATA) data: any,
        private _formBuilder: FormBuilder,
        public _dialogRef: MatDialogRef<LaboratorioOSBancadaFormComponent>,
        private _filialService: FilialService,
        private _osBancadaPecasService: OSBancadaPecasService,
        private _pecaService: PecaService,
        private _pecaRE5114Service: PecaRE5114Service,
        private _userService: UserService,
        private _snack: CustomSnackbarService,
        private dialogRef: MatDialogRef<LaboratorioOSBancadaFormComponent>
    ) {
        this.osBancada = data?.osBancada;
        this.userSession = JSON.parse(this._userService.userSession);
    }

    async ngOnInit() {
        this.inicializarForm();
        this.obterFilial();
        this.obterPecas();
        this.registrarEmitters();
        this.loading = false;
    }

    private async obterPecas(filtro: string = '') {
        let params: PecaParameters = {
            filter: filtro,
            sortActive: 'nomePeca',
            sortDirection: 'asc',
            pageSize: 1000
        };

        const data = await this._pecaService
            .obterPorParametros(params)
            .toPromise();

        this.pecas = data.items;
    }

    private async obterFilial() {
        let params: FilialParameters = {
            sortActive: 'nomeFilial',
            sortDirection: 'asc',
            pageSize: 1000
        };
        const data = await this._filialService
            .obterPorParametros(params)
            .toPromise();
        this.filiais = data.items;
    }

    private async obterPeca5114() {
        let params: PecaRE5114Parameters = {
            numRe5114: this.form.controls['numRe5114'].value
        };
        
        return await this._pecaRE5114Service
            .obterPorParametros(params)
            .toPromise();
    }

    private inicializarForm() {
        this.form = this._formBuilder.group({
            numRe5114: [undefined],
            codFilialRe5114: [undefined],
            numSerie: [undefined],
            nomeTecnicoRelatante: [undefined],
            codPeca: [undefined],
            numPecaCliente: [undefined],
            valorEntrada: [undefined],
            numItemNf: [undefined],
            defeitoRelatado: [undefined],
            indGarantia: [undefined]

        });
    }

    private registrarEmitters() {
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

    cancelar() {
        this._dialogRef.close();
    }

    async salvar() {
        await this.criarPecaRE5114();
        this.criarOSBancadaPecas();
    }

    private async criarPecaRE5114(): Promise<any> {
        return new Promise((resolve) => {
            const form: any = this.form.getRawValue();

            let obj: PecaRE5114 = {
                ...this.pecaRe5114,
                ...form,
                ...{
                    dataCadastro: moment().format('YYYY-MM-DD HH:mm:ss'),
                    codUsuarioCadastro: this.userSession.usuario.codUsuario,
                    codOsbancada: this.osBancada.codOsbancada,
                    indSucata: 0,
                    indDevolver: 0,
            
                }
            };

            this._pecaRE5114Service.criar(obj).subscribe();
            resolve(null);
        });
    }

    private async criarOSBancadaPecas(){
        const pecaRE5114 = (await this.obterPeca5114()).items.shift();
        console.log(pecaRE5114);
        

        const form: any = this.form.getRawValue();

            let obj: OSBancadaPecas = {
                ...this.osBancadaPeca,
                ...form,
                ...{
                    dataCadastro: moment().format('YYYY-MM-DD HH:mm:ss'),
                    codUsuarioCadastro: this.userSession.usuario.codUsuario,
                    indPecaDevolvida: 0,
                    codPecaRe5114: pecaRE5114.codPecaRe5114,
                    codOsbancada: this.osBancada.codOsbancada,
                    indPecaLiberada: 0,
                }
            };
            console.log(obj);
            

            this._osBancadaPecasService.criar(obj).subscribe(() => {
                this._snack.exibirToast("Peça adicionada com sucesso!", "success");
                this.dialogRef.close(true);
              }, e => {
                this.form.enable();
              });
          
              this.dialogRef.close(true);
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}

