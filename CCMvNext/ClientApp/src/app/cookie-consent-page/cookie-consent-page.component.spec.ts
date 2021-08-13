import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CookieConsentPageComponent } from './cookie-consent-page.component';

describe('CookieConsentPageComponent', () => {
  let component: CookieConsentPageComponent;
  let fixture: ComponentFixture<CookieConsentPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CookieConsentPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CookieConsentPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
