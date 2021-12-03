import { Injectable } from '@angular/core';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { OrdemServico } from 'app/core/types/ordem-servico.types';

@Injectable({
    providedIn: 'root'
})
export class AgendaTecnicoValidator
{
    constructor (private _tecnicoService: TecnicoService) { }

    /**
     * valida se a região do chamado e a região do técnico são iguais
     */
    public async isRegiaoAtendimentoValida(os: OrdemServico, codTecnico: number)
    {
        var tecnico = (await this._tecnicoService.obterPorCodigo(codTecnico).toPromise());

        if (tecnico?.codRegiao == os.codRegiao)
            return true;

        return false;
    }
}