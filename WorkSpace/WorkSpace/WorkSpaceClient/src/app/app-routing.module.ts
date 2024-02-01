import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationModule } from './components/authentication/authentication.module';
import { SigninGuard } from './guards/signin.guard';
import { UserRole } from './constants/user-role';
import { EmployeeModule } from './components/employee/employee.module';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => AuthenticationModule,
    //canActivate: [SigninGuard()],
  },
  {
    path: UserRole.employee,
    loadChildren: () => EmployeeModule,
    canActivate: [AuthGuard()],
    data: { expectedRole: UserRole.employee },
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
