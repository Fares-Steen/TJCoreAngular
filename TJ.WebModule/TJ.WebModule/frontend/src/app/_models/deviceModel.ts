export interface IDeviceModel {
  id: number;
  name: string;
  addedDate: Date
}

export class DeviceModel implements IDeviceModel{
  id!: number;
  name!: string;
  addedDate!: Date
}
