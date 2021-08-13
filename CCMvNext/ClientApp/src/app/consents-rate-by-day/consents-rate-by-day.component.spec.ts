import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsentsRateByDayComponent } from './consents-rate-by-day.component';

describe('ConsentsRateByDayComponent', () => {
  let component: ConsentsRateByDayComponent;
  let fixture: ComponentFixture<ConsentsRateByDayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConsentsRateByDayComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsentsRateByDayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
