import { OnInit, Component, ViewEncapsulation } from '@angular/core';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { MonitoramentoService } from 'app/core/services/monitoramento.service';
import { MonitoramentoClienteViewModel } from 'app/core/types/monitoramento.type';

@Component({
    selector: 'default',
    templateUrl: './default.component.html',
    encapsulation: ViewEncapsulation.None
})


export class DefaultComponent {
    sessionData: UsuarioSessao;
    public loading: boolean;
    public listaMonitoramentoClientes: MonitoramentoClienteViewModel[] = [];

    constructor(
        private _userService: UserService,
        private _monitoramentoService: MonitoramentoService
    ) {
        this.sessionData = JSON.parse(this._userService.userSession);
    }

    ngOnInit(): void {
        this.loading = true;
        this._monitoramentoService.obterListaMonitoramentoClientes().subscribe(data => {
            this.listaMonitoramentoClientes = data;
            this.loading = false;
        });
    }
}