import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehicleInformationComponent } from './vehicle-information.component';

describe('VehicleInformationComponent', () => {
  let component: VehicleInformationComponent;
  let fixture: ComponentFixture<VehicleInformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VehicleInformationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VehicleInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
