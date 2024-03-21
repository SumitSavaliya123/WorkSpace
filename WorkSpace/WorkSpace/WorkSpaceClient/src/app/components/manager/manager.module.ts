import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ManagerRoutingModule } from './manager-routing.module';
import { ManagerDashboardComponent } from './manager-dashboard/manager-dashboard.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { GoogleMapsModule } from '@angular/google-maps';


@NgModule({
  declarations: [
    ManagerDashboardComponent
  ],
  imports: [
    CommonModule,
    ManagerRoutingModule,
    SharedModule,
    GoogleMapsModule
  ]
})
export class ManagerModule { }
