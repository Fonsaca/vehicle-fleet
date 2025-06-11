import { Inject, Injectable} from "@angular/core";
import { ChassisId, Vehicle, VehicleCreate, VehicleEdit } from "./vehicle.model";
import { HttpClient } from "@angular/common/http";
import { BASE_URL } from "../shared/base-url.token";
import { ApiResponse } from "../shared/api-response.model";
import { firstValueFrom, map } from "rxjs";
import { IVehicleMgmtApiService } from "./ivehicle-mgmt-api.service";

@Injectable()
export class VehicleMgmtApiService implements IVehicleMgmtApiService {

    private readonly baseApiUrl: string;

    constructor(private httpClient: HttpClient, @Inject(BASE_URL) private baseUrl: string) { 
        this.baseApiUrl = `${this.baseUrl}/vehicle`;
    }

    getVehicles(): Promise<Vehicle[]> {
        const url = this.baseApiUrl;
        var req = this.httpClient.get<ApiResponse<Vehicle>>(url)
            .pipe(map(response => this.mapResponse(response) ));
        return firstValueFrom(req);
    }

    findVehicles(chassisId: ChassisId): Promise<Vehicle|undefined> {
        const url = `${this.baseApiUrl}/serie/${chassisId.serie}/number/${chassisId.number}`;
        
        var req = this.httpClient.get<ApiResponse<Vehicle>>(url)
            .pipe(map(response => this.mapResponse(response)[0]));

        return firstValueFrom(req);
    }
    create(vehicle: VehicleCreate): Promise<void> {
        const url = this.baseApiUrl;
        
        var req = this.httpClient.post<ApiResponse<void>>(url, vehicle)
            .pipe(map(response => this.mapResponse(response,201)[0]));

        return firstValueFrom(req);
    }
    edit(vehicle: VehicleEdit): Promise<void> {
         const url = this.baseApiUrl;
        
        var req = this.httpClient.put<ApiResponse<void>>(url, vehicle)
            .pipe(map(response => this.mapResponse(response)[0]));

        return firstValueFrom(req);
    }

    private mapResponse<T>(response: ApiResponse<T>, successStatusCode: number = 200) {
        if(response.statusCode !== successStatusCode) {
            throw { error: response }
        }
        return response.data;
    }
}