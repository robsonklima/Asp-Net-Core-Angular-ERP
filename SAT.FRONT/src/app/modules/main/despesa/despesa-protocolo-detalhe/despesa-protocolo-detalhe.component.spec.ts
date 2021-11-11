import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DespesaProtocoloDetalheComponent } from './despesa-protocolo-detalhe.component';

describe('DespesaProtocoloDetalheComponent', () => {
  let component: DespesaProtocoloDetalheComponent;
  let fixture: ComponentFixture<DespesaProtocoloDetalheComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DespesaProtocoloDetalheComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DespesaProtocoloDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
