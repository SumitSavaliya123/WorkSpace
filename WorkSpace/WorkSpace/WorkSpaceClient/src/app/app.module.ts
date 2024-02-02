import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from './shared/shared.module';
import { LayoutModule } from './components/layout/layout.module';
import { JwtModule } from '@auth0/angular-jwt';
import { EmployeeModule } from './components/employee/employee.module';
import { ManagerModule } from './components/manager/manager.module';
import { ErrorComponentsModule } from './components/error-components/error-components.module';
import { API_INTERCEPTOR } from './interceptors/api-response.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    SharedModule,
    LayoutModule,
    EmployeeModule,
    ManagerModule,
    ErrorComponentsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem('authToken');
        },
      }
    })
    
  ],
  providers: [API_INTERCEPTOR],
  bootstrap: [AppComponent]
})
export class AppModule { }
