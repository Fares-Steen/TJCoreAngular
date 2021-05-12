import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DeviceModel, IDeviceModel } from '../_models/deviceModel';
import { DeviceService } from '../_services/device.service';

@Component({
  selector: 'app-create-device',
  templateUrl: './create-device.component.html',
  styleUrls: ['./create-device.component.scss']
})
export class CreateDeviceComponent {
  form = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.maxLength(50)]),
  })
  showSuccess = false;
  get name() {
    return this.form.get('name');
  }
  get Success() {
    return this.showSuccess;
  }
  set Success(success: boolean) {
    this.showSuccess = success
  }
  deviceModel: DeviceModel = new DeviceModel
  constructor(private deviceService: DeviceService) {
  }

  createDevice() {
    this.deviceModel.name = this.name?.value

    if (this.form.valid) {
      this.deviceService.createDevice(this.deviceModel).subscribe((result: any) => {
        if ((result as DeviceModel).id != 0) {
          this.showSuccess = true;
        }
      })
    };
  }

  dismissAlertMessage() {
    this.showSuccess=false
  }
}
