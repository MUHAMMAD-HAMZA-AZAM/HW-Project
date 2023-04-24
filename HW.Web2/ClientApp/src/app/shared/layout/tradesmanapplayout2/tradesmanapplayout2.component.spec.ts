import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { Tradesmanapplayout2Component } from './tradesmanapplayout2.component';

describe('Tradesmanapplayout2Component', () => {
  let component: Tradesmanapplayout2Component;
  let fixture: ComponentFixture<Tradesmanapplayout2Component>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ Tradesmanapplayout2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Tradesmanapplayout2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
