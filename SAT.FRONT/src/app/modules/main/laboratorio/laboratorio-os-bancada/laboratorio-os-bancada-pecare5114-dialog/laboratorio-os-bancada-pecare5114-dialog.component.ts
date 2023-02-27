import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import _ from 'lodash';
import { LaboratorioOSBancadaFormComponent } from '../laboratorio-os-bancada-form/laboratorio-os-bancada-form.component';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { PecaRE5114 } from 'app/core/types/pecaRE5114.types';
import { forkJoin, Subject } from 'rxjs';
import { PecaRE5114Service } from 'app/core/services/pecaRE5114.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OSBancadaPecas } from 'app/core/types/os-bancada-pecas.types';
import { OSBancadaPecasService } from 'app/core/services/os-bancada-pecas.service';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import moment from 'moment';

@Component({
    selector: 'app-laboratorio-os-bancada-pecare5114-dialog',
    templateUrl: './laboratorio-os-bancada-pecare5114-dialog.component.html'
})
export class LaboratorioOSBancadaPecaRE5114DialogComponent implements OnInit {
    pecaRe5114: PecaRE5114;
    osBancadaPeca: OSBancadaPecas;
    userSession: UserSession;
    public form: FormGroup;
    loading: boolean = true;
    pecaFilterCtrl: FormControl = new FormControl();
    protected _onDestroy = new Subject<void>();

    constructor(
        @Inject(MAT_DIALOG_DATA) data: any,
        private _formBuilder: FormBuilder,
        public _dialogRef: MatDialogRef<LaboratorioOSBancadaFormComponent>,
        private _pecaRE5114Service: PecaRE5114Service,
        private _osBancadaPecaService: OSBancadaPecasService,
        private _userService: UserService,
        private _snack: CustomSnackbarService,
    ) {
        this.osBancadaPeca = data?.osBancadaPecas;
        this.userSession = JSON.parse(this._userService.userSession);
    }

    async ngOnInit() {
        this.inicializarForm();
        this.pecaRe5114 = await this._pecaRE5114Service.obterPorCodigo(this.osBancadaPeca.codPecaRe5114).toPromise();
        this.preencherForm();
        this.loading = false;
    }

    private inicializarForm() {
        this.form = this._formBuilder.group({
            numRe5114: [undefined],
            indGarantia: [undefined],
            defeitoConstatado: [undefined],
            solucao: [undefined],
            indSucata: [undefined],
            indDevolver: [undefined],
            indPecaLiberada: [undefined],
            motivoSucata: [undefined],
            motivoDevolucao: [undefined],
        });
    }

    private preencherForm(): void {
        this.form.controls['numRe5114'].setValue(this.pecaRe5114?.numRe5114);
        this.form.controls['indGarantia'].setValue(this.osBancadaPeca?.indGarantia);
        this.form.controls['defeitoConstatado'].setValue(this.osBancadaPeca?.defeitoConstatado);
        this.form.controls['solucao'].setValue(this.osBancadaPeca?.solucao);
        this.form.controls['motivoSucata'].setValue(this.pecaRe5114?.motivoSucata);
        this.form.controls['motivoDevolucao'].setValue(this.pecaRe5114?.motivoDevolucao);
    }

    async onChangeSucata($event: MatSlideToggleChange) {
        if ($event.checked)
            this.pecaRe5114.indSucata = 1;
        else
            this.pecaRe5114.indSucata = 0;
    }

    async onChangeDevolucao($event: MatSlideToggleChange) {
        if ($event.checked)
            this.pecaRe5114.indDevolver = 1;
        else
            this.pecaRe5114.indDevolver = 0;
    }

    async onChangeLiberado($event: MatSlideToggleChange) {
        if ($event.checked) {
            this.osBancadaPeca.indPecaLiberada = 1;
            this.osBancadaPeca.dataHoraPecaLiberada = moment().format('YYYY-MM-DD HH:mm:ss');
        }
        else {
            this.osBancadaPeca.indPecaLiberada = 0;
            this.osBancadaPeca.dataHoraPecaLiberada = null;
        }
    }

    cancelar() {
        this._dialogRef.close();
    }

    async salvar() {
        const form: any = this.form.getRawValue();

        let pecaRe5114: PecaRE5114 = {
            ...this.pecaRe5114,
            ...form,
            ...{
                numRe5114: this.pecaRe5114.numRe5114,
                dataManut: moment().format('YYYY-MM-DD HH:mm:ss'),
                codUsuarioManut: this.userSession.usuario?.codUsuario,
                indSucata: this.pecaRe5114.indSucata,
                indDevolver: this.pecaRe5114.indDevolver,
            }
        };

        let osBancadaPeca: OSBancadaPecas = {
            ...this.osBancadaPeca,
            ...form,
            ...{
                dataManut: moment().format('YYYY-MM-DD HH:mm:ss'),
                codUsuarioManut: this.userSession.usuario?.codUsuario,
                indPecaLiberada: this.osBancadaPeca.indPecaLiberada,
                dataHoraPecaLiberada: this.osBancadaPeca.dataHoraPecaLiberada
            }
        };

        forkJoin([
            this._pecaRE5114Service.atualizar(pecaRe5114),
            this._osBancadaPecaService.atualizar(osBancadaPeca),
        ]).subscribe(([result1, result2]) => {
            this._snack.exibirToast('Peça atualizada com sucesso', 'sucess');
            this._dialogRef.close();
        });
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}

