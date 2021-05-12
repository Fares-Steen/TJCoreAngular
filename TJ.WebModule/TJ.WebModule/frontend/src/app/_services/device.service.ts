import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { IDeviceModel } from '../_models/deviceModel';

@Injectable({
  providedIn: 'root'
})


export class DeviceService {
  baseUrl = environment.apiUrl + '/api/device/';

  constructor(private http: HttpClient) {

  }

   createDevice(model: any) {
    return this.http.post<IDeviceModel>(this.baseUrl + 'create', model);
  }

  getDevices() {
    return this.http.get<IDeviceModel[]>(this.baseUrl+'get')
  }
}
