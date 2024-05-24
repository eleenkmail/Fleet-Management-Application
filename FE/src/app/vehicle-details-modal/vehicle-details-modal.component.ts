import { Component, Input, Output, EventEmitter, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HomeComponent} from '../home/home.component'
import { ActivatedRoute } from '@angular/router';
import {GVAR} from '../services/GVAR';

@Component({
  selector: 'app-vehicle-details-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './vehicle-details-modal.component.html',
  styleUrl: './vehicle-details-modal.component.css'
})
export class VehicleDetailsModalComponent implements OnInit {

  vehicleDetails = new GVAR();
  constructor(private componenet: HomeComponent) {}

  ngOnInit() {

    this.vehicleDetails = this.componenet.getMoreInfo();
    console.log(this.vehicleDetails);
  }
  @Output() closeModalEvent = new EventEmitter<void>();


  
 
  closeModal(): void {
    this.closeModalEvent.emit();
  }

  

  
  
}
