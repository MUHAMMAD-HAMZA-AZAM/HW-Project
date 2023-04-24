import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ShippingService {
  APIKEY = 'ZVg4Z1pWeFlSYnJnRVlaaDlQbWZqQWFXM0dIclhwOXlJMTIxbzlyZndxTDZBdlNEN2locWdlVzdBU3dG60755fc4a3e76';
  constructor(private _http: HttpClient) {
  }
  PostData(apiUrl: string, data: any) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': this.APIKEY
    });
    return this._http.post(apiUrl, JSON.stringify(data), { headers: headers })
  }
}
