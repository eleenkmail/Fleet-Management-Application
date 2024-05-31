import { Component, OnInit } from '@angular/core';
import {CommonModule} from '@angular/common';
import {VehicleService} from '../services/vehicle.service'
import {GVAR} from '../services/GVAR'
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-driver',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './driver.component.html',
  styleUrl: './driver.component.css'
})
export class DriverComponent implements OnInit{
  constructor(private vehicleService: VehicleService) { }


  Drivers = new GVAR();
  updateDriver: boolean = false;
  DriverToUpdate = new GVAR();
  DriverToAddGvar = new GVAR();
  DriverToAdd = {
    "DriverID": '',
    "DriverName": '',
    "PhoneNumber": '',
  };


  ngOnInit(): void {
    this.fetchDrivers();
  }
  
  showUpdateDriver(driver: Object):void{
    this.updateDriver = true;
    this.DriverToUpdate.DicOfDic.Tags = driver;
   
      }

  convertToString(data:any): string{
    return data.toString();
  }
      
  closeUpdateDriver(): void{
    this.updateDriver = false;
  }

  fetchDrivers(): void {
  
    this.vehicleService.getAllDrivers().subscribe(
      (data: GVAR) => {

        if (data.DicOfDic.Tags.STS == "1")
          this.Drivers = data;

        else 
          console.log("(DataBase Error) in Fetching Drivers data " ,data.DicOfDic.Tags.Msg);
    
      },
      (error: any) => {
        console.error('(Client-Server Error) in fetching Drivers data ', error);
      }
    );
  }




  addDriverInfo(Gvar:Object): void{
    this.DriverToAddGvar.DicOfDic.Tags = Gvar;
    this.vehicleService.addDriver(this.DriverToAddGvar).subscribe(
      (data: GVAR) => {
        if (data.DicOfDic.Tags.STS == "1")
          console.log("Driver Added Successfully ", data.DicOfDic.Tags.Msg);

        else
          console.log("(DataBase Error) in Adding Driver ",data.DicOfDic.Tags.Msg);
          
    
      },
      (error: any) => {
        console.error('(Client-Server Error) in Adding Driver ', error);
      }
    );
    
  }

  updateDriverInfo(Gvar:GVAR):void{
    this.DriverToUpdate = Gvar;
    this.vehicleService.updateDriver(this.DriverToUpdate).subscribe( 
      (data: GVAR) => {

        if (data.DicOfDic.Tags.STS == "1")
          console.log("Driver updated Successfullly");

        else
          console.log("(DataBase Error) in updating Driver information" ,data.DicOfDic.Tags.Msg);
      },

      (error: any) => {
        console.error('(Client-Server Error) in updating Driver information:', error);
      });

    this.updateDriver = false;
  }

}


