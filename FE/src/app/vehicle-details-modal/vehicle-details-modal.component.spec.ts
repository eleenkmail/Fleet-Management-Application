import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehicleDetailsModalComponent } from './vehicle-details-modal.component';

describe('VehicleDetailsModalComponent', () => {
  let component: VehicleDetailsModalComponent;
  let fixture: ComponentFixture<VehicleDetailsModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VehicleDetailsModalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VehicleDetailsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
