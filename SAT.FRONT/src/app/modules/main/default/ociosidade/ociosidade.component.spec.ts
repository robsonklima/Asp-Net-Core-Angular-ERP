import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OciosidadeComponent } from './ociosidade.component';

describe('OciosidadeComponent', () => {
  let component: OciosidadeComponent;
  let fixture: ComponentFixture<OciosidadeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OciosidadeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OciosidadeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
