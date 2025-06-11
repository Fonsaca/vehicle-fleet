import { Injectable, signal, WritableSignal } from '@angular/core';
import { IVehicleMgmtApiService } from '../../../rest-apis/vehicle/ivehicle-mgmt-api.service';
import { StatePromise } from '../../../infra/state-promise.model';
import { ChassisId, Vehicle, VehicleCreate, VehicleEdit } from '../../../rest-apis/vehicle/vehicle.model';
import { BehaviorSubject } from 'rxjs';
import { MessageService } from 'primeng/api';

@Injectable()
export class VehicleMgmtService {

  constructor(private api: IVehicleMgmtApiService, private notification: MessageService) { }

  loadVehiclesData = new BehaviorSubject<Vehicle[]>([]);
  loadVehicleLoading = new BehaviorSubject<boolean>(false);

  createModalState = new BehaviorSubject<boolean>(false);
  editModalState = new BehaviorSubject<{ showing: boolean, vehicle?: Vehicle }>({ showing: false, vehicle: undefined });

  async loadVehicles(): Promise<void> {
    var request = new StatePromise(this.api.getVehicles());
    request.registerLoadingSubject(this.loadVehicleLoading);
    
    const result = await request.awaitSafe();

    if(!result.isSuccess){
      this.notification.add({ severity: 'error', summary: 'Get Vehicles', detail: 'Error loading vehicles' });
      console.error(result.error);
    } else{
      this.loadVehiclesData.next(result.result!);
    }
  }

  async create(vehicle: VehicleCreate, loadingSignal : WritableSignal<boolean>)
    : Promise<{isSuccess: boolean, error?: string}> {
      
    var request = new StatePromise(this.api.create(vehicle));
    request.registerLoadingSignal(loadingSignal);
    
    const result = await request.awaitSafe();
    
    if(!result.isSuccess){
      this.notification.add({ severity: 'error', summary: 'Create Vehicle', detail: `Error creating the vehicle: ${result.error}` }); 
    } else{
      this.notification.add({ severity: 'success', summary: 'Create Vehicle', detail: `Vehicle created!` }); 
    }

    return {
      isSuccess: result.isSuccess,
      error: result.error
    }
  }

  async edit(vehicle: VehicleEdit, loadingSignal : WritableSignal<boolean>)
    : Promise<{isSuccess: boolean, error?: string}> {
      
    var request = new StatePromise(this.api.edit(vehicle));
    request.registerLoadingSignal(loadingSignal);
    
    const result = await request.awaitSafe();
    
    if(!result.isSuccess){
      this.notification.add({ severity: 'error', summary: 'Edit Vehicle', detail: `Error editing the vehicle. ${result.error}` }); 
    } else{
      this.notification.add({ severity: 'success', summary: 'Edit Vehicle', detail: `Vehicle edited!` }); 
    }

    return {
      isSuccess: result.isSuccess,
      error: result.error
    }
  }

  async search(chassisId: ChassisId, loadingSignal : WritableSignal<boolean> ): Promise<Vehicle|undefined>{
    var request = new StatePromise(this.api.findVehicles(chassisId));
    request.registerLoadingSignal(loadingSignal);
    
    const result = await request.awaitSafe();
    
    return result.result;
  }

}
