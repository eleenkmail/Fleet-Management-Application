import { Injectable } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import {GVAR} from './GVAR';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private apiUrl = 'https://localhost:7104/';

  constructor(private http: HttpClient) { }

  getVehiclesWithLastEpoch(): Observable<any> {
    return this.http.get<GVAR>(`${this.apiUrl}RouteHistory/RetrieveAllWithLastEpoch`);
  }
 
  showmoreinfo(id:string):Observable<any> {
    const Gvar = {
      "DicOfDic": {
        "Tags": {
          "VehicleID": id
        }
      },
      "DicOfDT": {}
    };
    
    return this.http.post<GVAR>(`${this.apiUrl}VehicleInformation/RrtrieveDetailedInfo`, Gvar);
  }


  getAllDrivers():Observable<any> {
    return this.http.get<GVAR>(`${this.apiUrl}Driver/RetriveAllDrivers`); 
  }

  updateDriver(Gvar:GVAR):Observable<any> {
    return this.http.post<GVAR>(`${this.apiUrl}Driver/Update`,Gvar); 
  }

  addDriver(Gvar:GVAR):Observable<any> {
    return this.http.post<GVAR>(`${this.apiUrl}Driver/Add`,Gvar); 
  }


  getVehiclesInfo():Observable<any>{
    return this.http.get<GVAR>(`${this.apiUrl}VehicleInformation/RrtrieveVehicleInfo`);
  }

  addVehicleInfo(Gvar:GVAR):Observable<any> {
    return this.http.post<GVAR>(`${this.apiUrl}VehicleInformation/Add`,Gvar); 
  }

  addVehicle(Gvar:GVAR):Observable<any> {
    return this.http.post<GVAR>(`${this.apiUrl}Vehicle/Add`,Gvar); 
  }

  updateVheicle(Gvar:GVAR):Observable<any>{
    return this.http.post<GVAR>(`${this.apiUrl}Vehicle/Update`,Gvar); 
  }
  updateVheicleInfos(Gvar:GVAR):Observable<any>{
    return this.http.post<GVAR>(`${this.apiUrl}VehicleInformation/Update`,Gvar); 
    
  }


  deleteVehicleInfo(Gvar:GVAR):Observable<any> {
    return this.http.post<GVAR>(`${this.apiUrl}VehicleInformation/Delete`,Gvar); 
  }

  deleteVehicle(Gvar:GVAR):Observable<any>{
    return this.http.post<GVAR>(`${this.apiUrl}Vehicle/Delete`,Gvar);
  }

  getAllVehicles():Observable<any>{
    return this.http.get<GVAR>(`${this.apiUrl}Vehicle/RetriveAll`);
  }

  getRouteHistoryForVehicle(Gvar:GVAR):Observable<any>{
    return this.http.post<GVAR>(`${this.apiUrl}RouteHistory/WithEpochRange`, Gvar);
  }

  addRouteHistory(Gvar:GVAR):Observable<any>{
    return this.http.post<GVAR>(`${this.apiUrl}RouteHistory/AddRouteHistory`, Gvar);
  }

  getAllGeofences():Observable<any>{
    return this.http.get<GVAR>(`${this.apiUrl}Geofences/RetrieveGeofencesInformation`);
  }

}

