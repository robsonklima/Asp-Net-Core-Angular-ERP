import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Erro403Component } from './erro-403.component';
import { erro403Routes } from './erro-403.routing';

@NgModule({
    declarations: [
        Erro403Component
    ],
    imports     : [
        RouterModule.forChild(erro403Routes)
    ]
})
export class Erro403Module
{
}
