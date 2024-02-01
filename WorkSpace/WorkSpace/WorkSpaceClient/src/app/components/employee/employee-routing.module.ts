import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonLayoutComponent } from '../layout/common-layout/common-layout.component';
import { RoutingPathConstant } from 'src/app/constants/routing/routing-path';
import { EmployeeDashboardComponent } from './employee-dashboard/employee-dashboard.component';

const routes: Routes = [
  {
    path:'',
    component:CommonLayoutComponent,
    children:[
      {
        path:RoutingPathConstant.employeeDashboard,
        component:EmployeeDashboardComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }
