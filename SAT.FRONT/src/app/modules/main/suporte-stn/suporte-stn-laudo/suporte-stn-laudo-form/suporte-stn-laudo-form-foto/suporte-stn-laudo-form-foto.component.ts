import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { FormGroup } from '@angular/forms';
import { AuditoriaFoto } from 'app/core/types/auditoria-foto.types';
import { Foto } from 'app/core/types/foto.types';
import { MatDialog } from '@angular/material/dialog';
import { Laudo } from 'app/core/types/laudo.types';
import { LaudoService } from 'app/core/services/laudo.service';
import { SuporteStnLaudoFormFotoDialogComponent } from './suporte-stn-laudo-form-foto-dialog/suporte-stn-laudo-form-foto-dialog.component';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';

@Component({
    selector: 'app-suporte-stn-laudo-form-foto',
    templateUrl: './suporte-stn-laudo-form-foto.component.html'
})
export class SuporteStnLaudoFormFotoComponent implements OnInit {

    codLaudo: number;
    laudo: Laudo;
    relatorioAtendimento: RelatorioAtendimento;
    userSession: UsuarioSessao;
    form: FormGroup;
    fotos: Foto[];

    constructor(
        private _route: ActivatedRoute,
        private _laudoService: LaudoService,
        private _relatorioAtendimentoService: RelatorioAtendimentoService,
        private _userService: UserService,
        private _dialog: MatDialog
    ) {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    async ngOnInit() {
        this.codLaudo = +this._route.snapshot.paramMap.get('codLaudo');

        this.laudo = await this._laudoService.obterPorCodigo(this.codLaudo).toPromise();
        console.log(this.laudo);
        
        this.obterRAT()
            
    }

    async obterRAT() {
        this.relatorioAtendimento = await this._relatorioAtendimentoService.obterPorCodigo(this.laudo.codRAT)
            .toPromise();
        console.log(this.relatorioAtendimento);
    }

    ampliarFoto(foto: Foto) {
        this._dialog.open(SuporteStnLaudoFormFotoDialogComponent, {
            data: { foto: foto }
        });
    }

}
