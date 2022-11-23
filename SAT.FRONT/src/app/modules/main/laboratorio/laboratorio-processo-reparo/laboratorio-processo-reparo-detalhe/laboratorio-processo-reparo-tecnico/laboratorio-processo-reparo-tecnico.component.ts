import { Component, Input, OnInit } from '@angular/core';
import { BancadaLaboratorioService } from 'app/core/services/bancada-laboratorio.service';
import { ViewLaboratorioTecnicoBancada } from 'app/core/types/bancada-laboratorio.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';

@Component({
  selector: 'app-laboratorio-processo-reparo-tecnico',
  templateUrl: './laboratorio-processo-reparo-tecnico.component.html'
})
export class LaboratorioProcessoReparoTecnicoComponent implements OnInit {
  @Input() codTecnico: string;
  tecnicoBancada: ViewLaboratorioTecnicoBancada;
  userSession: UserSession;
  loading: boolean = true;

  constructor(
    private _userService: UserService,
    private _bancadaLaboratorioService: BancadaLaboratorioService,
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
    console.log(this.codTecnico);
    
  }

  async ngOnInit() {
    const tecnicosBancada = await this._bancadaLaboratorioService
      .obterPorView({ codUsuario: this.codTecnico })
      .toPromise();

    this.tecnicoBancada = tecnicosBancada.shift();
    this.loading = false;
  }

  public obterEvolucaoReparo(tecnico: ViewLaboratorioTecnicoBancada) {
    if (!tecnico.tempoEmReparo || !tecnico.tempoEmReparo || tecnico.tempoReparoPeca == '00:00')
      return 0;

    const tempoEmReparo = moment.duration(tecnico.tempoEmReparo).asMinutes();
    const tempoReparoPeca = moment.duration(tecnico.tempoReparoPeca).asMinutes();
    const percentualEvolucao = (tempoEmReparo / tempoReparoPeca * 100).toFixed(2)
    return +percentualEvolucao <= 100 ? percentualEvolucao : 100;
  }
}
