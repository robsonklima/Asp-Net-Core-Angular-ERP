import { AfterViewInit, Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Peca, PecaParameters } from 'app/core/types/peca.types';
import { PecaService } from 'app/core/services/peca.service';
import { fromEvent, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import _ from 'lodash';
import { LaboratorioOrcamentoFormComponent } from '../laboratorio-orcamento-form/laboratorio-orcamento-form.component';
import { OsBancadaPecasOrcamento } from 'app/core/types/os-bancada-pecas-orcamento.types';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { OrcamentoPecasEspec } from 'app/core/types/orcamento-pecas-espec.types';
import { OrcamentoPecasEspecService } from 'app/core/services/orcamento-pecas-espec.service';

@Component({
    selector: 'app-laboratorio-orcamento-peca-dialog',
    templateUrl: './laboratorio-orcamento-peca-dialog.component.html'
})
export class LaboratorioOrcamentoPecaDialogComponent implements OnInit, AfterViewInit {
    orcamento: OsBancadaPecasOrcamento;
    pecaOrcamento: OrcamentoPecasEspec;
    peca: Peca;
    quantidade: number = 0;
    valorDesconto: number = 0.0;
    userSession: UserSession;
    public form: FormGroup;
    loading: boolean = false;
    pecaFilterCtrl: FormControl = new FormControl();
    protected _onDestroy = new Subject<void>();
    @ViewChild('pecaInputControl') pecaInputControl: ElementRef;


    constructor(
        @Inject(MAT_DIALOG_DATA) data: any,
        private _formBuilder: FormBuilder,
        public _dialogRef: MatDialogRef<LaboratorioOrcamentoFormComponent>,
        private _pecaService: PecaService,
        private _orcamentoPecaEspecService: OrcamentoPecasEspecService,
        private _userService: UserService,
        private _snack: CustomSnackbarService,
        private dialogRef: MatDialogRef<LaboratorioOrcamentoFormComponent>
    ) {
        this.orcamento = data?.orcamento;
        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngOnInit() {
        this.inicializarForm();
    }

    ngAfterViewInit() {
        fromEvent(this.pecaInputControl.nativeElement, 'keyup').pipe(
            map((event: any) => {
                return event.target.value;
            })
            , debounceTime(1000)
            , distinctUntilChanged()
        ).subscribe((text: string) => {
            this.obterPecas(text);
        });
    }

    private async obterPecas(filtro: string = '') {
        let params: PecaParameters = {
            codMagnus: filtro
        };

        const data = await this._pecaService
            .obterPorParametros(params)
            .toPromise();

        this.peca = data.items.shift();
        this.preencherForm();
    }

    private inicializarForm() {
        this.form = this._formBuilder.group({
            codMagnus: [undefined],
            codPeca: [undefined],
            valorPeca: [undefined],
            percIpi: [undefined],
            quantidade: [undefined],
            valorDesconto: [undefined]
        });
    }

    private preencherForm(): void {
        this.form.controls['codPeca'].setValue(this.peca?.codPeca);
        this.form.controls['valorPeca'].setValue(this.peca?.valPeca);
        this.form.controls['percIpi'].setValue(this.peca?.valIPI);
    }

    onChangeDesconto($event: MatSlideToggleChange) {
        if ($event.checked)
            this.valorDesconto = 40.0;
        else
            this.valorDesconto = 0.0;
    }

    cancelar() {
        this._dialogRef.close();
    }

    async salvar() {
        const form: any = this.form.getRawValue();

        let obj: OrcamentoPecasEspec = {
            ...this.pecaOrcamento,
            ...form,
            ...{
                codPeca: this.peca.codPeca,
                valorDesconto: this.valorDesconto,
                codOrcamento: this.orcamento.codOrcamento,
                codOsbancada: this.orcamento.codOsbancada,
                codPecaRe5114: this.orcamento.codPecaRe5114
            }
        };

        this._orcamentoPecaEspecService.criar(obj).subscribe(() => {
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

