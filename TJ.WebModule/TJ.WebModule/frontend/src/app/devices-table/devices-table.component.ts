import { Component, OnInit } from '@angular/core';
import { DeviceModel, IDeviceModel } from '../_models/deviceModel';
import { DeviceService } from '../_services/device.service';
import { faSort } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-devices-table',
  templateUrl: './devices-table.component.html',
  styleUrls: ['./devices-table.component.scss']
})
export class DevicesTableComponent implements OnInit {
  faSort = faSort;
  devices: DeviceModel[] = [];
  mydate = Date.now()
  mydate2= "string"
  constructor(private deviceService: DeviceService) {
  }
  ngOnInit(): void {
    this.readAllDevices()
  }

  readAllDevices() {
    this.deviceService.getDevices().subscribe((response: IDeviceModel[]) => {
      this.devices = response;
    });
  }

  key:string = 'date'
  reverse: boolean = false;
  sortDevicesTableBy(key: string) {
    this.key = key
    this.reverse = !this.reverse
  }
}
