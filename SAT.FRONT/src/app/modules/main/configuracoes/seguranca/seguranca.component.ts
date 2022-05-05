import { ChangeDetectionStrategy, ChangeDetectorRef, Component, EventEmitter, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { UsuarioDispositivoService } from 'app/core/services/usuario-dispositivo.service';
import { UsuarioDispositivo } from 'app/core/types/usuario-dispositivo.types';
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
    usuarioDispositivos: UsuarioDispositivo[] = [];
    formSenhaAtual: FormGroup;
    formNovaSenha: FormGroup;

    public carregandoDispositivos: boolean = true;

    constructor(
        private _formBuilder: FormBuilder,
        private _userSvc: UserService,
        private _snack: CustomSnackbarService,
        private _userService: UserService,
        private _usuarioDispositivo: UsuarioDispositivoService,
        private _cdr: ChangeDetectorRef,
    ) {
        this.userSession = JSON.parse(this._userSvc.userSession);
    }

    async ngOnInit() {
        this.carregandoDispositivos = true;
        this.carregado = false;
        this.inicializarForm();
        this.usuario = await this._userSvc.obterPorCodigo(this.userSession.usuario.codUsuario).toPromise();
        this.usuarioDispositivos = (await this._usuarioDispositivo.obterPorParametros({ codUsuario: this.usuario.codUsuario, indAtivo: 1 }).toPromise()).items;
        this.carregandoDispositivos = false;
        this.carregado = true;
        this.respostaPanel.emit(this.carregado);
    }

    private inicializarForm(): void {
        this.formSenhaAtual = this._formBuilder.group({
            senhaAtual: [undefined, [Validators.required]]
        }),
            this.formNovaSenha = this._formBuilder.group({
                novaSenha: [undefined, [
                    Validators.required,
                    //Minimo 8 caracteres, pelo menos uma letra maiuscula, uma letra minuscula, um numero e um caractere especial
                    Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[()[\\]{}=\\-~,.;<>:@$!%*?&#])[A-Za-z\\d()[\\]{}=\\-~,.;<>:@$!%*?&#]{8,}$')]]
            })
    }

    salvar() {
        this.formSenhaAtual.disable();
        this.formNovaSenha.disable();

        let model = new SegurancaUsuarioModel();
        model.codUsuario = this.usuario.codUsuario;
        model.novaSenha = this.formNovaSenha.getRawValue().novaSenha;
        model.senhaAtual = this.formSenhaAtual.getRawValue().senhaAtual;

        this._userService.alterarSenha(model).subscribe(
            // Success
            (response) => {
                this._snack.exibirToast(`Senha alterada com sucesso!`, "success");
                this.formSenhaAtual.reset();
                this.formNovaSenha.reset();
                this.formSenhaAtual.enable();
                this.formNovaSenha.enable();
            },
            // Success
            (response) => {
                this._snack.exibirToast(response.error.errorMessage, "error");
                this.formSenhaAtual.enable();
                this.formNovaSenha.enable();
            }
        );
    }

    desconectar(codUsuarioDispositivo: UsuarioDispositivo) {
        this.carregandoDispositivos = true;
        codUsuarioDispositivo.indAtivo = 0;
        this._usuarioDispositivo.atualizar(codUsuarioDispositivo).toPromise().then(async () => {
            this._snack.exibirToast(`Dispositivo desconectado.`, "success");
            this.usuarioDispositivos = (await this._usuarioDispositivo.obterPorParametros({ codUsuario: this.usuario.codUsuario, indAtivo: 1 }).toPromise()).items;
            this.carregandoDispositivos = false;
            this._cdr.detectChanges();
        });
    }
}
