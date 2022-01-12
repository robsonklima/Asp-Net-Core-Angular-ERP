import { ChangeDetectionStrategy, Component, EventEmitter, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Usuario } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { SegurancaUsuarioModel, UserSession } from 'app/core/user/user.types';

@Component({
    selector: 'configuracoes-seguranca',
    templateUrl: './seguranca.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ConfiguracoesSegurancaComponent implements OnInit {

    @Output() respostaPanel = new EventEmitter();
    carregado: boolean;
    userSession: UserSession;
    usuario: Usuario;
    securityForm: FormGroup;

    constructor(
        private _formBuilder: FormBuilder,
        private _userSvc: UserService,
        private _snack: CustomSnackbarService,
        private _userService: UserService
    ) {
        this.userSession = JSON.parse(this._userSvc.userSession);
    }

    async ngOnInit() {
        this.carregado = false;
        this.inicializarForm();
        this.usuario = await this._userSvc.obterPorCodigo(this.userSession.usuario.codUsuario).toPromise();
        this.carregado = true;
        this.respostaPanel.emit(this.carregado);
    }

    private inicializarForm(): void {
        this.securityForm = this._formBuilder.group({
            FormSenhaAtual: this._formBuilder.group({
                senhaAtual: [undefined]
            }),
            FormNovaSenha: this._formBuilder.group({
                novaSenha: [undefined]
            })
        });
    }

    salvar() {
        this.securityForm.disable();
        const form: any = this.securityForm.getRawValue();
        let model = new SegurancaUsuarioModel();
        model.codUsuario = this.usuario.codUsuario;
        model.novaSenha = form.novaSenha;
        model.senhaAtual = form.senhaAtual;

        this._userService.alterarSenha(model).subscribe(() => {
            this._snack.exibirToast(`Senha alterada com sucesso!`, "success");
            this.securityForm.enable();
        });
    }
}
