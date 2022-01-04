import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Erro404Component } from './erro-404.component';
import { erro404Routes } from './erro-404.routing';

@NgModule({
    declarations: [
        Erro404Component
    ],
    imports     : [
        RouterModule.forChild(erro404Routes)
    ]
})
export class Erro404Module
{
}
