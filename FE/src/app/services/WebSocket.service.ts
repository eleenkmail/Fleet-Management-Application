
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class WebSocketService {
  private socket: WebSocket;
  private messageSubject = new Subject<any>();
  
  public recived: boolean = true;
  public messages$: Observable<any> = this.messageSubject.asObservable();

  constructor() {
  
   
    this.socket = new WebSocket('ws://localhost:8181');


    this.socket.onopen = ()=>{
        console.log("websocket oppened");
    
    }

    this.socket.onclose = ()=>{
        console.log("websocket closed");
    
    }

    this.socket.onmessage = (event) => {
      this.messageSubject.next(event.data);
    };

    this.socket.onerror = (event) =>{
        console.log(event);
    }
  }
}
