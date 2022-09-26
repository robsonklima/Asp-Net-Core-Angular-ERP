import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Auditoria } from 'app/core/types/auditoria.types';
import { first } from 'rxjs/operators';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { AuditoriaService } from 'app/core/services/auditoria.service';
import { FormGroup } from '@angular/forms';
import { AuditoriaFoto } from 'app/core/types/auditoria-foto.types';
import { Foto } from 'app/core/types/foto.types';
import { MatDialog } from '@angular/material/dialog';
import { AuditoriaFotoDialogComponent } from './auditoria-foto-dialog/auditoria-foto-dialog.component';

@Component({
  selector: 'app-auditoria-foto',
  templateUrl: './auditoria-foto.component.html'
})
export class AuditoriaFotoComponent implements OnInit {

  codAuditoria: number;
  auditoria: Auditoria;
  userSession: UsuarioSessao;
  form: FormGroup;
  fotos: AuditoriaFoto;

  constructor(
    private _route: ActivatedRoute,
    private _auditoriaService: AuditoriaService,
    private _userService: UserService,
    private _dialog: MatDialog
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  ngOnInit(): void {
    this.codAuditoria = +this._route.snapshot.paramMap.get('codAuditoria');

    this._auditoriaService.obterPorCodigo(this.codAuditoria)
      .pipe(first())
      .subscribe(data => {

        this.auditoria = data;
      });
  }

  ampliarFoto(foto: Foto) {
		this._dialog.open(AuditoriaFotoDialogComponent, {
			data: { foto: foto}
		});
	}

}
