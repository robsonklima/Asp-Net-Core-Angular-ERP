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

export class AgendaTecnicoOrdenacaoDialogComponent implements OnInit {
  form: FormGroup;
  ordenacoes: any = [];
  isLoading: boolean = false;
  tecnico: Tecnico;
  weekStart: string;
  weekEnd: string;

  constructor(
    @Inject(
      MAT_DIALOG_DATA) private data: any,
    private _formBuilder: FormBuilder,
  ) {
    if (data)
    {
      this.tecnico = data.tecnico;
      this.weekStart = data.weekStart;
      this.weekEnd = data.weekEnd;
    }
  }

  ngOnInit(): void {
    this.criarForm();
    this.obterOrdenacoes();
  }

  private obterOrdenacoes(): void {
    Object.keys(AgendaTecnicoOrdenationEnum).map(key => parseInt(key)).forEach(key => {
      if (!(Number.isNaN(key)))
        this.ordenacoes.push({
          codOrdenacao: key,
          nomeOrdenacao: key == 1 ? "FIM DE SLA" : "MENOR TRAGETÓRIA"
        })
    });

    this.ordenacoes =
      Enumerable.from(this.ordenacoes).orderBy(i => (i as any).nomeOrdenacao).toArray();
  }

  criarForm() {
    this.form = this._formBuilder.group({
      codOrdenacao: [undefined, [Validators.required]]
    });
  }

  async confirmar() {

  }
}