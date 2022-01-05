import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Erro0Component } from './erro-0.component';
import { erro0Routes } from './erro-0.routing';

@NgModule({
    declarations: [
        Erro0Component
    ],
    imports     : [
        RouterModule.forChild(erro0Routes)
    ]
})
export class Erro0Module
{
}
