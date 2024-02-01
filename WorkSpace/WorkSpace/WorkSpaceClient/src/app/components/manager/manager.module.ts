import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ManagerRoutingModule } from './manager-routing.module';
import { ManagerDashboardComponent } from './manager-dashboard/manager-dashboard.component';


@NgModule({
  declarations: [
    ManagerDashboardComponent
  ],
  imports: [
    CommonModule,
    ManagerRoutingModule
  ]
})
export class ManagerModule { }
