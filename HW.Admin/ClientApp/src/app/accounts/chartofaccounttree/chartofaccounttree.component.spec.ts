import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChartofaccounttreeComponent } from './chartofaccounttree.component';

describe('ChartofaccounttreeComponent', () => {
  let component: ChartofaccounttreeComponent;
  let fixture: ComponentFixture<ChartofaccounttreeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChartofaccounttreeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChartofaccounttreeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
