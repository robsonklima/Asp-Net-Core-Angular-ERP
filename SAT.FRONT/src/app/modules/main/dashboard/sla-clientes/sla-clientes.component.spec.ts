import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SlaClientesComponent } from './sla-clientes.component';

describe('SlaClientesComponent', () => {
  let component: SlaClientesComponent;
  let fixture: ComponentFixture<SlaClientesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SlaClientesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SlaClientesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
