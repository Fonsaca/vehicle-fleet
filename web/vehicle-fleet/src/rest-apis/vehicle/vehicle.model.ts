
export interface ChassisId{
    serie: string;
    number: number;
}

export interface Vehicle{
    
    chassisId: ChassisId;
    color: string;
    type: string;
    numberOfPassengers: number;

}

export interface VehicleCreate{
    chassisId: ChassisId;
    color: string;
    type: string;
}

export interface VehicleEdit{
    chassisId: ChassisId;
    color: string;
}