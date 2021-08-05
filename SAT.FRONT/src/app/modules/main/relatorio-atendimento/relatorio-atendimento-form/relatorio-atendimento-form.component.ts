import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { StatusServicoService } from 'app/core/services/status-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { RelatorioAtendimento } from 'app/core/types/relatorio-atendimento.types';
import { StatusServico } from 'app/core/types/status-servico.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import { Usuario } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';

@Component({
  selector: 'app-relatorio-atendimento-form',
  templateUrl: './relatorio-atendimento-form.component.html'
})
export class RelatorioAtendimentoFormComponent implements OnInit {
  usuario: Usuario;
  codOS: number;
  codRAT: number;
  ordemServico: OrdemServico;
  relatorioAtendimento: RelatorioAtendimento;
  form: FormGroup;
  isAddMode: boolean;
  statusServicos: StatusServico[] = [];
  tecnicos: Tecnico[] = [];

  constructor(
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _router: Router,
    private _snack: CustomSnackbarService,
    private _relatorioAtendimentoService: RelatorioAtendimentoService,
    private _ordemServicoService: OrdemServicoService,
    private _userService: UserService,
    private _statusServicoService: StatusServicoService,
    private _tecnicoService: TecnicoService,
  ) {
    this.usuario = JSON.parse(this._userService.userSession).usuario;
  }

  ngOnInit(): void {
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
    this.codRAT = +this._route.snapshot.paramMap.get('codRAT');
    this.isAddMode = !this.codRAT;

    this.form = this._formBuilder.group({
      codRAT: [
        {
          value: undefined,
          disabled: true,
        }, [Validators.required]
      ],
      numRAT: [undefined],
      codTecnico: [undefined],
      codStatusServico: [undefined],
      nomeAcompanhante: [undefined],
      data: [
        {
          value: moment(),
          disabled: false,
        }, [Validators.required]
      ],
      horaInicio: [moment().format('HH:mm'), [Validators.required]],
      horaFim: [undefined, [Validators.required]],
      obsRAT: [undefined],
    });

    if (this.isAddMode) {
      this.relatorioAtendimento = new RelatorioAtendimento();
      this.relatorioAtendimento.codOS = this.codOS;
      this.relatorioAtendimento.relatorioAtendimentoDetalhes = [];
    }

    //this.obterOrdemServico();
    //this.obterStatusServicos();
  }
}
