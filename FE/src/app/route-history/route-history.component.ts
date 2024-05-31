import { Component, OnInit } from '@angular/core';
import {VehicleService} from '../services/vehicle.service';
import {GVAR} from '../services/GVAR';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-route-history',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './route-history.component.html',
  styleUrl: './route-history.component.css'
})
export class RouteHistoryComponent implements OnInit{

  vehicles = new GVAR();
  selectedVehicleId: string | undefined;
  routeHistory: any[] = [];
  sortedRouteHistory = new GVAR();
  RouteHistoryToAdd = new GVAR();
  Vehicle = new GVAR();

  RouteHistory = {
    "VehicleID": '',
    "VehicleDirection": '',
    "Status": '',
    "Epoch": '',
    "Address": '',
    "VehicleSpeed": '',
    "Latitude": '',
    "Longitude": ''
  };

  
  constructor(private vehicleService: VehicleService) {}

  ngOnInit(): void {
    this.getVehicles();
  }

  getVehicles(): void {
    this.vehicleService.getAllVehicles().subscribe((data:GVAR) => {
      if (data.DicOfDic.Tags.STS == "1")
        this.vehicles = data;

      else console.log("(DataBase Error) in fetching Vehicles data", data.DicOfDic.Tags.Msg);
    },
    (error: any) => {
      console.error('(Client-Server Error) in fetching Vehicles data:', error);
    });


  }

  convertDateToEpoch(date:Date): string{
    
    return String(Math.floor(new Date(date).getTime()) / 1000)

  }

  convertEpochToDate(indate:string): string{
    
    return new Date(Number(indate)*1000).toLocaleString().split(',')[0];

  }

  onVehicleSelect(): void {
    this.Vehicle.DicOfDic.Tags = {
      "VehicleID": this.selectedVehicleId,
      "FirstEpoch":"0",
      "LastEpoch": Math.floor(new Date().getTime() / 1000).toString()
      },

      this.vehicleService.getRouteHistoryForVehicle(this.Vehicle).subscribe((data: GVAR) => {
      if (data.DicOfDic.Tags.STS == "1"){
          this.routeHistory = data.DicOfDT.RouteHistory;
          this.sortedRouteHistory = data;
        }
      
      else console.log("(DataBase Error) in fetching Vehicle History data", data.DicOfDic.Tags.Msg);
      },

      (error: any) => {
        console.error('(Client-Server Error) in fetching Vehicle History data:', error);
      });
  }

  addHistory(){
    this.RouteHistoryToAdd.DicOfDic.Tags = this.RouteHistory;
    this.RouteHistoryToAdd.DicOfDic.Tags.VehicleID = this.selectedVehicleId;
    this.RouteHistoryToAdd.DicOfDic.Tags.Epoch = this.convertDateToEpoch(this.RouteHistoryToAdd.DicOfDic.Tags.Epoch);
    this.vehicleService.addRouteHistory(this.RouteHistoryToAdd).subscribe(

      (data: GVAR) => {
        if(data.DicOfDic.Tags.STS == "1")
          console.log("Route History Added Successfully");

        else 
        console.log("(DataBase Error) Route History not Added: ", data.DicOfDic.Tags.Msg);
      },

      (error: any) => {
        console.error('(Client-Server Error) Route History not Added:', error);
      });
      

  }
}


