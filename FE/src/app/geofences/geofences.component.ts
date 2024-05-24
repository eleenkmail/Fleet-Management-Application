import { Component, OnInit } from '@angular/core';
import { VehicleService} from '../services/vehicle.service';
import {GVAR} from '../services/GVAR';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-geofences',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './geofences.component.html',
  styleUrl: './geofences.component.css'
})
export class GeofencesComponent implements OnInit{

  Geofences = new GVAR();
  constructor(private vehicleService: VehicleService){}

  ngOnInit(): void {
    this.getGeofences()
  }

  convertEpochToDate(indate:string): string{
    
    return new Date(Number(indate)*1000).toLocaleString().split(',')[0];

  }

  getGeofences():void {
    this.vehicleService.getAllGeofences().subscribe((data:GVAR) => {
      if (data.DicOfDic.Tags.STS == "1")
        this.Geofences = data;
      else
        console.error("(DataBase Error) in fetching Geofences data", data.DicOfDic.Tags.Msg);
    },
    (error: any) => {
      console.error('(Client-Server Error) in fetching Geofences data:', error);
    });

  }
}
