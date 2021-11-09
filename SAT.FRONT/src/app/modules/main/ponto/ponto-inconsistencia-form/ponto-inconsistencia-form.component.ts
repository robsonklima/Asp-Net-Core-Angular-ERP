import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PontoUsuarioDataMotivoDivergenciaService } from 'app/core/services/ponto-usuario-data-motivo-divergencia.service';
import { PontoUsuarioDataTipoAdvertenciaService } from 'app/core/services/ponto-usuario-data-tipo-advertencia.service';
import { PontoUsuarioDataMotivoDivergencia } from 'app/core/types/ponto-usuario-data-motivo-divergencia.types';
import { PontoUsuarioDataTipoAdvertencia } from 'app/core/types/ponto-usuario-data-tipo-advertencia.types';
import { PontoHorariosListaComponent } from '../ponto-horarios-lista/ponto-horarios-lista.component';

@Component({
  selector: 'app-ponto-inconsistencia-form',
  templateUrl: './ponto-inconsistencia-form.component.html'
})
export class PontoInconsistenciaFormComponent implements OnInit {
  form: FormGroup;
  motivosDivergencia: PontoUsuarioDataMotivoDivergencia[] = [];
  tiposAdvertencia: PontoUsuarioDataTipoAdvertencia[] = [];

  constructor(
    private _pontoUsuarioDataMorivoDivergenciaSvc: PontoUsuarioDataMotivoDivergenciaService,
    private _pontoUsuarioDataTipoAdvertenciaSvc: PontoUsuarioDataTipoAdvertenciaService,
    public dialogRef: MatDialogRef<PontoHorariosListaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _formBuilder: FormBuilder
  ) { }

  async ngOnInit() {
    this.form = this._formBuilder.group({
      codPontoUsuarioDataMotivoDivergencia: [
        {
          value: undefined,
        }, [Validators.required]
      ],
      codPontoUsuarioDataTipoAdvertencia: [
        {
          value: undefined,
        }, [Validators.required]
      ],
    });

    const motivos = await this._pontoUsuarioDataMorivoDivergenciaSvc.obterPorParametros({}).toPromise();
    this.motivosDivergencia = motivos.items;

    const tipos = await this._pontoUsuarioDataTipoAdvertenciaSvc.obterPorParametros({}).toPromise();
    this.tiposAdvertencia = tipos.items;
  }

  salvar(): void {
    // const form = this.form.getRawValue();
    // let dataHora = moment(`${form.data.format('YYYY-MM-DD')} ${form.hora}`).format('YYYY-MM-DD HH:mm');
    // let agendamento: Agendamento = {
    //   codOS: this.data.codOS,
    //   codMotivo: form.codMotivo,
    //   dataHoraUsuAgendamento: moment().format('YYYY-MM-DD HH:mm'),
    //   dataAgendamento: dataHora,
    //   codUsuarioAgendamento: this.userSession.usuario.codUsuario
    // }

    this.dialogRef.close();
  }

  fechar() {
    this.dialogRef.close();
  }
}
