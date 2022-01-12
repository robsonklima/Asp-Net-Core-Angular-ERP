import {
    AfterViewInit,
    ChangeDetectionStrategy, ChangeDetectorRef, Component, Input,
    OnDestroy, OnInit, ViewEncapsulation
} from '@angular/core';
import { Router } from '@angular/router';
import { BooleanInput } from '@angular/cdk/coercion';
import { Subject } from 'rxjs';
import { UserService } from 'app/core/user/user.service';
import { AuthService } from 'app/core/auth/auth.service';
import { ImagemPerfilModel } from 'app/modules/main/dialog/dialog-alterar-foto-perfil/dialog-alterar-foto-perfil.component';
import { FotoService } from 'app/core/services/foto.service';
import { SharedService } from 'app/shared.service';
import { ConfiguracoesComponent } from 'app/modules/main/configuracoes/configuracoes.component';

@Component({
    selector: 'user',
    templateUrl: './user.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    exportAs: 'user'
})
export class UserComponent implements OnInit, OnDestroy, AfterViewInit {
    static ngAcceptInputType_showAvatar: BooleanInput;
    @Input() showAvatar: boolean = true;
    usuario: any;
    loading: boolean = true;

    public dadosFotoUsuario: ImagemPerfilModel;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _router: Router,
        private _userService: UserService,
        private _authService: AuthService,
        private _fotoService: FotoService,
        private _cdr: ChangeDetectorRef,
        private _sharedService: SharedService
    ) {
        this.usuario = this._userService.get();
    }

    async ngOnInit() {
        this.dadosFotoUsuario = (await this._fotoService.buscarFotoUsuario(this.usuario.codUsuario).toPromise());
        this.loading = false;
        this._cdr.detectChanges();
    }

    async ngAfterViewInit() {
        this._sharedService.listenEvent(ConfiguracoesComponent).subscribe(async (params) => {
            this.dadosFotoUsuario = (await this._fotoService.buscarFotoUsuario(this.usuario.codUsuario).toPromise());
            this._cdr.detectChanges();
        });
    }

    ngOnDestroy(): void {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    signOut(): void {
        this._authService.signOut();
        this._router.navigate(['/sign-in']);
    }

    navegarParaPerfil(): void {
        this._router.navigate(['/configuracoes/' + this.usuario.codUsuario]);
    }
}
