import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import _ from 'lodash';
import { OSBancada } from 'app/core/types/os-bancada.types';
import { LaboratorioOSBancadaFormComponent } from '../laboratorio-os-bancada-form/laboratorio-os-bancada-form.component';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { OSBancadaPecas } from 'app/core/types/os-bancada-pecas.types';
import { PecaRE5114 } from 'app/core/types/pecaRE5114.types';
import { Filial, FilialParameters } from 'app/core/types/filial.types';
import { FilialService } from 'app/core/services/filial.service';
import { Peca, PecaParameters } from 'app/core/types/peca.types';
import { PecaService } from 'app/core/services/peca.service';
import { Subject } from 'rxjs';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import moment from 'moment';

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
        private _pecaService: PecaService,
        private _userService: UserService
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
        const form: any = this.form.getRawValue();
		
        let pecaRE5114: PecaRE5114 = {
                ...this.pecaRe5114,
                ...form,
                ...{
                    dataCadastro: moment().format('YYYY-MM-DD HH:mm:ss'),
                    codUsuarioCad: this.userSession.usuario?.codUsuario,
                    codOsbancada: this.osBancada.codOsbancada
                }
            };
    
        // let protocolo: ProtocoloChamadoSTN = {
        //         ...this.protocolo,
        //         ...form,
        //         ...{
        //             dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
        //             codUsuarioCad: this.userSession.usuario?.codUsuario,
        //     indAtivo: statusConst.ATIVO,
        //     acaoSTN: this.form?.controls['acaoSTN']?.value +  moment().format(' DD/MM HH:mm') + ' * ',
        //     tecnicoCampo: this.form.controls['codTecnicos']?.value
        //         }
        //     };
    
        // forkJoin([
        //   this._ordemServicoSTNService.atualizar(atendimento),
        //   this._protocoloChamadoSTNService.atualizar(protocolo),
        // ]).subscribe(([result1, result2]) => {
        //     this._snack.exibirToast('Atendimento atualizado com sucesso','sucess');
        //     this._router.navigate(['suporte-stn/lista']);
        //   });
        
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}

