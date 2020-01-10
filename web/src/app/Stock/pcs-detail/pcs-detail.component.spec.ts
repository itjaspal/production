import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PcsDetailComponent } from './pcs-detail.component';

describe('PcsDetailComponent', () => {
  let component: PcsDetailComponent;
  let fixture: ComponentFixture<PcsDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PcsDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PcsDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
