import { ChangeDetectorRef, Component, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DespesaItemService } from 'app/core/services/despesa-item.service';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { Cidade } from 'app/core/types/cidade.types';
import { DespesaPeriodoTecnico, DespesaPeriodoTecnicoStatusEnum } from 'app/core/types/despesa-periodo.types';
import { Despesa, DespesaItem, DespesaTipoEnum } from 'app/core/types/despesa.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import Enumerable from 'linq';
import moment from 'moment';
import { Subject } from 'rxjs';
import { takeUntil, debounceTime, filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-despesa-atendimento-reprovacao-lista',
  templateUrl: './despesa-atendimento-reprovacao-lista.component.html',
  styles: [`.list-grid-despesa-atendimento-reprovacao {
            grid-template-columns: 80px 80px auto 100px 75px 75px;
            @screen sm { grid-template-columns: 80px 80px auto 100px 75px 75px; }
            @screen md { grid-template-columns: 80px 80px auto 100px 75px 75px; }
            @screen lg { grid-template-columns: 80px 80px auto 100px 75px 75px; }
        }
    `],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})

export class DespesaAtendimentoReprovacaoListaComponent implements OnInit
{
  protected _onDestroy = new Subject<void>();
  despesaPeriodoTecnico: DespesaPeriodoTecnico;
  despesas: Despesa[] = [];
  despesaItens: DespesaItem[] = [];
  codDespesaPeriodoTecnico: number;
  despesaItemSelecionada: DespesaItem;
  despesaSelecionadaForm: FormGroup;
  public userSession: UserSession;
  isLoading: boolean = false;
  showObservacao: boolean = false;

  constructor (private _despesaPeriodoTecnicoService: DespesaPeriodoTecnicoService,
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _userSvc: UserService,
    private _changeDetectorRef: ChangeDetectorRef,
    private _despesaItemSvc: DespesaItemService,
    private _snack: CustomSnackbarService,
    private _dialog: MatDialog)
  {
    this.codDespesaPeriodoTecnico = +this._route.snapshot.paramMap.get('codDespesaPeriodoTecnico');
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  async ngOnInit()
  {
    await this.getDespesas();
    this.criarForm();
    this.registerEmitters();
  }

  private criarForm()
  {
    this.despesaSelecionadaForm = this._formBuilder.group({
      dataInicio: [undefined],
      dataSolucao: [undefined],
      valorUnitario: [undefined],
      notaFiscal: [undefined],
      kmPercorrido: [undefined],
      kmPrevisto: [undefined],
      obs: [undefined],
      motivo: [undefined],
      obsReprovacao: [undefined],
      origem: [undefined],
      destino: [undefined],
      indReprovado: [undefined]
    });
  }

  private registerEmitters()
  {
    this.despesaSelecionadaForm.controls['indReprovado'].valueChanges.subscribe(async () =>
    {
      var currentIndReprovacaoValue =
        this.despesaItemSelecionada.indReprovado == 1 ? true : false;

      var formIndReprovacaoValue =
        this.despesaSelecionadaForm.controls['indReprovado'].value;

      if (formIndReprovacaoValue)
        this.showObservacao = true;
      else this.showObservacao = false;

      if (formIndReprovacaoValue != currentIndReprovacaoValue)
      {
        this.despesaItemSelecionada.indReprovado = this.despesaSelecionadaForm.controls['indReprovado'].value ? 1 : 0;
        this.despesaItemSelecionada.dataHoraManut = moment().format('yyyy-MM-DD HH:mm:ss');
        this.despesaItemSelecionada.codUsuarioManut = this.userSession.usuario.codUsuario;
        await this._despesaItemSvc.atualizar(this.despesaItemSelecionada).toPromise();
      }
    });

    this.despesaSelecionadaForm.controls['obsReprovacao'].valueChanges.pipe(
      filter(query => !!query),
      takeUntil(this._onDestroy),
      debounceTime(700),
      map(async () =>
      {
        var currentObsReprovacaoValue =
          this.despesaItemSelecionada.obsReprovacao;

        var formObsReprovacaoValue =
          this.despesaSelecionadaForm.controls['obsReprovacao'].value;

        if (this.showObservacao && currentObsReprovacaoValue != formObsReprovacaoValue)
        {
          this.despesaItemSelecionada.obsReprovacao = this.despesaSelecionadaForm.controls['obsReprovacao'].value;
          this.despesaItemSelecionada.dataHoraManut = moment().format('yyyy-MM-DD HH:mm:ss');
          this.despesaItemSelecionada.codUsuarioManut = this.userSession.usuario.codUsuario;
          await this._despesaItemSvc.atualizar(this.despesaItemSelecionada).toPromise();
        }
      }),
      takeUntil(this._onDestroy)
    ).subscribe();
  }

  async getDespesas()
  {
    this.isLoading = true;

    this.despesaPeriodoTecnico =
      (await this._despesaPeriodoTecnicoService.obterPorCodigo(this.codDespesaPeriodoTecnico).toPromise());

    this.despesas =
      this.despesaPeriodoTecnico.despesas;

    this.despesaItens = Enumerable.from(this.despesas)
      .selectMany(i => i.despesaItens)
      .toArray();

    this.isLoading = false;
  }


  async toggleDetails(codDespesaItem: number)
  {
    if (this.despesaItemSelecionada && this.despesaItemSelecionada.codDespesaItem === codDespesaItem)
    {
      this.closeDetails();
      return;
    }

    var despesaItem =
      Enumerable.from(this.despesas)
        .selectMany(i => i.despesaItens).
        firstOrDefault(i => i.codDespesaItem == codDespesaItem);

    this.despesaItemSelecionada = despesaItem;
    await this.populateForm(despesaItem);

    this._changeDetectorRef.markForCheck();
  }

  async populateForm(despesaItem: DespesaItem)
  {
    var despesa = Enumerable.from(this.despesas)
      .firstOrDefault(i => Enumerable.from(i.despesaItens)
        .contains(despesaItem));

    this.despesaSelecionadaForm.controls['dataInicio']
      .setValue(moment(despesa.relatorioAtendimento.dataHoraInicio).format('DD/MM/YY HH:mm'));

    this.despesaSelecionadaForm.controls['dataSolucao']
      .setValue(moment(despesa.relatorioAtendimento.dataHoraSolucao).format('DD/MM/YY HH:mm'));

    this.despesaSelecionadaForm.controls['valorUnitario']
      .setValue(despesaItem.despesaValor);

    this.despesaSelecionadaForm.controls['notaFiscal']
      .setValue(despesaItem.numNF ?? "Não consta");

    this.despesaSelecionadaForm.controls['indReprovado']
      .setValue(despesaItem.indReprovado == 1 ? true : false);

    this.despesaSelecionadaForm.controls['notaFiscal']
      .setValue(despesaItem.obs);

    this.despesaSelecionadaForm.controls['obsReprovacao']
      .setValue(despesaItem.obsReprovacao);

    if (despesaItem.codDespesaTipo != DespesaTipoEnum.KM) return;

    this.despesaSelecionadaForm.controls['kmPercorrido']
      .setValue(despesaItem.kmPercorrido);

    this.despesaSelecionadaForm.controls['kmPrevisto']
      .setValue(despesaItem.kmPrevisto);

    this.despesaSelecionadaForm.controls['destino']
      .setValue(this.populateAddress(despesaItem.enderecoDestino, despesaItem.bairroDestino, despesaItem.numDestino, despesaItem.cidadeDestino));

    this.despesaSelecionadaForm.controls['origem']
      .setValue(this.populateAddress(despesaItem.enderecoOrigem, despesaItem.bairroOrigem, despesaItem.numOrigem, despesaItem.cidadeOrigem));
  }

  closeDetails(): void
  {
    this.despesaItemSelecionada = null;
  }

  private populateAddress(endereco: string, bairro: string, numero: string, cidade: Cidade): string
  {
    var enderecoFormatado = "";

    if (endereco)
      enderecoFormatado += endereco + ", ";

    if (bairro)
      enderecoFormatado += bairro + ", ";

    if (numero)
      enderecoFormatado += numero;

    if (cidade?.nomeCidade)
      enderecoFormatado += ", " + cidade.nomeCidade;

    if (cidade?.unidadeFederativa?.siglaUF)
      enderecoFormatado += " - " + cidade.unidadeFederativa.siglaUF;

    return enderecoFormatado.toUpperCase();
  }

  async aprovarPeriodoTecnico()
  {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: 'Deseja aprovar este período?',
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
    {
      if (confirmacao)
      {
        if (this.temReprovacoes())
        {
          this._snack.exibirToast('Para aprovar um periodo, todas as despesas precisam ser aprovadas.', 'error');
          return;
        }

        this.despesaPeriodoTecnico.codDespesaPeriodoTecnicoStatus = parseInt(DespesaPeriodoTecnicoStatusEnum.APROVADO);

        this._despesaPeriodoTecnicoService.atualizar(this.despesaPeriodoTecnico).subscribe(() =>
        {
          this._snack.exibirToast('Período aprovado com sucesso!', 'success');
        },
          e =>
          {
            this.despesaPeriodoTecnico.codDespesaPeriodoTecnicoStatus = parseInt(DespesaPeriodoTecnicoStatusEnum['LIBERADO PARA ANÁLISE']);
            this._snack.exibirToast('Erro ao aprovar período.', 'error');
          })
      }
    });
  }

  async reprovarPeriodoTecnico()
  {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: 'Deseja reprovar este período?',
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) =>
    {
      if (confirmacao)
      {
        if (!this.temReprovacoes())
        {
          this._snack.exibirToast('Para reprovar um periodo, uma ou mais despesas precisam ser reprovadas.', 'error');
          return;
        }

        this.despesaPeriodoTecnico.codDespesaPeriodoTecnicoStatus = parseInt(DespesaPeriodoTecnicoStatusEnum.REPROVADO);

        this._despesaPeriodoTecnicoService.atualizar(this.despesaPeriodoTecnico).subscribe(() =>
        {
          this._snack.exibirToast('Período reprovado com sucesso!', 'success');
        }, e =>
        {
          this.despesaPeriodoTecnico.codDespesaPeriodoTecnicoStatus = parseInt(DespesaPeriodoTecnicoStatusEnum['LIBERADO PARA ANÁLISE']);
          this._snack.exibirToast('Erro ao reprovar período.', 'error');
        })
      }
    });
  }

  async showInMap(despesaItem: DespesaItem)
  {
    var destino =
      this.populateAddress(despesaItem.enderecoDestino, despesaItem.bairroDestino,
        despesaItem.numDestino, despesaItem.cidadeDestino);

    var origem =
      this.populateAddress(despesaItem.enderecoOrigem, despesaItem.bairroOrigem,
        despesaItem.numOrigem, despesaItem.cidadeOrigem);

    var windowPopup =
      window.open(`https://www.google.com.br/maps/dir/${origem}/${destino}`, '_blank', 'width=800,height=800');

    windowPopup.open();
  }

  private temReprovacoes(): boolean
  {
    return Enumerable.from(this.despesaItens).any(i => i.indReprovado == 1);
  }
}