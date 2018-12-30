import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class HeaderInterceptor implements HttpInterceptor {

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const updatedRequest = request.clone({
      headers: request.headers.set('Authorization', 'bearer ' + localStorage.getItem('token'))
    });
    return next.handle(updatedRequest);
  }


}


export const HeaderInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: HeaderInterceptor,
  multi: true
};
