import { SharedModule } from '../../../shared/shared.module';
import { PlanilhaComponent } from './planilha.component';
import { NgModule } from '@angular/core';



@NgModule({
  declarations: [
    PlanilhaComponent,
  ],
  imports: [
    SharedModule,
   
  ],
  exports:[
    PlanilhaComponent,
  ],
  bootstrap: [PlanilhaComponent]
})
export class PlanilhaModule { }
