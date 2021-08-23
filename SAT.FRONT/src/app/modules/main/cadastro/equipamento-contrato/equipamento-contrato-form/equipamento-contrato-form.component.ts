import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ClienteService } from 'app/core/services/cliente.service';
import { ContratoService } from 'app/core/services/contrato.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { EquipamentoContratoService } from 'app/core/services/equipamento-contrato.service';
import { EquipamentoService } from 'app/core/services/equipamento.service';
import { Cliente, ClienteParameters } from 'app/core/types/cliente.types';
import { Contrato, ContratoParameters } from 'app/core/types/contrato.types';
import { EquipamentoContrato } from 'app/core/types/equipamento-contrato.types';
import { Equipamento, EquipamentoParameters } from 'app/core/types/equipamento.types';
import { Subject } from 'rxjs';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-equipamento-contrato-form',
  templateUrl: './equipamento-contrato-form.component.html',
  encapsulation: ViewEncapsulation.None
})
export class EquipamentoContratoFormComponent implements OnInit, OnDestroy {
  codEquipContrato: number;
  isAddMode: boolean;
  form: FormGroup;
  equipamentoContrato: EquipamentoContrato;
  clientes: Cliente[] = [];
  contratos: Contrato[] = [];
  modelos: Equipamento[] = [];
  protected _onDestroy = new Subject<void>();

  constructor(
    private _formBuilder: FormBuilder,
    private _equipamentoContratoService: EquipamentoContratoService,
    private _contratoService: ContratoService,
    private _route: ActivatedRoute,
    private _snack: CustomSnackbarService,
    private _location: Location,
    private _clienteService: ClienteService,
    private _equipamentoService: EquipamentoService,
  ) {}

  async ngOnInit() {
    this.codEquipContrato = +this._route.snapshot.paramMap.get('codEquipContrato');
    this.isAddMode = !this.codEquipContrato;
    this.inicializarForm();
    this.obterClientes();
    this.obterModelos();

    this.form.controls['codCliente'].valueChanges.subscribe(async () => {
      this.obterContratos();
    });

    if (!this.isAddMode) {
      this._equipamentoContratoService.obterPorCodigo(this.codEquipContrato)
      .pipe(first())
      .subscribe(data => {
        this.form.patchValue(data);
        this.equipamentoContrato = data;
      });
    }
  }

  private inicializarForm() {
    this.form = this._formBuilder.group({
      codEquipContrato: [
        {
          value: undefined,
          disabled: true
        }, Validators.required
      ],
      numSerie: [undefined, Validators.required],
      codEquip: [undefined, [Validators.required]],
      codTipoEquip: [undefined, Validators.required],
      codGrupoEquip: [undefined, Validators.required],
      codCliente: [undefined, Validators.required],
      codSLA: [undefined, Validators.required],
      codContrato: [undefined, Validators.required],
      codPosto: [undefined, Validators.required],
      codFilial: [undefined, Validators.required],
      codRegiao: [undefined, Validators.required],
      codAutorizada: [undefined, Validators.required],
    });
  }

  private async obterClientes() {
    const params: ClienteParameters = {
      sortActive: 'nomeFantasia',
      sortDirection: 'asc',
      indAtivo: 1,
      pageSize: 300
    }

    const data = await this._clienteService.obterPorParametros(params).toPromise();
    this.clientes = data.clientes;
  }

  private async obterContratos() {
    const codCliente = this.form.controls['codCliente'].value;

    const params: ContratoParameters = {
      sortActive: 'nomeContrato',
      sortDirection: 'asc',
      indAtivo: 1,
      codCliente: codCliente,
      pageSize: 100
    }

    const data = await this._contratoService.obterPorParametros(params).toPromise();
    this.contratos = data.contratos;
  }

  private async obterModelos() {
    const params: EquipamentoParameters = {
      sortActive: 'nomeEquip',
      sortDirection: 'asc',
      pageSize: 1000
    }

    const data = await this._equipamentoService.obterPorParametros(params).toPromise();
    this.modelos = data.equipamentos;
  }

  salvar(): void {
    this.isAddMode ? this.criar() : this.atualizar();
  }

  atualizar(): void {
    const form: any = this.form.getRawValue();
    
    this._equipamentoContratoService.atualizar(form).subscribe(() => {
      this._snack.exibirToast("Registro atualizado com sucesso!", "success");
      this._location.back();
    });
  }

  criar(): void {
    const form: any = this.form.getRawValue();
    
    this._equipamentoContratoService.criar(form).subscribe(() => {
      this._snack.exibirToast("Registro inserido com sucesso!", "success");
      this._location.back();
    });
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
