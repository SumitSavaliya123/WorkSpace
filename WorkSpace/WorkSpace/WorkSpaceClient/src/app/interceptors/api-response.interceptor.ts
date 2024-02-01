import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { MessageService } from '../shared/services/message.service';
import { IResponse } from '../models/shared/response';

@Injectable()
export class ApiResponseInterceptor implements HttpInterceptor {
  constructor(private messageService: MessageService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      tap((event: HttpEvent<any>) => {
        if (
          event instanceof HttpResponse &&
          (request.method === 'POST' ||
            request.method === 'PUT' ||
            request.method === 'DELETE') &&
          event.body.message &&
          event.body.message !== 'Success' &&
          event.body.message.length > 0
        ) 
        {
          const response : IResponse<null> = event.body;
          this.messageService.success(response.message);
        }
      })
    );
  }
}

export const API_INTERCEPTOR = {
  multi: true,
  useClass: ApiResponseInterceptor,
  provide: HTTP_INTERCEPTORS,
}
