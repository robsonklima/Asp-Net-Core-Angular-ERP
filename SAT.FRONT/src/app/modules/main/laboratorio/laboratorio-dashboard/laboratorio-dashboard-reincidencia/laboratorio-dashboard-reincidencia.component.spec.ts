import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LaboratorioDashboardReincidenciaComponent } from './laboratorio-dashboard-reincidencia.component';

describe('LaboratorioDashboardReincidenciaComponent', () => {
  let component: LaboratorioDashboardReincidenciaComponent;
  let fixture: ComponentFixture<LaboratorioDashboardReincidenciaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LaboratorioDashboardReincidenciaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LaboratorioDashboardReincidenciaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
