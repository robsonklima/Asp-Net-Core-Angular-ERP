import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PontoExportacaoDialogComponent } from './ponto-exportacao-dialog.component';

describe('PontoExportacaoDialogComponent', () => {
  let component: PontoExportacaoDialogComponent;
  let fixture: ComponentFixture<PontoExportacaoDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PontoExportacaoDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PontoExportacaoDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
