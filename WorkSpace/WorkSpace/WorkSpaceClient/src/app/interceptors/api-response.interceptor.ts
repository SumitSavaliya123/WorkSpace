import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { Observable, finalize, tap } from 'rxjs';
import { MessageService } from '../shared/services/message.service';
import { IResponse } from '../models/shared/response';
import { LoaderService } from '../shared/services/loader.service';

@Injectable()
export class ApiResponseInterceptor implements HttpInterceptor {
  constructor(private messageService: MessageService, private loaderService : LoaderService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      tap((event: HttpEvent<any>) => {
        this.loaderService.setLoader(true);
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
      }),
      finalize(() => this.loaderService.setLoader(false)) 
      );
  }
}

export const API_INTERCEPTOR = {
  multi: true,
  useClass: ApiResponseInterceptor,
  provide: HTTP_INTERCEPTORS,
}
