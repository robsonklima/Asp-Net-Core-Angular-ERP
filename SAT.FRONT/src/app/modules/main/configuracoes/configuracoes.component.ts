import {
    AfterViewInit,
    ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy,
    OnInit, ViewChild, ViewEncapsulation
} from '@angular/core';
import { MatDrawer } from '@angular/material/sidenav';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FuseMediaWatcherService } from '@fuse/services/media-watcher';
import { UserService } from 'app/core/user/user.service';
import { Usuario } from 'app/core/types/usuario.types';
import { UserSession } from 'app/core/user/user.types';
import { MatDialog } from '@angular/material/dialog';
import { DialogAlterarFotoPerfilComponent, ImagemPerfilModel } from '../dialog/dialog-alterar-foto-perfil/dialog-alterar-foto-perfil.component';
import { FotoService } from 'app/core/services/foto.service';
import { SharedService } from 'app/shared.service';

@Component({
    selector: 'configuracoes',
    templateUrl: './configuracoes.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ConfiguracoesComponent implements OnInit, OnDestroy, AfterViewInit {

    @ViewChild('drawer') drawer: MatDrawer;
    drawerMode: 'over' | 'side' = 'side';
    drawerOpened: boolean = true;
    panels: any[] = [];
    selectedPanel: string = 'conta';
    loading: boolean = true;
    userSession: UserSession;
    usuario: Usuario;
    showEdit: boolean = false;
    public dadosFotoUsuario: ImagemPerfilModel;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    constructor(
        private _sharedService: SharedService,
        protected _changeDetectorRef: ChangeDetectorRef,
        protected _fuseMediaWatcherService: FuseMediaWatcherService,
        private _userSvc: UserService,
        private _fotoService: FotoService,
        private _cdr: ChangeDetectorRef,
        private _dialog?: MatDialog,
    ) {
        this.userSession = JSON.parse(this._userSvc.userSession);
    }

    // função para receber emit Output do Filho
    panelCarregado(panelCarregado: any) {
        this.loading = !panelCarregado;
        this._cdr.detectChanges();
    }

    onShowEdit(value: boolean) {
        this.showEdit = value;
    }

    ngAfterViewInit(): void {
        this._sharedService.clearListEvents();
    }

    async alterarFoto() {
        const dialogRef = this._dialog.open(DialogAlterarFotoPerfilComponent);
        dialogRef.componentInstance.codUsuario = this.usuario.codUsuario;
        dialogRef.componentInstance.fotoUsuario = this.dadosFotoUsuario.base64;
        dialogRef.afterClosed().subscribe(async (data: any) => {
            this.dadosFotoUsuario = (await this._fotoService.buscarFotoUsuario(this.usuario.codUsuario).toPromise());
            // Faz a chamada para o UserComponent quando o usuário alterar a foto
            this._sharedService.invokeEvent(ConfiguracoesComponent);
            this._cdr.detectChanges();
        });
    }

    ngOnInit() {
        this.getInformacoesUsuario().then(async () => {
            this.dadosFotoUsuario = (await this._fotoService.buscarFotoUsuario(this.usuario.codUsuario).toPromise());
            // Setup available panels
            this.panels = [
                {
                    id: 'conta',
                    icon: 'heroicons_outline:user-circle',
                    title: 'inicio',
                    hidden: false
                },
                {
                    id: 'informacoes-pessoais',
                    icon: 'heroicons_outline:identification',
                    title: 'informacoes pessoais',
                    hidden: false
                },
                {
                    id: 'informacoes-tecnicas',
                    icon: 'heroicons_outline:briefcase',
                    title: 'informacoes tecnicas',
                    hidden: !this.usuario?.codTecnico
                },
                {
                    id: 'seguranca',
                    icon: 'heroicons_outline:lock-closed',
                    title: 'seguranca',
                    hidden: false
                }
            ];

            // Subscribe to media changes
            this._fuseMediaWatcherService.onMediaChange$
                .pipe(takeUntil(this._unsubscribeAll))
                .subscribe(({ matchingAliases }) => {

                    // Set the drawerMode and drawerOpened
                    if (matchingAliases.includes('lg')) {
                        this.drawerMode = 'side';
                        this.drawerOpened = true;
                    }
                    else {
                        this.drawerMode = 'over';
                        this.drawerOpened = false;
                    }

                    // Mark for check
                    this._changeDetectorRef.markForCheck();
                });

            this._cdr.detectChanges();
        });
    }

    async getInformacoesUsuario() {
        this.usuario = await this._userSvc.obterPorCodigo(this.userSession.usuario.codUsuario).toPromise();
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    goToPanel(panel: string): void {
        this.loading = true;
        this.selectedPanel = panel;

        // Close the drawer on 'over' mode
        if (this.drawerMode === 'over') {
            this.drawer.close();
        }
    }

    getPanelInfo(id: string): any {
        return this.panels.find(panel => panel.id === id);
    }

    trackByFn(index: number, item: any): any {
        return item.id || index;
    }
}
