import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { MessageService } from '../shared/services/message.service';
import { MessageConstant } from '../constants/message-constant';
import { RoutingPathConstant } from '../constants/routing/routing-path';
import { UserRole } from '../constants/user-role';

export function AuthGuard(): CanActivateFn {
  return (next: ActivatedRouteSnapshot) => {
    const authService = inject(AuthService);
    const router = inject(Router);
    const messageService = inject(MessageService);

    const expectedRole = next.data['expextedRole'];
    const isAuthenticated = authService.isAuthenticate();
    const userRole = authService.getUserRole();
    
    if(!isAuthenticated){
        messageService.error(MessageConstant.loginFirst,MessageConstant.close);
        router.navigate([RoutingPathConstant.loginUrl]);
        return false;
    }

    if(userRole == UserRole.managerRoleId && expectedRole !== UserRole.manager){
        messageService.error(MessageConstant.unauthorize,MessageConstant.close);
        router.navigate([RoutingPathConstant.unauthorizeUrl]);
        return false;
    }

    return true;
  }
};
