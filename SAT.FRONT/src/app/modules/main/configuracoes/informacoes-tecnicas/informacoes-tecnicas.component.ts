import { formatDate } from '@angular/common';
import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { DespesaCartaoCombustivel } from 'app/core/types/despesa-cartao-combustivel.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import { Usuario } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import Enumerable from 'linq';
import moment from 'moment';
import { Subject } from 'rxjs';

@Component({
    selector: 'informacoes-tecnicas',
    templateUrl: './informacoes-tecnicas.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class InformacoesTecnicasComponent implements OnInit, OnDestroy {

    @Output() respostaPanel = new EventEmitter();
    carregado: boolean;
    formInformacoes: FormGroup;
    userSession: UserSession;
    tecnico: Tecnico;

    protected _onDestroy = new Subject<void>();

    public despesaCartaoCombustivel: DespesaCartaoCombustivel;

    constructor(
        private _formBuilder: FormBuilder,
        private _userSvc: UserService,
        private _tecnicoService: TecnicoService,
        private _snack: CustomSnackbarService
    ) {
        this.userSession = JSON.parse(this._userSvc.userSession);
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }

    async ngOnInit() {
        this.carregado = false;
        this.inicializarForm();
        this.tecnico = await this._tecnicoService.obterPorCodigo(this.userSession.usuario.codTecnico).toPromise();
        let dadosVeiculos = Enumerable.from(this.tecnico.despesaCartaoCombustivelTecnico).toArray();

        for (let dados of dadosVeiculos) {
            //if (dados.despesaCartaoCombustivel.indAtivo) {
            this.despesaCartaoCombustivel = dados.despesaCartaoCombustivel;
            break;
            // }
        }

        // Unico jeito válido até o momento para preencher certo a data no form
        this.formInformacoes.get('dataNascimento').setValue(new Date(this.tecnico.dataNascimento).toISOString().split('T')[0]);

        this.formInformacoes.controls['apelido'].setValue(this.tecnico.apelido);
        this.formInformacoes.controls['rg'].setValue(this.tecnico.rg);
        this.formInformacoes.controls['fonePerto'].setValue(this.tecnico.fonePerto);
        this.formInformacoes.controls['foneParticular'].setValue(this.tecnico.foneParticular);

        this.carregado = true;
        this.respostaPanel.emit(this.carregado);
    }

    private inicializarForm(): void {
        this.formInformacoes = this._formBuilder.group({
            apelido: [undefined, [Validators.required]],
            dataNascimento: new FormControl('', Validators.required),
            rg: [undefined, [Validators.required]],
            fonePerto: [undefined, [Validators.required]],
            foneParticular: [undefined, [Validators.required]]
        });
    }

    salvar() {

        this.formInformacoes.disable();
        const form: any = this.formInformacoes.getRawValue();

        let updateTecnico = {
            ...this.tecnico,
            ...form
        };

        this._tecnicoService.atualizar(updateTecnico).subscribe(() => {
            this._snack.exibirToast(`Técnico atualizado com sucesso!`, "success");
            this.formInformacoes.enable();
        });
    }
}
