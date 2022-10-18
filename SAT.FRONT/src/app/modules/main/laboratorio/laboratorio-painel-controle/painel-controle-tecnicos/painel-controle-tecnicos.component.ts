import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, ViewChild } from '@angular/core';
import { BancadaLaboratorioService } from 'app/core/services/bancada-laboratorio.service';
import { ViewLaboratorioTecnicoBancada } from 'app/core/types/bancada-laboratorio.types';
import { debounceTime, distinctUntilChanged, map, startWith, takeUntil } from 'rxjs/operators';
import { fromEvent, interval, Subject } from 'rxjs';
import Enumerable from 'linq';
import moment from 'moment';
import { MatDialog } from '@angular/material/dialog';
import { PainelControleTecnicosItensComponent } from './painel-controle-tecnicos-itens/painel-controle-tecnicos-itens.component';

@Component({
  selector: 'app-painel-controle-tecnicos',
  templateUrl: './painel-controle-tecnicos.component.html'
})
export class PainelControleTecnicosComponent implements AfterViewInit {
  tecnicos: ViewLaboratorioTecnicoBancada[] = [];
  tecnicosFiltered: ViewLaboratorioTecnicoBancada[] = [];
  @ViewChild('searchInputControl') searchInputControl: ElementRef;
  somenteAtrasos: boolean;
  loading: boolean = true;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _bancadaLaboratorioService: BancadaLaboratorioService,
    public dialog: MatDialog,
    private _cdr: ChangeDetectorRef
  ) { }

  async ngAfterViewInit() {
    interval(3 * 60 * 1000)
			.pipe(
				startWith(0),
				takeUntil(this._onDestroy)
			)
			.subscribe(() => {
				this.obterDados();
			});
    
    this.registrarEmitters();
  }

  private async obterDados() {
    this.loading = true;
    this.tecnicos = await this._bancadaLaboratorioService.obterTecnicosBancada().toPromise();
    this.tecnicosFiltered = Enumerable.from(this.tecnicos).orderBy(i => i.numBancada).toArray();
    this.loading = false;
    this._cdr.detectChanges();
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

  public abrirItens(tecBancada: ViewLaboratorioTecnicoBancada) {
    this.dialog.open(PainelControleTecnicosItensComponent, {
      data: {
        tecnicoBancada: tecBancada,
      },
    });
  }

  ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
