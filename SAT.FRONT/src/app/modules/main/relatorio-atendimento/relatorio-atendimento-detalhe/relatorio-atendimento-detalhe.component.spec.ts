import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RelatorioAtendimentoDetalheComponent } from './relatorio-atendimento-detalhe.component';

describe('RelatorioAtendimentoDetalheComponent', () => {
  let component: RelatorioAtendimentoDetalheComponent;
  let fixture: ComponentFixture<RelatorioAtendimentoDetalheComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RelatorioAtendimentoDetalheComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RelatorioAtendimentoDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
