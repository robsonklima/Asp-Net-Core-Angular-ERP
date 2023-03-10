import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { fromEvent, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { Location } from '@angular/common';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { MatDialog } from '@angular/material/dialog';
import { OsBancadaPecasOrcamento } from 'app/core/types/os-bancada-pecas-orcamento.types';
import { OsBancadaPecasOrcamentoService } from 'app/core/services/os-bancada-pecas-orcamento.service';
import { PecaRE5114Service } from 'app/core/services/pecaRE5114.service';
import { OSBancadaService } from 'app/core/services/os-bancada.service';
import { OSBancada } from 'app/core/types/os-bancada.types';
import { PecaRE5114 } from 'app/core/types/pecaRE5114.types';
import moment from 'moment';
import { LaboratorioOrcamentoPecaDialogComponent } from '../laboratorio-orcamento-peca-dialog/laboratorio-orcamento-peca-dialog.component';
import { Exportacao, ExportacaoFormatoEnum, ExportacaoTipoEnum } from 'app/core/types/exportacao.types';
import { FileMime } from 'app/core/types/file.types';
import { ExportacaoService } from 'app/core/services/exportacao.service';

@Component({
    selector: 'app-laboratorio-orcamento-form',
    templateUrl: './laboratorio-orcamento-form.component.html'
})
export class LaboratorioOrcamentoFormComponent implements OnInit, OnDestroy {

    protected _onDestroy = new Subject<void>();
    public orcamento: OsBancadaPecasOrcamento;
    public osBancada: OSBancada;
    public pecaRE5114: PecaRE5114;
    public loading: boolean = true;
    public codOrcamento: number;
    public isAddMode: boolean;
    public form: FormGroup;
    userSession: UsuarioSessao;
    clienteBancadaFilterCtrl: FormControl = new FormControl();
    @ViewChild('numRE5114InputControl') numRE5114InputControl: ElementRef;

    constructor(
        private _formBuilder: FormBuilder,
        private _snack: CustomSnackbarService,
        private _route: ActivatedRoute,
        private _osBancadaPecaOrcamentoService: OsBancadaPecasOrcamentoService,
        private _osbancadaService: OSBancadaService,
        private _peca5114Service: PecaRE5114Service,
        public _location: Location,
        private _dialog: MatDialog,
        private _exportacaoService: ExportacaoService,
        private _userService: UserService
    ) {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    async ngOnInit() {
        this.codOrcamento = +this._route.snapshot.paramMap.get('codOrcamento');
        this.inicializarForm();
        this.isAddMode = !this.codOrcamento;
        this.orcamento = await this._osBancadaPecaOrcamentoService.obterPorCodigo(this.codOrcamento).toPromise();
        this.registrarEmitters();
 
        this.loading = false;
    }

    private inicializarForm() {
        this.form = this._formBuilder.group({
            numRe5114: [undefined],
            valorPreAprovado: [undefined],
            observacao: [undefined],
            indOrcamentoAprov: [undefined],
            motivoReprov:  [undefined],
            codMagnus: [{
                value: undefined,
                disabled: true,
            }],
            nfentrada: [{
                value: undefined,
                disabled: true,
            }],
            nomePeca: [{
                value: undefined,
                disabled: true,
            }],
            apelido: [{
                value: undefined,
                disabled: true,
            }],
            numSerie: [{
                value: undefined,
                disabled: true,
            }],
        });
    }

    private registrarEmitters() {
        fromEvent(this.numRE5114InputControl.nativeElement, 'keyup').pipe(
            map((event: any) => {
              return event.target.value;
            })
            , debounceTime(1000)
            , distinctUntilChanged()
          ).subscribe((query: number) => {
            this.obterDados(query);
          });
        
    }

    private preencherForm(): void {
        this.form.controls['numRe5114'].setValue(this.pecaRE5114?.numRe5114);
        this.form.controls['codMagnus'].setValue(this.pecaRE5114?.peca?.codMagnus);
        this.form.controls['nfentrada'].setValue(this.osBancada?.nfentrada);
        this.form.controls['nomePeca'].setValue(this.pecaRE5114?.peca?.nomePeca);
        this.form.controls['apelido'].setValue(this.osBancada?.clienteBancada?.apelido);
        this.form.controls['numSerie'].setValue(this.pecaRE5114?.numSerie);
    }

    async obterDados(codigo: number){
        this.pecaRE5114 = (await this._peca5114Service.obterPorParametros({
            numRe5114: codigo.toString()}).toPromise()).items.shift();
        
        this.osBancada = await this._osbancadaService.obterPorCodigo(this.pecaRE5114.codOsbancada).toPromise();

        this.preencherForm();
    }

    public salvar(): void {
        this.isAddMode ? this.criar() : this.atualizar();
    }

     atualizar(): void {
        const form: any = this.form.getRawValue();


        let obj: OsBancadaPecasOrcamento = {
            ...this.orcamento,
            ...form,
            ...{
                dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
                codUsuarioManut: this.userSession.usuario?.codUsuario
            }
        };

        this._osBancadaPecaOrcamentoService.atualizar(obj).subscribe(() => {
            this._snack.exibirToast(`Orçamento atualizado com sucesso!`, "success");
            this._location.back();
        });
    }

    criar(): void {
        const form: any = this.form.getRawValue();

        let obj: OsBancadaPecasOrcamento = {
            ...this.orcamento,
            ...form,
            ...{
                codPecaRe5114: this.pecaRE5114.codPecaRe5114,
                codOsbancada: this.osBancada.codOsbancada
            }
        };
        
        this._osBancadaPecaOrcamentoService.criar(obj).subscribe(() => {
            this._snack.exibirToast(`Orçamento criado com sucesso!`, "success");
            this._location.back();
        });
    }

    adicionarPeca() {
        const dialogRef = this._dialog.open(LaboratorioOrcamentoPecaDialogComponent, {
          data: { orcamento: this.orcamento }
        });

        dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
            if (confirmacao) {
                this.ngOnInit();
            }
        });
      }

      exportar(codOrcamento) {
        let exportacaoParam: Exportacao = {
          formatoArquivo: ExportacaoFormatoEnum.PDF,
          tipoArquivo: ExportacaoTipoEnum.ORC_BANCADA,
          entityParameters: {
            codOrcamento: codOrcamento
          }
        }
    
        this._exportacaoService
          .exportar(FileMime.PDF, exportacaoParam)
          .catch(e => { this._snack.exibirToast(`Não foi possível realizar o download ${e.message}`) });
      }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }
}
