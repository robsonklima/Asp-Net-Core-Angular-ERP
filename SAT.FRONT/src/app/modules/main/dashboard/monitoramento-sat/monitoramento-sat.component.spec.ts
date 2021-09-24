import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonitoramentoSatComponent } from './monitoramento-sat.component';

describe('MonitoramentoSatComponent', () => {
  let component: MonitoramentoSatComponent;
  let fixture: ComponentFixture<MonitoramentoSatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MonitoramentoSatComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MonitoramentoSatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
