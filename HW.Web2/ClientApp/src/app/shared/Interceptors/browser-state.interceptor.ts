import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse
} from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { makeStateKey, TransferState } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class BrowserStateInterceptor implements HttpInterceptor {

  constructor(
    private transferState: TransferState,
  ) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.method === 'GET') {
      const key = makeStateKey(req.url);
      const storedResponse: string = this.transferState.get(key, null) as any;
      if (storedResponse) {
        const response = new HttpResponse({ body: storedResponse, status: 200 });
        return of(response);
      }
    }

    return next.handle(req);
  }
}
