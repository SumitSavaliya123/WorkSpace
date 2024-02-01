import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';
import { UserRole } from '../constants/user-role';
import { RoutingPathConstant } from '../constants/routing/routing-path';

export function SigninGuard(): CanActivateFn{
  return () => {
    const authService = inject(AuthService);
    const router = inject(Router);

    const isAuthenticated = authService.isAuthenticate();
      if(isAuthenticated){
        if(authService.getUserRole() == UserRole.managerRoleId){
          router.navigate([RoutingPathConstant.managerDashboardUrl]);
        }
        else {
          router.navigate([RoutingPathConstant.employeeDashboardUrl]);
        }
       return false; 
      }
      return true;
    
  };
};
