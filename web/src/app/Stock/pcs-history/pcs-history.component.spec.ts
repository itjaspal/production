import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PcsHistoryComponent } from './pcs-history.component';

describe('PcsHistoryComponent', () => {
  let component: PcsHistoryComponent;
  let fixture: ComponentFixture<PcsHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PcsHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PcsHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
