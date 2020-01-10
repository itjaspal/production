import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StockLocationComponent } from './stock-location.component';

describe('StockLocationComponent', () => {
  let component: StockLocationComponent;
  let fixture: ComponentFixture<StockLocationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StockLocationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
