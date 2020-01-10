import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StockInquiryComponent } from './stock-inquiry.component';

describe('StockInquiryComponent', () => {
  let component: StockInquiryComponent;
  let fixture: ComponentFixture<StockInquiryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StockInquiryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockInquiryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
