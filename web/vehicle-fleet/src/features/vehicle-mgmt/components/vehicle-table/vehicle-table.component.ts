import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { Vehicle } from '../../../../rest-apis/vehicle/vehicle.model';
import { VehicleMgmtService } from '../../services/vehicle-mgmt.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'vehicle-table',
  standalone: false,
  templateUrl: './vehicle-table.component.html',
  styleUrl: './vehicle-table.component.css'
})
export class VehicleTableComponent implements OnInit, OnDestroy{

  constructor(private service: VehicleMgmtService){}
 
  async ngOnInit(): Promise<void> {
    this.registerLoadVehicles();

    await this.service.loadVehicles();
  }

  ngOnDestroy(): void {
    this.lifecycle.next();
  }


  private lifecycle = new Subject<void>();

  vehicles : Vehicle[] = [];

  loading = signal(false);


  beginCreate(): void{
    this.service.createModalState.next(true);
  }

  beginEdit(vehicle: Vehicle): void{
    this.service.editModalState.next({ showing: true, vehicle: vehicle });
  }

  private registerLoadVehicles(){
    this.service.loadVehicleLoading
      .pipe(takeUntil(this.lifecycle))
      .subscribe(loading => this.loading.set(loading));

    this.service.loadVehiclesData
      .pipe(takeUntil(this.lifecycle))
      .subscribe(vehicles =>this.vehicles = vehicles);
  }
}
