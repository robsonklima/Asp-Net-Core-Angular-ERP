import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';
import { AgendaTecnicoOrdenationEnum } from 'app/core/types/agenda-tecnico.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import Enumerable from 'linq';

@Component({
  selector: 'app-agenda-tecnico-ordenacao-dialog',
  templateUrl: './agenda-tecnico-ordenacao-dialog.component.html',
  styleUrls: ['./agenda-tecnico-ordenacao-dialog.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})

export class AgendaTecnicoOrdenacaoDialogComponent implements OnInit
{
  form: FormGroup;
  ordenacoes: any = [];
  isLoading: boolean = false;
  tecnico: Tecnico;
  weekStart: string;
  weekEnd: string;

  constructor (@Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef: MatDialogRef<AgendaTecnicoOrdenacaoDialogComponent>,
    private _formBuilder: FormBuilder,
    private _agendaTecnicoService: AgendaTecnicoService) 
  {
    if (data)
    {
      this.tecnico = data.tecnico;
      this.weekStart = data.weekStart;
      this.weekEnd = data.weekEnd;
    }
  }

  ngOnInit(): void
  {
    this.criarForm();
    this.obterOrdenacoes();
  }

  private obterOrdenacoes(): void
  {
    Object.keys(AgendaTecnicoOrdenationEnum).filter(element => isNaN(Number(element))).forEach((tr, i) =>
    {
      this.ordenacoes.push({
        codOrdenacao: i,
        nomeOrdenacao: i == AgendaTecnicoOrdenationEnum.FIM_SLA ? "FIM DE SLA" : "MENOR TRAGETÓRIA"
      })
    });

    this.ordenacoes =
      Enumerable.from(this.ordenacoes).orderBy(i => (i as any).nomeOrdenacao);
  }

  criarForm()
  {
    this.form = this._formBuilder.group({
      codOrdenacao: [undefined, [Validators.required]]
    });
  }

  private ordernar()
  {
    this.isLoading = true;
    this._agendaTecnicoService.ordenarAgendaTecnico({
      codTecnico: this.tecnico.codTecnico,
      codUsuario: this.tecnico.usuario.codUsuario,
      inicioPeriodoAgenda: this.weekStart,
      fimPeriodoAgenda: this.weekEnd,
      ordenacao: this.form.controls.codOrdenacao.value,
      sortActive: 'nome',
      sortDirection: 'asc'
    }).toPromise().then(() =>
    {
      this.isLoading = false;
      this.dialogRef.close(true);
    });
  }

  async confirmar()
  {
    this.ordernar();
  }
}