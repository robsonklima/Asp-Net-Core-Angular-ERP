import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { LaboratorioService } from 'app/core/services/laboratorio.service';
import { ViewLaboratorioTecnicoBancada } from 'app/core/types/laboratorio.types';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { fromEvent } from 'rxjs';
import Enumerable from 'linq';
import moment from 'moment';

@Component({
  selector: 'app-painel-controle-tecnicos',
  templateUrl: './painel-controle-tecnicos.component.html'
})
export class PainelControleTecnicosComponent implements OnInit {
  tecnicos: ViewLaboratorioTecnicoBancada[] = [];
  tecnicosFiltered: ViewLaboratorioTecnicoBancada[] = [];
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  somenteAtrasos: boolean;
  loading: boolean = true;

  constructor(
    private _laboratorioService: LaboratorioService
  ) { }

  async ngOnInit() {
    this.tecnicos = await this._laboratorioService.obterTecnicosBancada().toPromise();
    this.tecnicosFiltered = Enumerable.from(this.tecnicos).orderBy(i => i.numBancada).toArray();
    console.log(this.tecnicosFiltered);
    
    this.registrarEmitters();
    this.loading = false;
  }

  private registrarEmitters() {
    fromEvent(this.searchInputControl.nativeElement, 'keyup').pipe(
			map((event: any) => {
				return event.target.value;
			})
			, debounceTime(1000)
			, distinctUntilChanged()
		).subscribe((text: string) => {
			this.tecnicosFiltered = Enumerable
        .from(this.tecnicos)
        .where(i => i.numBancada == +text || i.nome.toLowerCase().includes(text.toLowerCase()))
        .orderBy(i => i.numBancada)
        .toArray();
		});
  }

  public onToggleAtraso(event: any) {
    if (event.checked) {
      this.tecnicosFiltered = Enumerable
        .from(this.tecnicos)
        .where(i => i.statusReparo == 'ATRASADO' && !i.tempoOcioso)
        .orderBy(i => i.numBancada)
        .toArray();
    } else {
      this.tecnicosFiltered = Enumerable
        .from(this.tecnicos)
        .orderBy(i => i.numBancada)
        .toArray();
    }
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
