import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CookieConsentPageComponent } from './cookie-consent-page/cookie-consent-page.component'
import { ConsentsRateByDayComponent } from './consents-rate-by-day/consents-rate-by-day.component';

const routes: Routes = [
  { path: '', component: CookieConsentPageComponent },
  { path: 'consent-rate', component: ConsentsRateByDayComponent },];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
