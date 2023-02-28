import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ContratoService } from 'app/core/services/contrato.service';
import { InstalacaoPleitoService } from 'app/core/services/instalacao-pleito.service';
import { InstalacaoTipoPleitoService } from 'app/core/services/instalacao-tipo-pleito-service';
import { Contrato } from 'app/core/types/contrato.types';
import { InstalacaoPleito } from 'app/core/types/instalacao-pleito.types';
import { InstalacaoTipoPleito } from 'app/core/types/instalacao-tipo-pleito.types';
import { statusConst } from 'app/core/types/status-types';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-instalacao-pleito-form',
  templateUrl: './instalacao-pleito-form.component.html'
})
export class InstalacaoPleitoFormComponent implements OnInit {
  @Input() instalPleito: InstalacaoPleito;
  form: FormGroup;
  isAddMode: boolean;
  contratos: Contrato[] = [];
  tiposPleito: InstalacaoTipoPleito[] = [];
  contratoFilterCtrl: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formbuilder: FormBuilder,
    private _instalPleitoService: InstalacaoPleitoService,
    private _instalacaoTipoPleitoService: InstalacaoTipoPleitoService,
    private _contratoService: ContratoService
  ) {}

  ngOnInit(): void {
    this.isAddMode = !this.instalPleito;
    this.obterTiposPleito();
    this.obterContratos();
    this.inicializarForm();
    this.registrarEmitters();
  }
  
  registrarEmitters() {
    this.contratoFilterCtrl.valueChanges
			.pipe(
				takeUntil(this._onDestroy),
				debounceTime(400),
				distinctUntilChanged()
			)
			.subscribe((query) =>
			{
				this.obterContratos(query);
			});
  }

  async obterContratos(query: string="") {
    const data = await this._contratoService
      .obterPorParametros({ 
        sortActive: "NomeContrato", 
        sortDirection: "asc", 
        indAtivo: statusConst.ATIVO, 
        filter: query,
        pageSize: 50 })
      .toPromise();

    this.contratos = data?.items;
  }

  async obterTiposPleito() {
    const data = await this._instalacaoTipoPleitoService
      .obterPorParametros({ 
        sortActive: "NomeTipoPleito", 
        sortDirection: "asc" })
      .toPromise();

    this.tiposPleito = data?.items;
  }

  inicializarForm() {
    this.form = this._formbuilder.group({
      nomePleito: [undefined, Validators.required],
      descPleito: [undefined, Validators.required],
      codInstalTipoPleito: [undefined, Validators.required],
      codContrato: [undefined, Validators.required],
      dataEnvio: [undefined, Validators.required],
    });
  }

  salvar() {

  }

  ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}
