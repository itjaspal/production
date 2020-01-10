import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StockLedgerComponent } from './stock-ledger.component';

describe('StockLedgerComponent', () => {
  let component: StockLedgerComponent;
  let fixture: ComponentFixture<StockLedgerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StockLedgerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockLedgerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
