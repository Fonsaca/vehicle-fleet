import { Injectable } from "@angular/core";
import { ChassisId, Vehicle, VehicleCreate, VehicleEdit } from "./vehicle.model"
import { VehicleMgmtApiService } from "./vehicle-mgmt-api.service";


export abstract class IVehicleMgmtApiService {
    
    abstract getVehicles(): Promise<Vehicle[]>;

    abstract findVehicles(chassisId: ChassisId): Promise<Vehicle|undefined>;

    abstract create(vehicle: VehicleCreate): Promise<void>;

    abstract edit(vehicle: VehicleEdit): Promise<void>;

}


