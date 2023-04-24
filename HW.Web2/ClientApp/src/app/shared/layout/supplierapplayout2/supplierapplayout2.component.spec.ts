import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { Supplierapplayout2Component } from './supplierapplayout2.component';

describe('Supplierapplayout2Component', () => {
  let component: Supplierapplayout2Component;
  let fixture: ComponentFixture<Supplierapplayout2Component>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ Supplierapplayout2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Supplierapplayout2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
