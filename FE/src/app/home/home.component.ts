import { Component, OnInit} from '@angular/core';
import { GVAR } from '../services/GVAR';
import {VehicleService} from '../services/vehicle.service';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import {VehicleDetailsModalComponent} from '../vehicle-details-modal/vehicle-details-modal.component'
import {DriverComponent} from '../driver/driver.component'
import { WebSocketService } from '../services/WebSocket.service';


@Component({
  selector:'app-home',
  standalone: true,
  imports: [CommonModule, VehicleDetailsModalComponent, DriverComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})

export class HomeComponent implements OnInit{

  showModal: boolean = false;
  GvarVehicleMoreInfo = new GVAR();
  GvarVehiclesInfo = new GVAR();

  title = 'websocket-client';

  constructor(private vehicleService: VehicleService, private webSocketService: WebSocketService) { }

  ngOnInit(): void {

    this.fetchVehicles();

    this.webSocketService.messages$.subscribe((data) => {
      
      console.log("message from websocket", data);
      this.fetchVehicles();

      
    });

  }

  convertEpochToDate(indate:string): string{
    
    return new Date(Number(indate)*1000).toLocaleString();

  }

 

  fetchVehicles(): void {
  
    this.vehicleService.getVehiclesWithLastEpoch().subscribe(
      (data: GVAR) => {
        if (data.DicOfDic.Tags.STS == "1")
          this.GvarVehiclesInfo = data;

        
        else 
          console.log("(DataBase Error) in fetching vehicles data", data.DicOfDic.Tags.Msg);
       
      },
      (error: any) => {
        console.error('Client-server Error) in fetching vehicles data:', error);
      }
    );
  }

 
  showmoreinfo(id:string): void{
    this.vehicleService.showmoreinfo(id).subscribe(
      (data: GVAR) => {
        if (data.DicOfDic.Tags.STS == "1"){
          this.GvarVehicleMoreInfo = data;
          if (this.GvarVehicleMoreInfo.DicOfDT.VehicleInformation[0] !=null)
          this.GvarVehicleMoreInfo.DicOfDT.VehicleInformation[0].LastGPSTime = this.convertEpochToDate(this.GvarVehicleMoreInfo.DicOfDT.VehicleInformation[0].LastGPSTime);
        }
        else 
          console.log("(DataBase Error) in fetching more vehicle data", data.DicOfDic.Tags.Msg);
        
        this.showModal = true;
     
    },
    (error: any) => {
      console.error('(Client-server Error) in fetching more vehicle data:', error);
  
    },
   
    ); 
  }


    closeModal(): void {
      this.showModal = false;
      }


    getMoreInfo(): GVAR{
      return this.GvarVehicleMoreInfo;
    }


}


