import { OrdemServico, OrdemServicoRelatorioInstalacaoItem } from 'app/core/types/ordem-servico.types';
import { ordemServicoRoutes } from 'app/modules/main/ordem-servico/ordem-servico.routing';
import { OrdemServicoRelatorioInstalacao } from './../../../core/types/ordem-servico.types';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Subject } from 'rxjs';
import Enumerable from 'linq';
import { RelatorioInstalacaoService } from 'app/core/services/relatorio-instalacao.service';
import { RelatorioInstalacaoItemService } from 'app/core/services/relatorio-instalacao-item.service';
import { RelatorioInstalacaoNaoConformidadeService } from 'app/core/services/relatorio-instalacao-nao-conformidade.service';

@Component({
  selector: 'app-relatorio-instalacao',
  templateUrl: './relatorio-instalacao.component.html',
})
export class RelatorioInstalacaoComponent implements AfterViewInit {
  form: FormGroup;
  formNaoConforme: FormGroup;
  codOS: number;
  ordemServico: OrdemServico;
  relInstalacao: any;
  relatorioItens: any;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _router: Router,
    private _route: ActivatedRoute,
    private _relatorioInstalacaoService: RelatorioInstalacaoService,
    private _relatorioInstalacaoNaoConformidadeService: RelatorioInstalacaoNaoConformidadeService,
    private _relatorioInstalacaoItemService: RelatorioInstalacaoItemService,
    private _snack: CustomSnackbarService,
    fb: FormBuilder
  ) {
    this.form = fb.group({
      1: [undefined, [Validators.required]],
      2: [undefined, [Validators.required]],
      3: [undefined, [Validators.required]],
      4: [undefined, [Validators.required]],
      5: [undefined, [Validators.required]],
      6: [undefined, [Validators.required]],
      7: [undefined, [Validators.required]],
    });

  }

  async ngAfterViewInit() {
    this.relatorioItens = await this._relatorioInstalacaoItemService.obter().toPromise();
    this.codOS = +this._route.snapshot.paramMap.get('codOS');
  }


  salvar() {

    const data = Enumerable.from(this.form.controls).toArray();
    let naoConfObj = data.pop();
    console.log(naoConfObj);

    data.forEach(e => {
      let obj: OrdemServicoRelatorioInstalacao = {
        codOS: this.codOS,
        codOSRelatorioInstalacaoItem: +e.key.slice(-1),
        indStatus: +e.value.value
      }

      this._relatorioInstalacaoService.criar(obj).subscribe();
    });

    this._relatorioInstalacaoNaoConformidadeService.criar({
      codOS: this.codOS,
      codOSRelatorioInstalacaoNaoConformidadeItem: +naoConfObj.key,
      indStatus: +naoConfObj.value.value
    }).subscribe((rel) => {
      this._snack.exibirToast("Registro adicionado com sucesso!", "success");
    });

    this._router.navigate(['ordem-servico/detalhe/' + this.codOS]);
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}
