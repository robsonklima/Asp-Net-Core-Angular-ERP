import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Usuario } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';

@Component({
    selector: 'informacoes-tecnicas',
    templateUrl: './informacoes-tecnicas.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class InformacoesTecnicasComponent implements OnInit {

    @Output() respostaPanel = new EventEmitter();
    carregado: boolean;

    formInformacoes: FormGroup;
    userSession: UserSession;
    usuario: Usuario;

    constructor(
        private _formBuilder: FormBuilder,
        private _userSvc: UserService
    ) {
        this.userSession = JSON.parse(this._userSvc.userSession);
    }

    async ngOnInit() {
        this.carregado = false;
        this.inicializarForm();
        this.usuario = await this._userSvc.obterPorCodigo(this.userSession.usuario.codUsuario).toPromise();

        this.formInformacoes.controls['nomeUsuario'].setValue(this.usuario.nomeUsuario);
        this.formInformacoes.controls['dataNascimento'].setValue("02/08/1992");
        this.formInformacoes.controls['genero'].setValue("Masculino");
        this.formInformacoes.controls['email'].setValue(this.usuario.email);
        this.formInformacoes.controls['fone'].setValue(this.usuario.fone);

        this.carregado = true;
        this.respostaPanel.emit(this.carregado);
    }

    private inicializarForm(): void {
        this.formInformacoes = this._formBuilder.group({
            nomeUsuario: [undefined, [Validators.required]],
            dataNascimento: [undefined, [Validators.required]],
            endereco: [undefined, [Validators.required]],
            bairro: [undefined, [Validators.required]],
            cidade: [undefined, [Validators.required]],
            cep: [undefined, [Validators.required]],
            genero: [undefined, [Validators.required]],
            numero: [undefined, [Validators.required]],
            ramal: [undefined, [Validators.required]],
            email: [undefined],
            fone: [undefined]
        });
    }
}
