import { Data } from "@angular/router";

export interface IDeviceModel {
  id: number;
  name: string;
  dateAdded: Date
}

export class DeviceModel implements IDeviceModel{
  id!: number;
  name!: string;
  dateAdded!: Date
}
