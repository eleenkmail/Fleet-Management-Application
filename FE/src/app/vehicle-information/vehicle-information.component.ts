import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';
import {GVAR} from '../services/GVAR';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-vehicle-information',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './vehicle-information.component.html',
  styleUrl: './vehicle-information.component.css'
})

export class VehicleInformationComponent implements OnInit{

  constructor(private vehicleService:VehicleService){}

  ngOnInit(): void {
    this.fetchDrivers()
    this.fetchVehiclesInfo()
    this.fetchAllVehicles()
  }
  selectedDriverId: number | undefined;
  selectedVehicleId: string | undefined;
  Vehicles = new GVAR();
  VehiclesInfo = new GVAR();
  VehicleToAdd = new GVAR();
  VehicleInfoToAdd = new GVAR();
  Drivers = new GVAR();
  updateVehicleInfo:boolean = false;
  updateVehicle: boolean = false;
  VehicleToUpdate = new GVAR();
  VehicleToDelete = new GVAR();


  VehiclesInfoGvar = {
    "DriverID": '',
    "DriverName": '',
    "PhoneNumber": '',
    "VehicleID": '',
    "VehicleNumber": '',
    "VehicleType": '',
    "VehicleMake": '',
    "VehicleModel": '',
    "PurchaseDate": ''
  };

  convertDateToEpoch(date:Date): string{
    
    return String(Math.floor(new Date(date).getTime()) / 1000)

  }

  convertEpochToDate(indate:string): string{
    
    return new Date(Number(indate)*1000).toLocaleString().split(',')[0];

  }
 
  showUpdateVehiclesInfo(Gvar:Object):void{
    this.updateVehicleInfo = true;
    this.VehicleToUpdate.DicOfDic.Tags = Gvar;
  }

  showUpdateVehicles(Gvar:Object):void{
    this.updateVehicle = true;
    this.VehicleToUpdate.DicOfDic.Tags = Gvar;
  }

  closeUpdate(): void{
    this.updateVehicleInfo = false;
    this.updateVehicle = false;
  }

  

  fetchAllVehicles():void {
    this.vehicleService.getAllVehicles().subscribe(
      (data: GVAR) => {
        if (data.DicOfDic.Tags.STS == "1")
           this.Vehicles = data;

        else 
          console.log("DataBase Error in fetching Vehicles ", data.DicOfDic.Tags.Msg);
      },

      (error: any) => {
        console.error('(Client-Server Error) in fetching Vehilces:', error);
      }
    );
}

  fetchVehiclesInfo(): void {
  
  this.vehicleService.getVehiclesInfo().subscribe(
      (data: GVAR) => {
        if (data.DicOfDic.Tags.STS == "1")
           this.VehiclesInfo = data;

        else 
          console.log("DataBase Error in fetching Vehicles informations ", data.DicOfDic.Tags.Msg);
      },

      (error: any) => {
        console.error('(Client-Server Error) in fetching Vehilces informations:', error);
      }
    );

  }


  fetchDrivers(): void {
    this.vehicleService.getAllDrivers().subscribe(
      (data: GVAR) => {
        if (data.DicOfDic.Tags.STS == "1")
          this.Drivers = data;

        else 
        console.log("(DataBase Error) in fetchin Drivers informations ", data.DicOfDic.Tags.Msg);
    },
    
      (error: any) => {
        console.error('(Cient-Server Error) in fetching Drivers informations:', error);
      }
    );
  }


  addVehicle(Gvar:Object): void{
    this.VehicleToAdd.DicOfDic.Tags = Gvar;
    this.vehicleService.addVehicle(this.VehicleToAdd).subscribe(
      (data: GVAR) =>{
        if (data.DicOfDic.Tags.STS == "1"){
          this.Drivers = data;
          console.log("Vehicle Added Successfully");
        }
        else 
        console.log("(DataBase Error) in Adding Vehicle", data.DicOfDic.Tags.Msg);
    },
    
      (error: any) => {
        console.error('(Cient-Server Error) in Adding Vehicle', error);
      
      }
    );
  }

  addVheicleInfo(Gvar:Object):void{

    this.VehicleInfoToAdd.DicOfDic.Tags = Gvar;
    this.VehicleInfoToAdd.DicOfDic.Tags.PurchaseDate = this.convertDateToEpoch(this.VehicleInfoToAdd.DicOfDic.Tags.PurchaseDate);
    this.vehicleService.addVehicleInfo(this.VehicleInfoToAdd).subscribe(
      (data: GVAR) =>{
        if (data.DicOfDic.Tags.STS == "1"){
          console.log("Vehicle Information Added Successfully");
        }
        else 
        console.log("(DataBase Error) in Adding Vehicle Information", data.DicOfDic.Tags.Msg);
    },
    
      (error: any) => {
        console.error('(Cient-Server Error) in Adding Vehicle Information', error);
      
      }
  );


  }

  updateVheicle(Gvar:GVAR):void{

    this.VehicleToUpdate = Gvar;
   
    this.vehicleService.updateVheicle(this.VehicleToUpdate).subscribe(
        (data: GVAR) =>{
          if (data.DicOfDic.Tags.STS == "1"){
            console.log("Vehicle Updated Successfully");
          }
          else 
          console.log("(DataBase Error) in updating Vehicle", data.DicOfDic.Tags.Msg);
      },
      
        (error: any) => {
          console.error('(Cient-Server Error) in updating Vehicle', error);
        
        }
    );

    this.updateVehicle = false;
  }


  updateVheicleInfo(Gvar:GVAR):void{
   
    this.VehicleToUpdate = Gvar;
    this.VehicleToUpdate.DicOfDic.Tags.PurchaseDate = this.convertDateToEpoch(this.VehicleToUpdate.DicOfDic.Tags.PurchaseDate);

    this.vehicleService.updateVheicleInfos(this.VehicleToUpdate).subscribe(
        (data: GVAR) =>{
          if (data.DicOfDic.Tags.STS == "1"){
            console.log("Vehicle information Updated Successfully");
          }
          else 
          console.log("(DataBase Error) in updating Vehicle information", data.DicOfDic.Tags.Msg);
      },
      
        (error: any) => {
          console.error('(Cient-Server Error) in updating Vehicle information', error);
        
        }
    );
    
    this.updateVehicleInfo = false;
  }


  deleteInfo(Gvar:Object):void{
   this.VehicleToDelete.DicOfDic.Tags = Gvar;
   this.vehicleService.deleteVehicleInfo(this.VehicleToDelete).subscribe(
    (data:GVAR)=>{

      if (data.DicOfDic.Tags.STS == "1")
          console.log("Vehicle information Deleted Successfully");
        
        else 
        console.log("(DataBase Error) in Deleting Vehicle information", data.DicOfDic.Tags.Msg);
      },
    
      (error: any) => {
        console.error('(Cient-Server Error) in Deleting Vehicle information', error);
      
      });
    }
   

  deleteVehicles(Gvar:Object):void{
      this.VehicleToDelete.DicOfDic.Tags = Gvar;
      this.vehicleService.deleteVehicle(this.VehicleToDelete).subscribe(
      (data:GVAR)=>{

        if (data.DicOfDic.Tags.STS == "1")
          console.log("Vehicle Deleted Successfully");
        
        else 
        console.log("(DataBase Error) in Deleting Vehicle", data.DicOfDic.Tags.Msg);
        },
    
      (error: any) => {
        console.error('(Cient-Server Error) in Deleting Vehicle ', error);
      
      });

    
       }
}
