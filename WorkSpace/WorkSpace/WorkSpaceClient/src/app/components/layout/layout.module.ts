import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { LayoutRoutingModule } from './layout-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AuthenticationLayoutComponent } from './authentication-layout/authentication-layout.component';
import { CommonLayoutComponent } from './common-layout/common-layout.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';

const components =[
  AuthenticationLayoutComponent,
  CommonLayoutComponent,
  HeaderComponent,
  FooterComponent
]

@NgModule({
  declarations: [
    ...components
  ],
  imports: [
    CommonModule,
    LayoutRoutingModule,
    SharedModule,
    FormsModule,
    NgbModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  exports: [CommonModule, ...components]
})
export class LayoutModule { }
