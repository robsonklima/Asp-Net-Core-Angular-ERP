import { ChangeDetectionStrategy, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    selector       : 'configuracoes-conta',
    templateUrl    : './conta.component.html',
    encapsulation  : ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ConfiguracoesContaComponent implements OnInit
{
    accountForm: FormGroup;
    
    constructor(
        private _formBuilder: FormBuilder
    ) {}
    
    ngOnInit(): void
    {
        // Create the form
        this.accountForm = this._formBuilder.group({
            nome: ['Robson Lima'],
            codUsuario: ['rklima'],
            title: ['Senior Frontend Developer'],
            company: ['YXZ Software'],
            about   : ['Hey! This is Brian; husband, father and gamer. I\'m mostly passionate about bleeding edge tech and chocolate! 🍫'],
            email   : ['hughes.brian@mail.com', Validators.email],
            phone   : ['121-490-33-12'],
            country : ['usa'],
            language: ['english']
        });
    }
}
