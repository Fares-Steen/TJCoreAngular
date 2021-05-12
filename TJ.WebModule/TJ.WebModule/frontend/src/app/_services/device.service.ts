import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})


export class DeviceService {
  baseUrl = environment.apiUrl + '/api/device/';

  constructor(private http: HttpClient) {

  }

   createDevice(model: any) {
    return this.http.post(this.baseUrl + 'create', model);
  }

  getDevices() {
    return this.http.get(this.baseUrl+'get')
  }
}
