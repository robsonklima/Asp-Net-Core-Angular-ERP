import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { Usuario, UsuarioParameters, UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';
import { Location } from '@angular/common';
import { statusConst } from 'app/core/types/status-types';
import { Auditoria } from 'app/core/types/auditoria.types';
import { AuditoriaService } from 'app/core/services/auditoria.service';
import { AuditoriaVeiculo } from 'app/core/types/auditoria-veiculo.types';
import { UsuarioService } from 'app/core/services/usuario.service';

@Component({
	selector: 'app-auditoria-utilizacao',
	templateUrl: './auditoria-utilizacao.component.html',
})
export class AuditoriaUtilizacaoComponent implements OnInit {

    async ngOnInit() {}

}
