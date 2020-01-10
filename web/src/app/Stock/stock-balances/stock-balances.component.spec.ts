import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StockBalancesComponent } from './stock-balances.component';

describe('StockBalancesComponent', () => {
  let component: StockBalancesComponent;
  let fixture: ComponentFixture<StockBalancesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StockBalancesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockBalancesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
