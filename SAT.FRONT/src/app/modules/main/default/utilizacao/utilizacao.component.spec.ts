import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UtilizacaoComponent } from './utilizacao.component';

describe('UtilizacaoComponent', () => {
  let component: UtilizacaoComponent;
  let fixture: ComponentFixture<UtilizacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UtilizacaoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UtilizacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
