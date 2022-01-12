import { ChangeDetectionStrategy, ChangeDetectorRef, Component, EventEmitter, OnDestroy, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CidadeService } from 'app/core/services/cidade.service';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { GoogleGeolocationService } from 'app/core/services/google-geolocation.service';
import { PaisService } from 'app/core/services/pais.service';
import { UnidadeFederativaService } from 'app/core/services/unidade-federativa.service';
import { Cidade } from 'app/core/types/cidade.types';
import { Pais } from 'app/core/types/pais.types';
import { UnidadeFederativa } from 'app/core/types/unidade-federativa.types';
import { Usuario } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { Subject } from 'rxjs';
import { debounceTime, delay, filter, map, takeUntil } from 'rxjs/operators';

@Component({
    selector: 'informacoes-pessoais',
    templateUrl: './informacoes-pessoais.component.html',
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})

export class InformacoesPessoaisComponent implements OnInit, OnDestroy {

    @Output() respostaPanel = new EventEmitter();
    carregado: boolean;
    formInformacoes: FormGroup;
    userSession: UserSession;
    usuario: Usuario;
    cidadeFiltro: FormControl = new FormControl();

    public buscandoCEP: boolean = false;
    public paises: Pais[] = [];
    public unidadesFederativas: UnidadeFederativa[] = [];
    public cidades: Cidade[] = [];

    protected _onDestroy = new Subject<void>();

    constructor(
        private _formBuilder: FormBuilder,
        private _userService: UserService,
        private _googleGeolocationService: GoogleGeolocationService,
        private _unidadeFederativaService: UnidadeFederativaService,
        private _cidadeService: CidadeService,
        private _paisService: PaisService,
        private _cdr: ChangeDetectorRef,
        private _snack: CustomSnackbarService
    ) {
        this.userSession = JSON.parse(this._userService.userSession);
    }

    ngOnDestroy() {
        this._onDestroy.next();
        this._onDestroy.complete();
    }

    async ngOnInit() {
        this.buscandoCEP = false;
        this.carregado = false;
        this.inicializarForm();
        this.paises = await this._paisService.obterPaises();

        this.formInformacoes.controls['codPais'].valueChanges.subscribe(async () => {
            this.unidadesFederativas = [];
            this.unidadesFederativas = await this._unidadeFederativaService.obterUnidadesFederativas(this.formInformacoes.controls['codPais'].value);
            this._cdr.detectChanges();
        });

        this.formInformacoes.controls['codUF'].valueChanges.subscribe(async () => {
            this.cidades = [];
            this.cidades = await this._cidadeService.obterCidades(this.formInformacoes.controls['codUF'].value);
            this._cdr.detectChanges();
        });

        this.cidadeFiltro.valueChanges.pipe(
            filter(filtro => !!filtro),
            debounceTime(700),
            delay(500),
            takeUntil(this._onDestroy),
            map(async filtro => {
                this.cidades = await this._cidadeService.obterCidades(this.formInformacoes.controls['codUF'].value, filtro);
                this._cdr.detectChanges();
            })
        ).toPromise();

        this._userService.obterPorCodigo(this.userSession.usuario.codUsuario).subscribe(dadosUsuario => {
            this.formInformacoes.patchValue(dadosUsuario);
            this.formInformacoes.controls['codPais'].setValue(dadosUsuario?.cidade?.unidadeFederativa?.pais?.codPais);
            this.formInformacoes.controls['codUF'].setValue(dadosUsuario?.cidade?.unidadeFederativa?.codUF);
            this.formInformacoes.controls['codCidade'].setValue(dadosUsuario?.cidade?.codCidade);

            this.usuario = dadosUsuario;
            this.carregado = true;
            this.respostaPanel.emit(this.carregado); this._cdr.detectChanges();
        });
    }

    private inicializarForm(): void {
        this.formInformacoes = this._formBuilder.group({
            codUsuario: [
                {
                    value: undefined,
                    disabled: true
                }, Validators.required
            ],
            nomeUsuario: [undefined, [Validators.required]],
            cpf: [undefined, [Validators.required]],
            endereco: [undefined, [Validators.required]],
            bairro: [undefined, [Validators.required]],
            cep: [undefined, [Validators.required]],
            codUF: [undefined, [Validators.required]],
            codPais: [undefined, [Validators.required]],
            codCidade: [undefined, Validators.required],
            email: [undefined],
            fone: [undefined],
            ramal: [undefined]
        });
    }

    async buscaCEP(cepCmp: any) {
        if (cepCmp.target.value) {
            this.formInformacoes.disable();
            // Google
            // Tenta pelo cep (nem sempre os endereços são corretos)
            let mapService = (await this._googleGeolocationService.obterPorParametros
                ({ enderecoCep: cepCmp.target.value.replace(/\D+/g, '') }).toPromise()).results.shift();

            if (mapService) {
                this.formInformacoes.controls['endereco'].setValue(mapService.address_components[1].long_name);
                this.formInformacoes.controls['bairro'].setValue(mapService.address_components[2].long_name);
                this.formInformacoes.controls['codCidade'].setValue(mapService.dadosSAT.codCidade);
                this.formInformacoes.controls['codUF'].setValue(mapService.dadosSAT.codUF);
                this.formInformacoes.controls['codPais'].setValue(mapService.dadosSAT.codPais);
            }
            this.formInformacoes.enable();
            this._cdr.detectChanges();
        }
    }

    salvar() {
        this.formInformacoes.disable();
        const form: any = this.formInformacoes.getRawValue();

        let updateUsuario = {
            ...this.usuario,
            ...form
        };

        this._userService.atualizar(updateUsuario).subscribe(() => {
            this._snack.exibirToast(`Usuário atualizado com sucesso!`, "success");
            this.formInformacoes.enable();
        });
    }
}
