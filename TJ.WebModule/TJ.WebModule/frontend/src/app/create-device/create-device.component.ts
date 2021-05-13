import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DeviceModel, IDeviceModel } from '../_models/deviceModel';
import { DeviceService } from '../_services/device.service';

@Component({
  selector: 'app-create-device',
  templateUrl: './create-device.component.html',
  styleUrls: ['./create-device.component.scss']
})
export class CreateDeviceComponent {
  @Output() newDeviceCreatedEvent = new EventEmitter();
  form = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.maxLength(50)]),
  })
  showAlert: Alert = Alert.dismissed;
  isLoading = false;

  get Loading() {
    return this.isLoading;
  }
  get name() {
    return this.form.get('name');
  }
  get Success() {
    return this.showAlert;
  }
  set Success(alert: Alert) {
    this.showAlert = alert
  }
  deviceModel: DeviceModel = new DeviceModel
  constructor(private deviceService: DeviceService) {
  }

  createDevice() {
    this.hideLoadingIcon()
    this.showAlert = Alert.dismissed;
    this.deviceModel.name = this.name?.value

    if (this.form.valid) {
      this.deviceService.createDevice(this.deviceModel).subscribe((result: IDeviceModel) => {
        this.showAlert = Alert.success;
        this.showLoadingIcon()
        this.clearFormNameInput()
        this.askTableComponentToRefresh()
        
      }, error => {
          this.showAlert = Alert.error;
          this.showLoadingIcon()
      })
    };
  }

  askTableComponentToRefresh() {
    this.newDeviceCreatedEvent.emit(null)
  }

  showLoadingIcon() {
    this.isLoading = false;
  }
  hideLoadingIcon() {
    this.isLoading = true;
  }

  clearFormNameInput() {
    this.name?.setValue("");
  }

  dismissAlertMessage() {
    this.showAlert = Alert.dismissed
  }
}
export enum Alert {
  success = 1,
  error,
  dismissed

}
