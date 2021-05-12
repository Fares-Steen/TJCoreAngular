import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainComponent } from './main/main.component';
import { CreateDeviceComponent } from './create-device/create-device.component';
import { DevicesTableComponent } from './devices-table/devices-table.component';

@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    CreateDeviceComponent,
    DevicesTableComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
