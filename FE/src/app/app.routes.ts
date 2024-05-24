import { Routes } from '@angular/router';
import { NgModule} from '@angular/core';
import {HomeComponent} from './home/home.component';
import {DriverComponent} from './driver/driver.component';
import {VehicleInformationComponent} from './vehicle-information/vehicle-information.component';
import {RouteHistoryComponent} from './route-history/route-history.component'
import {GeofencesComponent} from './geofences/geofences.component'
export const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'Drivers', component: DriverComponent },
    { path: 'VehicleInfo', component: VehicleInformationComponent },
    { path: 'RouteHistory', component: RouteHistoryComponent },
    { path: 'Geofences', component: GeofencesComponent },
    { path: '', redirectTo: '/home', pathMatch: 'full'},
];

