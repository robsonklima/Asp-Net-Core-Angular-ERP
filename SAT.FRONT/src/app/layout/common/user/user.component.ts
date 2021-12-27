import { ChangeDetectionStrategy, Component, Input, 
         OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { BooleanInput } from '@angular/cdk/coercion';
import { Subject } from 'rxjs';
import { UserService } from 'app/core/user/user.service';

@Component({
    selector       : 'user',
    templateUrl    : './user.component.html',
    encapsulation  : ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    exportAs       : 'user'
})
export class UserComponent implements OnInit, OnDestroy
{
    static ngAcceptInputType_showAvatar: BooleanInput;
    @Input() showAvatar: boolean = true;
    usuario: any;

    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _router: Router,
        private _userService: UserService,
    )
    {
        this.usuario = this._userService.get();
    }
    
    ngOnInit(): void
    {
       
    }

    ngOnDestroy(): void
    {
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
    
    signOut(): void
    {
        this._router.navigate(['/sign-out']);
    }

    navegarParaPerfil(): void {
        //this._router.navigate(['/configuracoes/'+this.usuario.codUsuario]);
    }
}
