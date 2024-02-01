import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ErrorComponentsRoutingModule } from './error-components-routing.module';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { SessionExpiredComponent } from './session-expired/session-expired.component';


@NgModule({
  declarations: [
    UnauthorizedComponent,
    PageNotFoundComponent,
    SessionExpiredComponent
  ],
  imports: [
    CommonModule,
    ErrorComponentsRoutingModule
  ]
})
export class ErrorComponentsModule { }
